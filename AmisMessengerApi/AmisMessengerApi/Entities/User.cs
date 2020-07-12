using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AmisMessengerApi.Entities

{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string PhoneNumber { get; set; }
        public string UserAvatar { get; set; }
        /// <summary>
        // băm ra dạng số nguyên
        /// </summary>
        /// byte lưu các số nguyên có kích thước là 8 bit
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
