using System;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Inception.Repository;
using Inception.Repository.Testing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inception.Testing.Results
{
    public class SiteTestResultController: Controller
    {
        private readonly IGenericRepository<SiteTestResult> _siteTestResultRepository;
        private readonly IMapper _mapper;



        public SiteTestResultController
            (
            IGenericRepository<SiteTestResult> siteTestResultRepository,
            IMapper mapper
            )
        {
            _siteTestResultRepository = siteTestResultRepository;
            _mapper = mapper;
        }



        public IActionResult GetAll(string filter)
        {
            var siteTestResults = _siteTestResultRepository.GetAll()
                .Include(siteTestResult => siteTestResult.LinkTestResults);

            var siteTestResultDtos = siteTestResults.Select(x => ToDto(x));

            return Ok(siteTestResultDtos);
        }



        public IActionResult GetById(int id)
        {            
            var specifiedSiteTestResult = _siteTestResultRepository.GetById
                (
                id,
                new Expression<Func<SiteTestResult, object>>[]
                {
                    siteTestResult => siteTestResult.LinkTestResults
                }).Result;

            var siteTestResultDto = ToDto(specifiedSiteTestResult);

            return Ok(siteTestResultDto);
        }



        public IActionResult GetSiteTestResultThumbnails(string domainName)
        {
            var SiteTestResults = _siteTestResultRepository.GetAll().Where(siteTestResult => siteTestResult.DomainName.Equals(domainName));

            var SiteTestResultThumbnails = SiteTestResults.Select(siteTestResult => ToSiteTestResultThumbnail(siteTestResult));

            return Ok(SiteTestResultThumbnails);
        }


        private SiteTestResultDto ToDto(SiteTestResult siteTestResult)
        {
            var siteTestResultDto = _mapper.Map<SiteTestResultDto>(siteTestResult);

            siteTestResultDto.LinkTestResultIds = siteTestResult.LinkTestResults.Select(x => x.Id)
                .ToList();

            return siteTestResultDto;
        }

        private SiteTestResultThumbnail ToSiteTestResultThumbnail(SiteTestResult siteTestResult)
        {
            var siteTestResultThumbnails = _mapper.Map<SiteTestResultThumbnail>(siteTestResult);

            return siteTestResultThumbnails;
        }
    }
}