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
    public class CompanyController : ApiController
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;
        private readonly Serilog.ILogger log;

        public CompanyController(ICompanyService companyService, IMapper mapper)
        {
            _companyService = companyService;
            _mapper = mapper;
            log = SerilogClass._logger;
        }

        // GET api/<controller>
        [HttpGet]
        public async Task<IEnumerable<CompanyDto>> GetAll()
        {
            try
            {
                var items = await _companyService.GetAllCompaniesAsync();
                log.Information("Companies : {@Items}", items);
                return _mapper.Map<IEnumerable<CompanyDto>>(items);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw;
            }
        }

        // GET api/<controller>/id?companyCode=#####
        [HttpGet]
        public async Task<CompanyDto> Get(string companyCode)
        {
            try
            {
                var item = await _companyService.GetCompanyByCodeAsync(companyCode);
                log.Information("Company <{Id}> : {@Item}", companyCode, item);
                return _mapper.Map<CompanyDto>(item);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw;
            }
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody] CompanyDto companyDto)
        {
            try
            {
                var companyies = _mapper.Map<CompanyInfo>(companyDto);
                await _companyService.SaveCompanyAsync(companyies);
                log.Information("Company <{Name}> has been Saved", companyies.CompanyName);
                return Ok();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw;
            }
        }

        // PUT api/<controller>/id?companyCode=#####
        [HttpPut]
        public async Task<IHttpActionResult> Update(string companyCode, [FromBody] CompanyDto companyDto)
        {
            try
            {
                var company = _mapper.Map<CompanyInfo>(companyDto);
                await _companyService.UpdateCompanyAsync(companyCode, company);
                log.Information("Company <{Name}> has been Updated", company.CompanyName);
                return Ok();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw;
            }
        }

        // DELETE api/<controller>/id?companyCode=#####
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteCompany(string companyCode)
        
        {
            try
            {
                await _companyService.DeleteCompanyAsync(companyCode);
                log.Information("Company Code <{Id}> has been Deleted", companyCode);
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