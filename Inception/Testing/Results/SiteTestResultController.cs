using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Inception.Repository;
using Inception.Repository.Testing;
using Inception.Utility.Exceptions;
using Inception.Utility.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inception.Testing.Results
{
    public class SiteTestResultController: Controller
    {
        private readonly IGenericRepository<SiteTestResult> _siteTestResultRepository;
        private readonly IMapper _mapper;
        private readonly IBusinessExceptionProvider _businessExceptionProvider;



        public SiteTestResultController
            (
            IGenericRepository<SiteTestResult> siteTestResultRepository,
            IMapper mapper,
            IBusinessExceptionProvider businessExceptionProvider
            )
        {
            _siteTestResultRepository = siteTestResultRepository;
            _mapper = mapper;
            _businessExceptionProvider = businessExceptionProvider;
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
            var siteTestResult = _siteTestResultRepository.GetById
                (
                id, 
                new Expression<Func<SiteTestResult, object>>[]
                {
                    someSiteTestResult => someSiteTestResult.LinkTestResults
                }).Result;

            if (siteTestResult == null)
            {
                throw _businessExceptionProvider.Create(BusinessError.IncorrectId, "There is no SiteTestResult with specified id");
            }


            var siteTestResultDto = ToDto(siteTestResult);

            return Ok(siteTestResultDto);
        }



        private SiteTestResultDto ToDto(SiteTestResult siteTestResult)
        {
            var siteTestResultDto = _mapper.Map<SiteTestResultDto>(siteTestResult);

            siteTestResultDto.LinkTestResultIds = siteTestResult.LinkTestResults.Select(x => x.Id)
                .ToList();

            return siteTestResultDto;
        }
    }
}