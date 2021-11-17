using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmisMessengerApi.Entities;
using AmisMessengerApi.Models.Users;
using AutoMapper;

namespace AmisMessengerApi.Helper
{
    public class AutoMapperUser : Profile
    {
        // ánh xạ đối tượng này sang đối tượng khác
        public AutoMapperUser()
        {
            // ánh xạ các thành phần của Usersystem sang UserModel
            CreateMap<User, UserModel>();
            CreateMap<RegisterModel, User>();
            CreateMap<EditUserModel, User>();
        }
    }
}
