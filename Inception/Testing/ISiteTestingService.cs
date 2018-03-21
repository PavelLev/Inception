using System.Collections.Generic;
using System.Threading.Tasks;
using Inception.Repository.Testing;

namespace Inception.Testing
{
    public interface ISiteTestingService
    {
        bool IsProcessing
            (
            SiteTestResult siteTestResult
            );

        Task Process
            (
            SiteTestResult siteTestResult
            );
    }
}
