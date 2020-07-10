using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmisMessengerApi.Entities

{
    public class User
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string PhoneNumber { get; set; }
        public string UserAvatar { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
