using Inception.Repository.Testing;
using Inception.Repository.Testing.Overview;

namespace Inception.Testing.Overview
{
    public interface ISiteTestOverviewService
    {
        void InitializeSiteTestOverview(SiteTestResult siteTestResult);

        void DeleteSiteTestOverView(SiteTestResult siteTestResult);

        void UpdateLinkTestOverviews(SiteTestResult siteTestResult);

    }
}
