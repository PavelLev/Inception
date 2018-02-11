using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Inception.Repository.Testing;
using Inception.Utility;

namespace Inception.Testing
{
    public class TestingService
    {
        private readonly HttpClient _httpClient;
        private readonly TestingConfiguration _testingConfiguration;
        private readonly IUriService _uriService;
        private readonly IHtmlParser _htmlParser;



        public TestingService
            (
            HttpClient httpClient,
            TestingConfiguration testingConfiguration,
            IUriService uriService,
            IHtmlParser htmlParser
            )
        {
            _httpClient = httpClient;
            _testingConfiguration = testingConfiguration;
            _uriService = uriService;
            _htmlParser = htmlParser;
        }



        public async Task<IEnumerable<LinkTestResult>> Process
            (
            string url
            )
        {
            var linkTestResults = new List<LinkTestResult>();

            await Process
                (
                url,
                linkTestResults,
                new HashSet<string>()
                );

            return linkTestResults;
        }



        private async Task Process
            (
            string url,
            IList<LinkTestResult> linkTestResults,
            ISet<string> visitedLinks
            )
        {
            var normalizedUrl = _uriService.Normalize(url);

            if (linkTestResults.Count >= _testingConfiguration.LinkTestResultLimit 
                ||
                visitedLinks.Contains(normalizedUrl))
            {
                return;
            }


            var start = DateTime.Now;

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return;
            }

            var html = await response.Content.ReadAsStringAsync();


            var end = DateTime.Now;

            var loaded = (end - start).TotalMilliseconds;


            var linkTestResult = new LinkTestResult
            {
                ResponseTime = Convert.ToInt32(loaded),
                Url = normalizedUrl
            };

            if (linkTestResults.Count < _testingConfiguration.LinkTestResultLimit 
                &&
                visitedLinks.Add(normalizedUrl))
            {
                linkTestResults.Add(linkTestResult);
            }


            var tasks = ProcessChildLinks
                (
                url,
                linkTestResults,
                visitedLinks,
                html
                );

            await Task.WhenAll(tasks);
        }



        private IEnumerable<Task> ProcessChildLinks
            (
            string url,
            IList<LinkTestResult> linkTestResults,
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
                        linkTestResults,
                        visitedLinks
                        );

                    return task;
                });

            return tasks;
        }
    }
}
