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
    public class ProfilesController : ControllerBase
    {
        private readonly DataContext _context;
        private IMapper _IMapper;
        private IProfilesService _IProfilesService;

        public ProfilesController(DataContext context, IMapper imapper, IProfilesService iProfilesService)
        {
            _context = context;
            _IMapper = imapper;
            _IProfilesService = iProfilesService;
        }

        // up file lên database
        [HttpPost("createprofile")]
        public IActionResult creatProfiles([FromBody] UpProfiles model)
        {
            // ánh xạ model đến File
            var profile = _IMapper.Map<Profiles>(model);
            try
            {
                _IProfilesService.creatProfiles(profile);
                return Ok();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("getprofile")]
        public IActionResult GetListProfile([FromQuery] int postID)
        {

            try
            {
                var post = _IProfilesService.GetListProfile(postID);
                return Ok(post);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }



    }
}
