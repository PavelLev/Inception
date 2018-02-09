using System.Threading.Tasks;
using AutoMapper;
using Inception.Repository;
using Inception.Repository.Testing;
using Microsoft.AspNetCore.Mvc;

namespace Inception.Testing
{
    public class LinkTestResultController : Controller
    {
        private readonly IGenericRepository<LinkTestResult> _linkTestResultRepository;
        private readonly IMapper _mapper;



        public LinkTestResultController
            (
            IGenericRepository<LinkTestResult> linkTestResultRepository,
            IMapper mapper
            )
        {
            _linkTestResultRepository = linkTestResultRepository;
            _mapper = mapper;
        }



        public async Task<IActionResult> GetById(int id)
        {
            var linkTestResult = await _linkTestResultRepository.GetById(id);

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
