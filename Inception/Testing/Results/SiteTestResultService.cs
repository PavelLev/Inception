using Inception.Repository;
using Inception.Repository.Testing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Inception.Testing.Results
{
    public class SiteTestResultService : ISiteTestResultService
    {
        private readonly IGenericRepository<SiteTestResult> _siteTestResultRepository;

        public SiteTestResultService(
            IGenericRepository<SiteTestResult> siteTestResultRepository
            )
        {
            _siteTestResultRepository = siteTestResultRepository;
        }


        public IIncludableQueryable<SiteTestResult, List<LinkTestResult>> GetAll()
        {
            var siteTestResults = _siteTestResultRepository.GetAll()
                .Include(siteTestResult => siteTestResult.LinkTestResults);


            return siteTestResults;
        }


        public SiteTestResult GetById(int id)
        {
            return _siteTestResultRepository.GetById
                (
                id,
                new Expression<Func<SiteTestResult, object>>[]
                {
                    siteTestResult => siteTestResult.LinkTestResults
                }).Result;
        }
    }
}
