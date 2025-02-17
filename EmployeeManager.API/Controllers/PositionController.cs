using AutoMapper;
using EmployeeManager.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManager.API.Controllers
{
    [Route("api/positions")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly IPositionRepository _positionRepository;
        private readonly IMapper _mapper;

        public PositionController(
            IPositionRepository positionRepository,
            IMapper mapper
            )
        {
            _positionRepository = _positionRepository ?? throw new ArgumentNullException(nameof(_positionRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public IActionResult GetAllPositions()
        {
            var res = _positionRepository.GetPositionsList();
            return Ok(res);
        }
    }
}
