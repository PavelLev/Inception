using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inception.Repository;
using Inception.Repository.Testing;
using Inception.Repository.Testing.Overview;
using Inception.Utility.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Inception.Testing
{
    public class TestingController : Controller
    {
        private readonly ISiteTestingService _testingService;
        private readonly IGenericRepository<SiteTestResult> _siteTestResultRepository;
        private readonly IGenericRepository<SiteTestOverview> _siteTestOverviewRepository;
        private readonly IBusinessExceptionProvider _businessExceptionProvider;



        public TestingController
            (
            ISiteTestingService testingService,
            IGenericRepository<SiteTestResult> siteTestResultRepository,
            IBusinessExceptionProvider businessExceptionProvider,
            IGenericRepository<SiteTestOverview> siteTestOverviewRepository
            )
        {
            _testingService = testingService;
            _siteTestResultRepository = siteTestResultRepository;
            _siteTestOverviewRepository = siteTestOverviewRepository;
            _businessExceptionProvider = businessExceptionProvider;
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

            SiteTestOverview siteTestOverview;

            var siteTestResult = new SiteTestResult
            {
                DomainName = domainName,
                TestedOn = DateTime.Now,
                LinkTestResults = new List<LinkTestResult>()
            };

            await _siteTestResultRepository.Create(siteTestResult);

            if (_siteTestOverviewRepository.GetAll().Any(siteTestOverviewRepository => siteTestOverviewRepository.DomainName == siteTestResult.DomainName))
            {
                siteTestOverview = _siteTestOverviewRepository.GetAll().Where(siteTestOverviewRepository => siteTestOverviewRepository.DomainName == siteTestResult.DomainName).First();

                siteTestOverview.LastTestedOn = siteTestResult.TestedOn;

                await _siteTestOverviewRepository.Update(siteTestOverview);
            }
            else
            {
                siteTestOverview = new SiteTestOverview
                {
                    FirstTestedOn = siteTestResult.TestedOn,
                    DomainName = siteTestResult.DomainName,
                    LastTestedOn = siteTestResult.TestedOn,
                    LinkTestOverviews = new List<LinkTestOverview>()
                };

                await _siteTestOverviewRepository.Create(siteTestOverview);
            }




            await _testingService.Process(siteTestResult);


            if (!siteTestResult.LinkTestResults.Any())
            {
                await _siteTestResultRepository.Delete(siteTestResult);

                await _siteTestOverviewRepository.Delete(siteTestOverview);

                throw _businessExceptionProvider.Create(BusinessError.UnableToTestSite, "Unable to test site");
            }

            if (siteTestOverview.LinkTestOverviews == null) 
            {
                siteTestOverview.LinkTestOverviews = new List<LinkTestOverview>();
            }  

            siteTestResult.LinkTestResults.ToList().ForEach(
                linkTestResult =>
                {
                    if (siteTestOverview.LinkTestOverviews.Any(x => x.Url == linkTestResult.Url))
                    {
                        var linkTestOverview = siteTestOverview.LinkTestOverviews.First(x => x.Url == linkTestResult.Url);
                        linkTestOverview.MinimumResponseTime = Math.Min(linkTestOverview.MinimumResponseTime, linkTestResult.ResponseTime);
                        linkTestOverview.MaximumResponseTime = Math.Max(linkTestOverview.MaximumResponseTime, linkTestResult.ResponseTime);
                    }
                    else
                    {
                        siteTestOverview.LinkTestOverviews.Add(
                            new LinkTestOverview
                            {
                                MaximumResponseTime = linkTestResult.ResponseTime,
                                MinimumResponseTime = linkTestResult.ResponseTime,
                                Url = linkTestResult.Url,
                                SiteTestOverviewId = siteTestOverview.Id,
                                SiteTestOverview = siteTestOverview
                            }
                        );
                    }
                }
            );

            await _siteTestOverviewRepository.Update(siteTestOverview);


            return Ok(siteTestResult.Id);
        }
    }
}
