using AutoMapper;
using EmployeeManager.API.Data.Dtos;
using EmployeeManager.API.Data.DTOs;
using EmployeeManager.API.Data.Models;
using EmployeeManager.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManager.API.Controllers
{
    [Route("api/positions")]
    [ApiController]
    //[Authorize]
    public class PositionController : ControllerBase
    {
        private readonly IPositionRepository _positionRepository;
        private readonly IMapper _mapper;

        public PositionController(
            IPositionRepository positionRepository,
            IMapper mapper
            )
        {
            _positionRepository = positionRepository ?? throw new ArgumentNullException(nameof(positionRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public IActionResult GetAllPositions()
        {
            var res = _positionRepository.GetPositionsList();
            return Ok(res);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetPosition(int id)
        {
            var res = _positionRepository.GetPosition(id);
            if (res == null)
            {
                return NotFound("Position not found.");
            }
            return Ok(res);
        }


        [HttpPost]
        [Route("create")]
        public IActionResult Create(PositionCreateDto submittedPosition)
        {
            var mappedPosition = _mapper.Map<Position>(submittedPosition);
            _positionRepository.CreatePosition(mappedPosition);
            _positionRepository.SaveChanges();
            return Ok(mappedPosition);
        }

        [HttpPut]
        [Route("update/{id}")]
        public IActionResult Update(int id, PositionUpdateDto updatedPosition)
        {
            var targetPosition = _positionRepository.GetPosition(id);
            if (targetPosition == null)
            {
                return NotFound("Position not found.");
            }
            _mapper.Map(updatedPosition, targetPosition);
            _positionRepository.SaveChanges();
            return Ok(targetPosition);
        }


        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var position = _positionRepository.GetPosition(id);
            if (position == null)
            {
                return NotFound("Position not found.");
            }
            _positionRepository.DeletePosition(position);
            _positionRepository.SaveChanges();
            return Ok();
        }
    }
}
