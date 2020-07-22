using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmisMessengerApi.Entities
{
    public class File
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid fileId { get; set; }
        public string convId { get; set; }
        public string fileName { get; set; }
        public string filePath { get; set; }
        public string fileType { get; set; }
       
    }
}
