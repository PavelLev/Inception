using System.Collections.Generic;

namespace Inception.Testing
{
    public interface IDomainNameService
    {
        List<string> GetTestedSiteDomainNames(string filter);
    }
}