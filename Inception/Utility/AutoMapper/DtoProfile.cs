﻿using AutoMapper;
using Inception.Repository.Testing;
using Inception.Repository.Testing.Overview;
using Inception.Testing.Results;

namespace Inception.Utility.AutoMapper
{
    public class DtoProfile : Profile
    {
        public DtoProfile()
        {
            CreateMap<SiteTestResult, SiteTestResultDto>();

            CreateMap<LinkTestResult, LinkTestResultDto>();

            CreateMap<SiteTestResult, SiteTestResultThumbnail>();

            CreateMap<SiteTestOverview, SiteTestOverviewDto>();

            CreateMap<LinkTestOverview, LinkTestOverviewDto>();
        }
    }
}
