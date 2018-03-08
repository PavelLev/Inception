using System.Collections.Generic;
using System.Threading.Tasks;
using Inception.Repository.Testing;

namespace Inception.Testing
{
    public interface ITestingService
    {
        bool IsProcessing
            (
            SiteTestResult siteTestResult
            );

        Task<SiteTestResult> Process
            (
            SiteTestResult siteTestResult
            );
    }
}
