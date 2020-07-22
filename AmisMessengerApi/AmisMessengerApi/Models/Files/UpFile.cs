using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmisMessengerApi.Models.Files
{
    public class UpFile
    {
        public string fileName { get; set; }
        public string convId { get; set; }
        public string filePath { get; set; }
        public string fileType { get; set; }
    }
}
