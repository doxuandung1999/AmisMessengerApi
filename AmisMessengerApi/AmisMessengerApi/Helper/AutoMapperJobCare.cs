using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmisMessengerApi.Entities;
using AmisMessengerApi.Models.Files;
using AutoMapper;

namespace AmisMessengerApi.Helper
{
    public class AutoMapperJobCare : Profile
    {
        // ánh xạ đối tượng này sang đối tượng khác
        public AutoMapperJobCare()
        {
            // ánh xạ các thành phần của UpFile  sang File
            CreateMap<UpJobCare, JobCare>();
        }
    }
}
