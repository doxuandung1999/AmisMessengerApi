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
    public class PostController : ControllerBase
    {
        private readonly DataContext _context;
        private IMapper _IMapper;
        private ICompanyService _ICompanyService;
        private IPostService _IPostService;

        public PostController(DataContext context, IMapper imapper, ICompanyService companyService, IPostService postService)
        {
            _context = context;
            _IMapper = imapper;
            _ICompanyService = companyService;
            _IPostService = postService;
        }

        // up file lên database
        [HttpPost("createpost")]
        public IActionResult creatPost([FromBody] UpPost model)
        {
            // ánh xạ model đến File
            var post = _IMapper.Map<Post>(model);
            try
            {
                _IPostService.creatPost(post);
                return Ok();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // up file lên database
        [HttpGet("getpostbyuserid")]
        public IActionResult GetAllByUserID([FromQuery] Guid userID)
        {

            try
            {
                var listpost = _IPostService.GetAllByUserID(userID);
                return Ok(listpost);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // up file lên database
        [HttpGet("getallpost")]
        public IActionResult GetAll()
        {

            try
            {
                var listpost = _IPostService.GetAll();
                return Ok(listpost);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("getpostnotacept")]
        public IActionResult GetPostNotAccept()
        {

            try
            {
                var listpost = _IPostService.GetPostNotAccept();
                return Ok(listpost);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // up file lên database
        [HttpGet("getpost")]
        public IActionResult GetPost([FromQuery] int postID ,[FromQuery] Guid userID)
        {

            try
            {
                var post = _IPostService.GetPost(postID,userID);
                return Ok(post);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("getpostbypostid")]
        public IActionResult GetPostByPostID([FromQuery] int postID)
        {

            try
            {
                var post = _IPostService.GetPostByPostID(postID);
                return Ok(post);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // up file lên database
        [HttpGet("getpostbycompanyid")]
        public IActionResult GetPostByCompanyID([FromQuery] int companyID, [FromQuery] Guid userID)
        {

            try
            {
                var post = _IPostService.GetPostByCompanyID(companyID, userID);
                return Ok(post);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        //edit usser
        [HttpPost("updatepost")]
        public async Task<IActionResult> EditPost([FromBody] Post model)
        {
            try
            {
                await _IPostService.EditPost(model);
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

        //edit usser
        [HttpPost("updatestatus")]
        public async Task<IActionResult> UpdateStatus([FromQuery] int postID)
        {
            try
            {
                await _IPostService.UpdateStatus(postID);
                return Ok();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("deletepost")]
        public async Task<IActionResult> DeletePost([FromQuery] int postID)
        {
            try
            {
                await _IPostService.DeletePost(postID);
                return Ok();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
