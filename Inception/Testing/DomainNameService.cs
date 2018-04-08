using Inception.Repository;
using Inception.Repository.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inception.Testing
{
    public class DomainNameService : IDomainNameService
    {
        private readonly IGenericRepository<SiteTestResult> _siteTestResultRepository;
        private readonly DomainNameConfiguration _domainNameConfiguration;

        public DomainNameService(
            IGenericRepository<SiteTestResult> siteTestResultRepository,
            DomainNameConfiguration domainNameConfiguration
            )
        {
            _siteTestResultRepository = siteTestResultRepository;
            _domainNameConfiguration = domainNameConfiguration;
        }


        public List<string> GetTestedSiteDomainNames(string filter)
        {

            return _siteTestResultRepository
                .GetAll()
                .Select(siteTestResult => siteTestResult.DomainName)
                .Where(domainName => domainName.Contains(filter))              
                .Distinct()
                .Take(_domainNameConfiguration.DomainNamesNumber)
                .OrderByDescending(domainName => domainName)
                .ToList();
        }
    }
}
