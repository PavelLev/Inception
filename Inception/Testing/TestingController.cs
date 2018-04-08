using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inception.Repository;
using Inception.Repository.Testing;
using Inception.Repository.Testing.Overview;
using Inception.Testing.Overview;
using Inception.Utility.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Inception.Testing
{
    public class TestingController : Controller
    {
        private readonly ISiteTestingService _testingService;
        private readonly IGenericRepository<SiteTestResult> _siteTestResultRepository;
        private readonly IBusinessExceptionProvider _businessExceptionProvider;
        private ISiteTestOverviewService _siteTestOverViewService;



        public TestingController
            (
            ISiteTestingService testingService,
            IGenericRepository<SiteTestResult> siteTestResultRepository,
            IBusinessExceptionProvider businessExceptionProvider,
            ISiteTestOverviewService siteTestOverViewService
            )
        {
            _testingService = testingService;
            _siteTestResultRepository = siteTestResultRepository;
            _businessExceptionProvider = businessExceptionProvider;
            _siteTestOverViewService = siteTestOverViewService;
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

            var siteTestResult = new SiteTestResult
            {
                DomainName = domainName,
                TestedOn = DateTime.Now,
                LinkTestResults = new List<LinkTestResult>()
            };

            await _siteTestResultRepository.Create(siteTestResult);

            _siteTestOverViewService.InitializeSiteTestOverview(siteTestResult);
            
            await _testingService.Process(siteTestResult);

            if (!siteTestResult.LinkTestResults.Any())
            {
                await _siteTestResultRepository.Delete(siteTestResult);

                _siteTestOverViewService.DeleteSiteTestOverView(siteTestResult);

                throw _businessExceptionProvider.Create(BusinessError.UnableToTestSite, "Unable to test site");
            }

            return Ok(siteTestResult.Id);
        }
    }
}
