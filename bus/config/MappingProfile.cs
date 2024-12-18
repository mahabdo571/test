using AutoMapper;
using DBA.Entities;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS.config
{
    internal class MappingProfile :Profile
    {
        public MappingProfile()

        {
            CreateMap<Users, RegisterDTO>().ReverseMap();
            CreateMap<Users, LoginDTO>().ReverseMap();
        }
    }
}
