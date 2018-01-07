using System;
using System.Collections.Generic;
using Inception.Testing.Overview;
using Microsoft.AspNetCore.Mvc;

namespace Inception.Testing
{
    public class SiteTestController: Controller
    {
        public List<SiteTestResultThumbnail> GetSiteResults(string filter)
        {
            throw new NotImplementedException();
        }

        public List<string> GetFilteredSiteDomainNames(string filter)
        {
            throw new NotImplementedException();
        }

        public string TestSite(string domainName)
        {
            throw new NotImplementedException();
        }

        public SiteTestResult GetDetailedSiteTestResult(string siteTestResultId)
        {
            throw new NotImplementedException();
        }

        public SiteTestOverview GetSiteTestOverview(string domainName)
        {
            throw new NotImplementedException();
        }
    }
}