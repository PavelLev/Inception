using System.Collections.Generic;
using System.Threading.Tasks;
using Inception.Repository.Testing;

namespace Inception.Testing
{
    public interface ILinkTestingService
    {
        Task<IEnumerable<string>> Process
            (
            string url,
            SiteTestResult siteTestResult,
            ISet<string> visitedLinks
            );
    }
}
