using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmisMessengerApi.Models.Files
{
    public class UpProfiles
    {
        public int PostId { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string JobPosition { get; set; }
        public string NameFileCV { get; set; }
        public string LinkFileCV { get; set; }
        public DateTime BirthDay { get; set; }

    }
}
