using AmisMessengerApi.Entities;
using AmisMessengerApi.Helper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmisMessengerApi.Services
{
    public interface IUserService
    {
        // xác thực tài khoản
        User Authenticate(string email, string password);
        IEnumerable<User> GetAll();
        User Creat(User user, string password);
        //User GetById(Guid id);
         Task<ActionResult<User>> GetUser(Guid id);


    }
    public class UserService : IUserService
    {
        private DataContext _context;
        public UserService( DataContext context)
        {
            _context = context;
        }
        // xác thực người dùng
        public User Authenticate(string email , string password)
        {
            // kiểm tra email hoặc password có rỗng hay ko
            if(string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return null;
            }
            var user = _context.Users.SingleOrDefault(x => x.UserEmail == email);
            // kiểm tra email có tồn tại ko
            if(user == null)
            {
                return null;
            }
            // kiểm tra password đúng ko
            if(!checkPassword(password,user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            return user;
        }
        // tạo user
        public User Creat(User user , string password)
        {
            if (string.IsNullOrWhiteSpace(password)) throw new ApplicationException("nhập lại password");
            // kiểm tra User đã tòn tại chưa
            if (_context.Users.Any(x => x.UserEmail == user.UserEmail)) throw new ApplicationException("Email " +user.UserEmail + " đã tồn tại");
            // tạo password nếu các đk thỏa mãn
            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                // lấy passwordhash và passwordSalt từ hàm creatUser ở bên dưới
                CreatPassword(password, out passwordHash, out passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.UserId = new Guid();

                //var _user = new User
                //{
                //    UserId = new Guid(),
                //    UserEmail = user.UserEmail,
                //    UserName = user.UserName,
                //    PhoneNumber = user.PhoneNumber,
                //    UserAvatar = user.UserAvatar,
                //    PasswordHash = user.PasswordHash,
                //    PasswordSalt = user.PasswordSalt,
                //};

                // add user vào database
                _context.Users.Add(user);
                _context.SaveChanges();

            }
            return user;

        }
        // lấy user theo id
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return null;
            }

            return user;
        }
        // lấy tất cả user
        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        // tạo và băm password
        // out : truyền tham chiếu
        private static void CreatPassword(string password , out byte[] passwordHash , out byte[] passwordSalt)
        {
            // ngoại lệ khi password null
            if (password == null) throw new ArgumentNullException("password");
            // ngoại lệ liên quan đến đối số
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException(" không được null hoặc có khoảng trắng ", "password");
            // tạo mảng băm
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                // salt là key đc thêm vào
                passwordSalt = hmac.Key;
                // tính 1 hàm băm , đầu vào là 1 mảng byte trả về 1 hàm băm dưới dạng 1 mảng byte
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        // kiểm tra password
        private static bool checkPassword(string password , byte[] passwordHash , byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException(password);
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentNullException("không được null hoặc có khoảng trắng","password");
            // tạo mảng băm với key là passwordSalt
            using (var hma = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                // tạo mảng băm với input là password và key là passwordsalt
               var  checkPasswordHash = hma.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                // so sánh mảng vừa được băm ra có giống với mảng được băm ra lúc adđ user không
                for(int i = 0; i < checkPasswordHash.Length; i++)
                {
                    if(checkPasswordHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }

            }
            return true;
        }

    }
}
