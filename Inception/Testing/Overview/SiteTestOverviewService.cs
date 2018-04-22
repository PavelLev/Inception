using Inception.Repository;
using Inception.Repository.Testing;
using Inception.Repository.Testing.Overview;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Inception.Testing.Overview
{
    public class SiteTestOverviewService : ISiteTestOverviewService
    {
        private readonly IGenericRepository<SiteTestOverview> _siteTestResultRepository;
        private readonly IGenericRepository<SiteTestOverview> _siteTestOverviewRepository;
        private readonly Dictionary<SiteTestResult, SiteTestOverview> _siteTestResultBySiteTestOverview = new Dictionary<SiteTestResult, SiteTestOverview>();
        private readonly AutoResetEvent _dbAutoResetEvent = new AutoResetEvent(true);

        public SiteTestOverviewService(
            IGenericRepository<SiteTestOverview> siteTestResultRepository,
            IGenericRepository<SiteTestOverview> siteTestOverviewRepository
            )
        {
            _siteTestResultRepository = siteTestResultRepository;
            _siteTestOverviewRepository = siteTestOverviewRepository;
        }

        public void InitializeSiteTestOverview(SiteTestResult siteTestResult)
        {

            var siteTestOverview = _siteTestOverviewRepository.GetAll()
                .Include(x => x.LinkTestOverviews)
                .FirstOrDefault(siteTestOverviewRepository => siteTestOverviewRepository.DomainName == siteTestResult.DomainName);
                

            _dbAutoResetEvent.WaitOne();

            if (siteTestOverview != null)
            {
                siteTestOverview.LastTestedOn = siteTestResult.TestedOn;                
                _siteTestOverviewRepository.Update(siteTestOverview);
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

                _siteTestOverviewRepository.Create(siteTestOverview);
               
            }
            _siteTestResultBySiteTestOverview.Add(siteTestResult, siteTestOverview);
            _dbAutoResetEvent.Set();
        }


        public void DeleteSiteTestOverView(SiteTestResult siteTestResult)
        {
            _dbAutoResetEvent.WaitOne();

            SiteTestOverview siteTestOverview = _siteTestResultBySiteTestOverview[siteTestResult];

            _siteTestOverviewRepository.Delete(siteTestOverview);

            _dbAutoResetEvent.Set();

            _siteTestResultBySiteTestOverview.Remove(siteTestResult);
        }


        public void UpdateLinkTestOverviews(SiteTestResult siteTestResult)
        {
            SiteTestOverview siteTestOverview = _siteTestResultBySiteTestOverview[siteTestResult];
            LinkTestOverview linkTestOverview = new LinkTestOverview();


            if (siteTestOverview.LinkTestOverviews == null)
            {
                siteTestOverview.LinkTestOverviews = new List<LinkTestOverview>();
            }

            siteTestResult.LinkTestResults.ToList().ForEach
                (
                linkTestResult =>
                    {
                        linkTestOverview = siteTestOverview.LinkTestOverviews.FirstOrDefault(x => x.Url == linkTestResult.Url);

                        if (linkTestOverview != null)
                        {
                            linkTestOverview = siteTestOverview.LinkTestOverviews.First(x => x.Url == linkTestResult.Url);
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
        }
    }
}
