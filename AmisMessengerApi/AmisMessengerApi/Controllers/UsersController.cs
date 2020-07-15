﻿using System;
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
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using AmisMessengerApi.Models.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace AmisMessengerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly DataContext _context;
        private IMapper _IMapper;
        private IUserService _IUserService;
        private AppSettings _AppSettings;

        public UsersController(DataContext context , IMapper imapper , IUserService iuserService , IOptions<AppSettings> appsettings)
        {
            _context = context;
            _AppSettings = appsettings.Value;
            _IMapper = imapper;
            _IUserService = iuserService;

        }
        // được phép truy cập ko cần xác thực
        [AllowAnonymous]
        [HttpPost("authenticate")]
        // định dạng response trả về cho client
        // formbody đọc 1 loại giá trị model từ thân request
        public IActionResult Authenticate([FromBody] AuthenticateModel model)
        {
            var user = _IUserService.Authenticate(model.UserEmail, model.Password);
            if(user == null)
            {
                // tạo ra 1 phản hồi
                return BadRequest(new { message = " Sai Email hoặc Password" });
            }
            // tạo token
            var tokenhandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_AppSettings.Secret);
            // khai báo các thuộc tính trong token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    // tạo jwt id
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString() ),
                    new Claim("userId", user.UserId.ToString()),
                    new Claim("userEmail", user.UserEmail.ToString()),
                    //new Claim("avatar" , user.UserAvatar.ToString()),
                    new Claim("userName" , user.UserName.ToString()),
                    new Claim("phoneNumber" , user.PhoneNumber.ToString())
                   
                }),
                // api key SID tạo bởi Stringee
                Issuer = _AppSettings.Issuer,
              
                // ngày hết hạn token
                Expires = DateTime.UtcNow.AddDays(7),
                // Đại diện cho khóa mật mã và thuật toán bảo mật được sử dụng để tạo chữ ký số
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenhandler.CreateToken(tokenDescriptor);
            var tokenString = tokenhandler.WriteToken(token);
            // return thông tin user và token
            return Ok(new
            {
                Id = user.UserId,
                email = user.UserEmail,
                name = user.UserName,
                phone = user.PhoneNumber,
                avatar = user.UserAvatar,
                token = tokenString


            }) ;
           
            
            
        }

        // đăng ký
        /// <summary>
        ///  cho phép truy cập ko càn xác thực
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("register")]
        // định dạng response trả về cho client
        // formbody đọc 1 loại giá trị model từ thân request
        public IActionResult Register([FromBody] RegisterModel model)
        {
            // ánh xạ model đến User
            var user = _IMapper.Map<User>(model);
            try
            {
                // tạo user
                _IUserService.Creat(user, model.Password);
                return Ok();

            }
            catch(ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(Guid id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
