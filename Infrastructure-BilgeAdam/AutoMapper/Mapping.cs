using ApplicationCore_BilgeAdam.DTO_s.ClassroomDTO;
using ApplicationCore_BilgeAdam.Entities.Concrete;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure_BilgeAdam.AutoMapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Classroom, CreateClassroomDTO>().ReverseMap();
        }
    }
}
