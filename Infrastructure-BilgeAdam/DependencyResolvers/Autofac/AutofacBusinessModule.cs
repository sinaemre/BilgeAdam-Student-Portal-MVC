using Autofac;
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
            builder.RegisterType<StudentRepository>().As<IStudentRepository>().SingleInstance();
            builder.RegisterType<TeacherRepository>().As<ITeacherRepository>().SingleInstance();
            builder.RegisterType<ClassroomRepository>().As<IClassroomRepository>().SingleInstance();
        }
    }
}
