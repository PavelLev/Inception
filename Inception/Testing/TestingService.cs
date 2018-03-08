using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Inception.Repository;
using Inception.Repository.Testing;
using Inception.Utility;
using Inception.Utility.Extensions;

namespace Inception.Testing
{
    public class TestingService : ITestingService
    {
        private readonly HttpClient _httpClient;
        private readonly TestingConfiguration _testingConfiguration;
        private readonly IUriService _uriService;
        private readonly IHtmlParser _htmlParser;
        private readonly IGenericRepository<SiteTestResult> _siteTestResultRepository;
        private readonly List<SiteTestResult> _processingSiteTestResults;
        private readonly AutoResetEvent _processAutoResetEvent = new AutoResetEvent(true);
        private readonly AutoResetEvent _dbAutoResetEvent = new AutoResetEvent(true);



        public TestingService
            (
            HttpClient httpClient,
            TestingConfiguration testingConfiguration,
            IUriService uriService,
            IHtmlParser htmlParser,
            IGenericRepository<SiteTestResult> siteTestResultRepository
            )
        {
            _httpClient = httpClient;
            _testingConfiguration = testingConfiguration;
            _uriService = uriService;
            _htmlParser = htmlParser;
            _siteTestResultRepository = siteTestResultRepository;
            _processingSiteTestResults = new List<SiteTestResult>();
        }



        public bool IsProcessing(SiteTestResult siteTestResult)
        {
            var isProcessing = _processingSiteTestResults.Any
                (
                someSiteTestResult => someSiteTestResult.Id == siteTestResult.Id
                );

            return isProcessing;
        }



        public async Task<SiteTestResult> Process
            (
            SiteTestResult siteTestResult
            )
        {
            _processingSiteTestResults.Add(siteTestResult);


            _processAutoResetEvent.WaitOne();

            await Process
                (
                siteTestResult.DomainName,
                siteTestResult,
                new HashSet<string>()
                );


            if (!siteTestResult.LinkTestResults.Any())
            {
                _processingSiteTestResults.Remove(siteTestResult);
            }

            return siteTestResult;
        }



        private async Task Process
            (
            string url,
            SiteTestResult siteTestResult,
            ISet<string> visitedLinks
            )
        {
            var normalizedUrl = _uriService.Normalize(url);

            if (siteTestResult.LinkTestResults.Count >= _testingConfiguration.LinkTestResultLimit 
                ||
                visitedLinks.Contains(normalizedUrl))
            {
                return;
            }


            var start = DateTime.Now;

            HttpResponseMessage httpResponseMessage;

            try
            {
                httpResponseMessage = await _httpClient.GetAsync(url);
            }
            catch //server may return some junk
            {
                return;
            }

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                return;
            }

            var html = await httpResponseMessage.Content.ReadAsStringAsync();


            var end = DateTime.Now;

            var loaded = (end - start).TotalMilliseconds;

            if (siteTestResult.LinkTestResults.Count >= _testingConfiguration.LinkTestResultLimit)
            {
                return;
            }

            if (visitedLinks.Add(normalizedUrl))
            {
                var linkTestResult = new LinkTestResult
                {
                    ResponseTime = Convert.ToInt32(loaded),
                    Url = normalizedUrl
                };

                siteTestResult.LinkTestResults.Add(linkTestResult);


                _dbAutoResetEvent.WaitOne();

                await _siteTestResultRepository.Update(siteTestResult);

                _dbAutoResetEvent.Set();
            }


            var tasks = ProcessChildLinks
                (
                url,
                siteTestResult,
                visitedLinks,
                html
                );


            if (siteTestResult.LinkTestResults.Count == 1)
            {
                Task.Run(async () =>
                    {
                        try
                        {
                            await Task.WhenAll(tasks);
                        }
                        finally
                        {
                            _processingSiteTestResults.Remove(siteTestResult);

                            _processAutoResetEvent.Set();
                        }
                    })
                    .NoWarning();
            }
            else
            {
                await Task.WhenAll(tasks);
            }
        }



        private IEnumerable<Task> ProcessChildLinks
            (
            string url,
            SiteTestResult siteTestResult,
            ISet<string> visitedLinks,
            string html
            )
        {
            var tasks = _htmlParser.GetLinks(html)
                .Select
                (
                link => _uriService.ToAbsolute
                    (
                    url,
                    link
                    )
                )
                .Where
                (
                link => _uriService.AreFromSameHost
                    (
                    link,
                    url
                    )
                )
                .Select
                (
                link =>
                {
                    var task = Process
                        (
                        link,
                        siteTestResult,
                        visitedLinks
                        );

                    return task;
                });

            return tasks;
        }
    }
}
