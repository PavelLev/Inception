using Inception.Repository;
using Inception.Repository.Testing.Overview;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inception.Testing.Overview
{
    public class SiteTestOverviewService
    {
        private readonly IGenericRepository<SiteTestOverview> _siteTestResultRepository;

        public SiteTestOverviewService(
            IGenericRepository<SiteTestOverview> siteTestResultRepository
            )
        {
            _siteTestResultRepository = siteTestResultRepository;
        }
    }
}
