using System.Threading.Tasks;
using AutoMapper;
using Inception.Repository;
using Inception.Repository.Testing;
using Inception.Utility.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Inception.Testing.Results
{
    public class LinkTestResultController : Controller
    {
        private readonly IGenericRepository<LinkTestResult> _linkTestResultRepository;
        private readonly IMapper _mapper;
        private readonly IBusinessExceptionProvider _businessExceptionProvider;



        public LinkTestResultController
            (
            IGenericRepository<LinkTestResult> linkTestResultRepository,
            IMapper mapper,
            IBusinessExceptionProvider businessExceptionProvider
            )
        {
            _linkTestResultRepository = linkTestResultRepository;
            _mapper = mapper;
            _businessExceptionProvider = businessExceptionProvider;
        }



        public async Task<IActionResult> GetById(int id)
        {
            var linkTestResult = await _linkTestResultRepository.GetById(id);

            if (linkTestResult == null)
            {
                throw _businessExceptionProvider.Create(BusinessError.IncorrectId, "There is no LinkTestResult with specified id");
            }


            var linkTestResultDto = ToDto(linkTestResult);

            return Ok(linkTestResultDto);
        }



        private LinkTestResultDto ToDto(LinkTestResult linkTestResult)
        {
            var linkTestResultDto = _mapper.Map<LinkTestResultDto>(linkTestResult);

            return linkTestResultDto;
        }
    }
}
