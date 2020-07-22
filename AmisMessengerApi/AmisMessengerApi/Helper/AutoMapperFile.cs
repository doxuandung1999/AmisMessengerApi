﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmisMessengerApi.Entities;
using AmisMessengerApi.Models.Files;

namespace AmisMessengerApi.Helper
{
    public class AutoMapperFile : Profile
    {
        // ánh xạ đối tượng này sang đối tượng khác
        public AutoMapperFile()
        {
            // ánh xạ các thành phần của UpFile  sang File
            CreateMap<UpFile, File>();
        }
    }
}
