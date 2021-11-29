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
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using AmisMessengerApi.Models.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using AmisMessengerApi.Models.Files;

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
        private ICompanyService _ICompanyService;

        public UsersController(DataContext context , IMapper imapper , IUserService iuserService, ICompanyService companyService , IOptions<AppSettings> appsettings)
        {
            _context = context;
            _AppSettings = appsettings.Value;
            _IMapper = imapper;
            _IUserService = iuserService;
            _ICompanyService = companyService;

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
            // lấy thông tin công ty
            var company = _ICompanyService.GetCompany(user.UserId).Result;

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
                role = user.Role,
                company = company
                //token = tokenString


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
            // ánh xạ model đến Usersystem
            var user = _IMapper.Map<User>(model);
            try
            {
                // tạo user
                _IUserService.Creat(user, model.Password);

                // tạo cty cho người tuyển dụng mới đăng ký
                if (user.Role == 1)
                {
                    
                    UpCompany company = new UpCompany();
                    company.UserId = user.UserId;
                    // ánh xạ model đến Usersystem
                    var companyObj = _IMapper.Map<Company>(company);
                    _ICompanyService.creatCompany(companyObj);
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
                    new Claim("userName" , user.UserName.ToString())

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
                return Ok(new
                {
                   
                    token = tokenString


                });

                //return Ok();

            }
            catch(ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/Usersystem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Usersystem.ToListAsync();
        }


        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var users = _IUserService.GetAll();
            var model = _IMapper.Map<IList<UserModel>>(users);
            return Ok(model);
        }


        // GET: api/Usersystem/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            var user = await _context.Usersystem.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        //edit usser
        [HttpPut("updateProfile")]
        public async Task<IActionResult> PutUserAsync([FromBody] EditUserModel model)
        {
            var user = _IMapper.Map<User>(model);
            try
            {
                await _IUserService.EditUser(model);
                //var token = _userService.GenerateJwtStringee(_appSettings.IsUser, _appSettings.Secret, user.Id.ToString(), user.Email, user.ImageUrl, user.FullName);
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
                    new Claim("userName" , user.UserName.ToString()),

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

                return Ok(new
                {
                    id = user.UserId,
                    name = user.UserName,
                    email = user.UserEmail,
                    token = tokenString
                });
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/Usersystem/5
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

        // POST: api/Usersystem
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Usersystem.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/Usersystem/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(Guid id)
        {
            var user = await _context.Usersystem.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Usersystem.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(Guid id)
        {
            return _context.Usersystem.Any(e => e.UserId == id);
        }
    }
}
