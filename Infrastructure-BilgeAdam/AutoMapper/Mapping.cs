using ApplicationCore_BilgeAdam.DTO_s.AccountDTO;
using ApplicationCore_BilgeAdam.DTO_s.ClassroomDTO;
using ApplicationCore_BilgeAdam.DTO_s.StudentDTO;
using ApplicationCore_BilgeAdam.DTO_s.TeacherDTO;
using ApplicationCore_BilgeAdam.Entities.Concrete;
using ApplicationCore_BilgeAdam.Entities.UserEntities.Concrete;
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
            CreateMap<Classroom, UpdateClassroomDTO>().ReverseMap();
            CreateMap<Classroom, ClassroomStudentDTO>().ReverseMap();
            

            CreateMap<Teacher, CreateTeacherDTO>().ReverseMap();
            CreateMap<Teacher, UpdateTeacherDTO>().ReverseMap();

            CreateMap<Student, CreateStudentDTO>().ReverseMap();
            CreateMap<Student, UpdateStudentDTO>().ReverseMap();
        }
    }
}
