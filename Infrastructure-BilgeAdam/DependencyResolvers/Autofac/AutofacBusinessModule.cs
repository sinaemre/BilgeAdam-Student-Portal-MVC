using ApplicationCore_BilgeAdam.DTO_s.AccountDTO;
using ApplicationCore_BilgeAdam.DTO_s.ClassroomDTO;
using ApplicationCore_BilgeAdam.DTO_s.RoleDTO;
using ApplicationCore_BilgeAdam.DTO_s.StudentDTO;
using ApplicationCore_BilgeAdam.DTO_s.TeacherDTO;
using Autofac;
using AutoMapper;
using FluentValidation;
using Infrastructure_BilgeAdam.AutoMapper;
using Infrastructure_BilgeAdam.FluentValidator.AccountValidators;
using Infrastructure_BilgeAdam.FluentValidator.ClassroomValidators;
using Infrastructure_BilgeAdam.FluentValidator.RoleValidators;
using Infrastructure_BilgeAdam.FluentValidator.StudentValidators;
using Infrastructure_BilgeAdam.FluentValidator.TeacherValidator;
using Infrastructure_BilgeAdam.Services.Concrete;
using Infrastructure_BilgeAdam.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure_BilgeAdam.DependencyResolvers.Autofac
{
    /*
     * Yaşam Döngüleri
     * 
     * 1)Transient
     *  Uygulama içerisinde bağımlılık olarak oluşturuduğumuz ve kullandığımız nesnenin her kullanım ve her çağrıda tekrardan oluşturulmasını sağlar.
     *  
     * 2)Scoped
     * Uygulama içerisinde bağımlılık oluşturduğumuz nesnenin request sonlanana kadar aynı nesneyi kullanmasını fakat farklı bir çağrı için gelindiğinde yeni bir nesne yaratılmasını sağlar. 
     * 
     * 3)Singleton
     * Uygulama içerisinde bağımlılık oluşturduğumuz ve kullandığımız nesnenin sadece tek bir kere olşturulmasını ve aynı nesnenin uygulamanın bütün istek ve kullanımlarında kullanılmasını sağlar.
     */


    public class AutofacBusinessModule : Module
    {
        //InstancePerDependency => Transient
        //InstancePerLifetimeScope => Scoped
        protected override void Load(ContainerBuilder builder)
        {
            //Services
            builder.RegisterType<StudentRepository>().As<IStudentRepository>().InstancePerLifetimeScope();
            builder.RegisterType<TeacherRepository>().As<ITeacherRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ClassroomRepository>().As<IClassroomRepository>().InstancePerLifetimeScope();

            //Mapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new Mapping());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            builder.RegisterInstance<IMapper>(mapper);

            //Validators
            builder.RegisterType<CreateClassroomValidator>().As<IValidator<CreateClassroomDTO>>().InstancePerLifetimeScope();
            builder.RegisterType<UpdateClassroomValidator>().As<IValidator<UpdateClassroomDTO>>().InstancePerLifetimeScope();
            builder.RegisterType<CreateTeacherValidator>().As<IValidator<CreateTeacherDTO>>().InstancePerLifetimeScope();
            builder.RegisterType<UpdateTeacherValidator>().As<IValidator<UpdateTeacherDTO>>().InstancePerLifetimeScope();
            builder.RegisterType<CreateStudentValidator>().As<IValidator<CreateStudentDTO>>().InstancePerLifetimeScope();
            builder.RegisterType<UpdateStudentValidator>().As<IValidator<UpdateStudentDTO>>().InstancePerLifetimeScope();
            builder.RegisterType<RegisterValidator>().As<IValidator<RegisterDTO>>().InstancePerLifetimeScope();
            builder.RegisterType<LoginValidator>().As<IValidator<LoginDTO>>().InstancePerLifetimeScope();
            builder.RegisterType<EditValidator>().As<IValidator<EditUserDTO>>().InstancePerLifetimeScope();
            builder.RegisterType<CreateRoleValidator>().As<IValidator<CreateRoleDTO>>().InstancePerLifetimeScope();
        }
    }
}
