using AutoMapper;
using EmployeeManager.API.Data.Dtos;
using EmployeeManager.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManager.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;


        public EmployeeController(
            IEmployeeRepository employeeRepository,
            IMapper mapper
            )
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [Route("employee/{id}/")]
        public IActionResult GetEmployee(int id)
        {
            var employee = _employeeRepository.GetEmployee(id);
            return Ok(_mapper.Map<EmployeeDto>(employee));
        }        
        
        [HttpGet]
        [Route("employees/")]
        public IActionResult GetAllEmployees()
        {
            var employees = _employeeRepository.GetAllEmployees();
            return Ok(_mapper.Map<IEnumerable<EmployeeDto>>(employees));
        }
        
        [HttpPost]
        [Route("employees/")]
        public IActionResult AddEmployee(int id)
        {
            var employees = _employeeRepository.GetAllEmployees();
            return Ok(_mapper.Map<IEnumerable<EmployeeDto>>(employees));
        }
    }
}
