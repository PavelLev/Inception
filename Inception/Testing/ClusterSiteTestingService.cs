using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Inception.Repository.Testing;
using Inception.Utility.Extensions;
using Inception.Utility.Parallel;

namespace Inception.Testing
{
    public class ClusterSiteTestingService : ISiteTestingService
    {
        private readonly ILinkTestingService _linkTestingService;
        private readonly ICluster<HttpClient> _cluster;



        public ClusterSiteTestingService(ILinkTestingService linkTestingService, Func<ClusterOptions, ICluster<HttpClient>> clusterFunc, TestingConfiguration testingConfiguration)
        {
            _linkTestingService = linkTestingService;

            _cluster = clusterFunc(new ClusterOptions
            {
                DegreeOfParallelism = testingConfiguration.DegreeOfParallelism
            });
        }



        public bool IsProcessing(SiteTestResult siteTestResult)
        {
            throw new System.NotImplementedException();
        }



        public async Task Process(SiteTestResult siteTestResult)
        {
            var visitedLinks = new HashSet<string>();

            var task = _cluster.DoWork(_ => _linkTestingService.Process(siteTestResult.DomainName, siteTestResult, visitedLinks));


            void ProcessChildLinks(Task<IEnumerable<string>> processTask)
            {
                foreach (var link in processTask.Result)
                {
                    _cluster.DoWork(_ => _linkTestingService.Process(link, siteTestResult, visitedLinks))
                        .ContinueWith(ProcessChildLinks);
                }
            }
            
            task.ContinueWith(ProcessChildLinks).NoWarning();


            await task;
        }
    }
}