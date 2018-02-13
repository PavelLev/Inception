using System.Linq;
using AutoMapper;
using Inception.Repository;
using Inception.Repository.Testing;
using Microsoft.AspNetCore.Mvc;

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
            var siteTestResults = _siteTestResultRepository.GetAll();

            var siteTestResultDtos = siteTestResults.Select(x => ToDto(x));

            return Ok(siteTestResultDtos);
        }



        public IActionResult GetById(int id)
        {
            var siteTestResult = _siteTestResultRepository.GetById(id).Result;

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