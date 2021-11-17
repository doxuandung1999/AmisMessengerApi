using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmisMessengerApi.Models.Files
{
    public class UpCompany
    {
        public Guid UserId { get; set; }
        public string Address { get; set; }
        public string CompanyName { get; set; }
        public string CompanyDescriber { get; set; }
        public string CompanyAvatar { get; set; }
        public string CompanyBanner { get; set; }

    }
}
