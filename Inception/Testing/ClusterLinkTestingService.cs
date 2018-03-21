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
    public class ClusterLinkTestingService : ILinkTestingService
    {
        private readonly IUriService _uriService;
        private readonly TestingConfiguration _testingConfiguration;
        private readonly HttpClient _httpClient;
        private readonly AutoResetEvent _dbAutoResetEvent;
        private readonly IGenericRepository<SiteTestResult> _siteTestResultRepository;
        private readonly IHtmlParser _htmlParser;

        public ClusterLinkTestingService
            (
            IUriService uriService, 
            TestingConfiguration testingConfiguration, 
            HttpClient httpClient, 
            AutoResetEvent dbAutoResetEvent, 
            IGenericRepository<SiteTestResult> siteTestResultRepository, 
            IHtmlParser htmlParser
            )
        {
            _uriService = uriService;
            _testingConfiguration = testingConfiguration;
            _httpClient = httpClient;
            _dbAutoResetEvent = dbAutoResetEvent;
            _siteTestResultRepository = siteTestResultRepository;
            _htmlParser = htmlParser;
        }



        public async Task<IEnumerable<string>> Process
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
                return Enumerable.Empty<string>();
            }


            var start = DateTime.Now;

            HttpResponseMessage httpResponseMessage;

            try
            {
                httpResponseMessage = await _httpClient.GetAsync(url);
            }
            catch //server may return some junk
            {
                return Enumerable.Empty<string>();
            }

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                return Enumerable.Empty<string>();
            }

            var html = await httpResponseMessage.Content.ReadAsStringAsync();


            var end = DateTime.Now;

            var loaded = (end - start).TotalMilliseconds;

            if (siteTestResult.LinkTestResults.Count >= _testingConfiguration.LinkTestResultLimit)
            {
                return Enumerable.Empty<string>();
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


            var links = _htmlParser.GetLinks(html)
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
                    );

            return links;
        }
    }
}