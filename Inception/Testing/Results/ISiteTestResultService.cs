using Inception.Repository.Testing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Inception.Testing.Results
{
    public interface ISiteTestResultService
    {
        IIncludableQueryable<SiteTestResult, List<LinkTestResult>> GetAll();

        SiteTestResult GetById(int id);


    }
}
