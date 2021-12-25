using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AmisMessengerApi.Entities

{
    public class Profiles
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int PostId { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string JobPosition { get; set; }
        public string NameFileCV { get; set; }
        public string LinkFileCV { get; set; }
        public DateTime BirthDay { get; set; }

    }
}
