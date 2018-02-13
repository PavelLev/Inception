using System;
using System.Linq;
using System.Threading.Tasks;
using Inception.Repository;
using Inception.Repository.Testing;
using Microsoft.AspNetCore.Mvc;

namespace Inception.Testing
{
    public class TestingController : Controller
    {
        private readonly ITestingService _testingService;
        private readonly IGenericRepository<SiteTestResult> _siteTestResultRepository;



        public TestingController
            (
            ITestingService testingService,
            IGenericRepository<SiteTestResult> siteTestResultRepository
            )
        {
            _testingService = testingService;
            _siteTestResultRepository = siteTestResultRepository;
        }



        public async Task<IActionResult> TestSite
            (
            string domainName
            )
        {
            if (!domainName.Contains("://"))
            {
                domainName = "http://" + domainName;
            }


            var linkTestResults = (await _testingService.Process(domainName))
                .ToList();

            if (linkTestResults.Count == 0)
            {
                return BadRequest();
            }


            var siteTestResult = new SiteTestResult
            {
                DomainName = domainName,
                TestedOn = DateTime.Now,
                LinkTestResults = linkTestResults
            };

            await _siteTestResultRepository.Create(siteTestResult);


            return Ok(siteTestResult.Id);
        }
    }
}
