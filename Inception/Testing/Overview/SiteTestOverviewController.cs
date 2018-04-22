using AutoMapper;
using Inception.Repository;
using Inception.Repository.Testing.Overview;
using Inception.Utility.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Inception.Testing.Overview
{
    public class SiteTestOverviewController : Controller
    {
        private readonly IBusinessExceptionProvider _businessExceptionProvider;
        private readonly IGenericRepository<SiteTestOverview> _siteTestOverviewRepository;
        private readonly IMapper _mapper;


        public SiteTestOverviewController(
            IBusinessExceptionProvider businessExceptionProvider,
            IGenericRepository<SiteTestOverview> siteTestOverviewRepository,
            IMapper mapper
            )
        {
            _businessExceptionProvider = businessExceptionProvider;
            _siteTestOverviewRepository = siteTestOverviewRepository;
            _mapper = mapper;
        }



        public SiteTestOverviewDto GetSiteTestOverview(string domainName)
        {
            var siteTestOverview = _siteTestOverviewRepository.GetAll()
            .Include(x => x.LinkTestOverviews)
            .FirstOrDefault(x => x.DomainName == domainName);

            var siteTestOverViewDto = ToDto(siteTestOverview);

            siteTestOverViewDto.LinkTestOverviews = siteTestOverViewDto.LinkTestOverviews.OrderBy(x => x.MinimumResponseTime).ToList();

            return siteTestOverViewDto;
        }

        private SiteTestOverviewDto ToDto(SiteTestOverview siteTestOverView)
        {
            var siteTestOverViewDto = _mapper.Map<SiteTestOverviewDto>(siteTestOverView);

            return siteTestOverViewDto;
        }
    }
}
