using System;
using DryIoc;
using Inception.Repository.Testing.Overview;
using Inception.Utility.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Inception.Testing.Overview
{
    public class SiteTestOverviewController : Controller
    {
        private readonly IBusinessExceptionProvider _businessExceptionProvider;



        public SiteTestOverviewController(IBusinessExceptionProvider businessExceptionProvider)
        {
            _businessExceptionProvider = businessExceptionProvider;
        }



        public SiteTestOverview GetSiteTestOverview(string domainName)
        {
            throw _businessExceptionProvider.Create(BusinessError.UnknownError, "Shit happened");
        }
    }
}
