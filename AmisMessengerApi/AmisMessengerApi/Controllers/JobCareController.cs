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
    public class JobCareController : ControllerBase
    {
        private readonly DataContext _context;
        private IMapper _IMapper;
        private IJobCareService _IJobCareService;

        public JobCareController(DataContext context, IMapper imapper, IJobCareService jobCareService)
        {
            _context = context;
            _IMapper = imapper;
            _IJobCareService = jobCareService;
        }

        // up file lên database
        [HttpPost("createjobcare")]
        public IActionResult creatJobcare([FromBody] UpJobCare model)
        {
            // ánh xạ model đến File
            var jobcare = _IMapper.Map<JobCare>(model);
            try
            {
                _IJobCareService.creatJobCare(jobcare);
                return Ok();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // up file lên database
        [HttpPost("updatejobcare")]
        public IActionResult UpdateJobCare([FromBody] UpJobCare model)
        {
            
            var jobcare = _IMapper.Map<JobCare>(model);
            try
            {
                _IJobCareService.UpdateJobCare(jobcare);
                return Ok();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
