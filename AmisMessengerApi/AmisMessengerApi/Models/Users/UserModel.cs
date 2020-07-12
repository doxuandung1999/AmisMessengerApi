using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmisMessengerApi.Models.Users
{
    public class UserModel
    // xác định dữ liệu được trả về cho các yêu cầu GET , và ngăn chặn trả về một số thuộc tính : getAll() , getById
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string PhoneNumber { get; set; }
        public string UserAvatar { get; set; }
        


    }
}
