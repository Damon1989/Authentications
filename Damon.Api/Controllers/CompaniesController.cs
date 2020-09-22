using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Damon.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Damon.Api.Controllers
{
    [ApiController]
    [Route("api/companies")]
    public class CompaniesController:ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;

        public CompaniesController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository??
                throw new ArgumentNullException(nameof(companyRepository));
        }


        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            var companies = await _companyRepository.GetCompaniesAsync();
            return new JsonResult(companies);
        }
    }
}
