using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AmisMessengerApi.Entities;
using AmisMessengerApi.Helper;
using AutoMapper;
using AmisMessengerApi.Services;
using AmisMessengerApi.Models.Files;

namespace AmisMessengerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly DataContext _context;
        private IMapper _IMapper;
        private ICompanyService _ICompanyService;

        public CompanyController(DataContext context, IMapper imapper, ICompanyService companyService)
        {
            _context = context;
            _IMapper = imapper;
            _ICompanyService = companyService;
        }

        // up file lên database
        [HttpPost("createcompany")]
        public IActionResult creatCompany([FromBody] UpCompany model)
        {
            // ánh xạ model đến File
            var company = _IMapper.Map<Company>(model);
            try
            {
                _ICompanyService.creatCompany(company);
                return Ok();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // get cty theo convId
        [HttpGet("{userId}")]
        public async Task<ActionResult<Company>> GetFile(Guid userID)
        {
            var file = await _context.Company.Where(u => u.UserId == userID).ToListAsync();

            return Ok(file);
        }

        // GET: api/Files
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Company>>> GetFile()
        {
            return await _context.Company.ToListAsync();
        }


        // GET: api/Files/5


        // PUT: api/Files/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFile(int id, [FromBody] Company company)
        {
            if (id != company.CompanyId)
            {
                return BadRequest();
            }

            _context.Entry(company).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //edit usser
        [HttpPost("updatecompany")]
        public async Task<IActionResult> EditCompany([FromBody] Company model)
        {
            try
            {
                await _ICompanyService.EditCompany(model);
                return Ok(new
                {
                    data = model
                });
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("getcompanybyid")]
        public async Task<IActionResult> GetCompany([FromQuery] Guid id)
        {
            try
            {
                var company = await _ICompanyService.GetCompany(id);
                return Ok(new
                {
                    data = company
                });
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("getcompanybycompanyid")]
        public async Task<IActionResult> GetCompanyByCompanyID([FromQuery] int id)
        {
            try
            {
                var company = await _ICompanyService.GetCompanyByCompanyID(id);
                return Ok(new
                {
                    data = company
                });
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        

        private bool CompanyExists(int id)
        {
            return _context.Company.Any(e => e.CompanyId == id);
        }
    }
}
