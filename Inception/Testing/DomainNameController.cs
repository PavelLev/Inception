using System;
using System.Collections.Generic;
using System.Linq;
using Inception.Repository;
using Inception.Repository.Testing;
using Microsoft.AspNetCore.Mvc;

namespace Inception.Testing
{
    public class DomainNameController : Controller
    {
        private readonly IDomainNameService _domainNameService;

        public DomainNameController
            (
            IDomainNameService domainNameService
            )
        {
            _domainNameService = domainNameService;
        }

        public List<string> GetTestedSiteDomainNames(string filter)
        {
            var testedSiteDomainNames = _domainNameService.GetTestedSiteDomainNames(filter);
            return testedSiteDomainNames; 
        }
    }
}
