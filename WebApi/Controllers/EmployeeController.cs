using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        private readonly Serilog.ILogger log;

        public EmployeeController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
            log = SerilogClass._logger;
        }
        // GET api/<controller>
        [HttpGet]
        public async Task<IEnumerable<EmployeeDto>> GetAll()
        {
            try
            {
                var employees = await _employeeService.GetAllEmployeesAsync();
                log.Information("Employees : {@Employees}", employees);
                return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw;
            }
        }

        // GET api/<controller>/id?employeeCode=99999
        [HttpGet]
        public async Task<EmployeeDto> Get(string employeeCode)
        {
            try
            {
                var employee = await _employeeService.GetEmployeeByCodeAsync(employeeCode);
                log.Information("Employees : {@Employee}", employee);
                return _mapper.Map<EmployeeDto>(employee);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw;
            }
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody] EmployeeDto employeeDto)
        {
            try
            {


                var employee = _mapper.Map<EmployeeInfo>(employeeDto);
                await _employeeService.SaveEmployeeAsync(employee);
                log.Information("Employee <{Name}> has been Saved", employee.EmployeeName);
                return Ok();


            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw;
            }

        }

        // PUT api/<controller>/id?employeeCode=99999
        [HttpPut]
        public async Task<IHttpActionResult> Update(string employeeCode, [FromBody] EmployeeDto employeeDto)
        {
            try
            {
                var employee = _mapper.Map<EmployeeInfo>(employeeDto);
                await _employeeService.UpdateEmployeeAsync(employeeCode, employee);
                log.Information("Employee <{Name}> has been Updated", employee.EmployeeName);
                return Ok();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw;
            }

        }

        // DELETE api/<controller>/id?employeeCode=99999
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteEmployee(string employeeCode)
        {
            try
            {
                await _employeeService.DeleteEmployeeAsync(employeeCode);
                log.Information("Emplopyee Code <{Id}> has been Deleted", employeeCode);
                return Ok();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw;
            }
        }
    }
}

