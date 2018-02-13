using System.Collections.Generic;
using System.Threading.Tasks;
using Inception.Repository.Testing;

namespace Inception.Testing
{
    public interface ITestingService
    {
        Task<IEnumerable<LinkTestResult>> Process
            (
            string url
            );
    }
}
