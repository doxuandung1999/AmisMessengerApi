using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AmisMessengerApi.Entities

{
    public class Company
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CompanyId { get; set; }
        public Guid UserId { get; set; }
        public string Address { get; set; }
        public string CompanyName { get; set; }
        public string CompanyDescriber { get; set; }
        public string CompanyAvatar { get; set; }
        public string CompanyBanner { get; set; }
        
    }
}
