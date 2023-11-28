using ApplicationCore_BilgeAdam.DTO_s.ClassroomDTO;
using ApplicationCore_BilgeAdam.Entities.UserEntities.Concrete;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure_BilgeAdam.AutoMapper;
using Infrastructure_BilgeAdam.Context;
using Infrastructure_BilgeAdam.Context.IdentityContext;
using Infrastructure_BilgeAdam.DependencyResolvers.Autofac;
using Infrastructure_BilgeAdam.FluentValidator.AccountValidators;
using Infrastructure_BilgeAdam.FluentValidator.ClassroomValidators;
using Infrastructure_BilgeAdam.FluentValidator.RoleValidators;
using Infrastructure_BilgeAdam.FluentValidator.StudentValidators;
using Infrastructure_BilgeAdam.FluentValidator.TeacherValidator;
using Infrastructure_BilgeAdam.SeedData;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace WEB_BilgeAdam
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            // Add services to the container.

            builder.Host
                    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                    .ConfigureContainer<ContainerBuilder>(builder =>
            {
                builder.RegisterModule(new AutofacBusinessModule());
            });

            builder.Services.AddControllersWithViews().AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssemblyContaining<CreateClassroomValidator>()
                  .RegisterValidatorsFromAssemblyContaining<UpdateClassroomValidator>()
                  .RegisterValidatorsFromAssemblyContaining<CreateTeacherValidator>()
                  .RegisterValidatorsFromAssemblyContaining<UpdateTeacherValidator>()
                  .RegisterValidatorsFromAssemblyContaining<CreateStudentValidator>()
                  .RegisterValidatorsFromAssemblyContaining<UpdateStudentValidator>()
                  .RegisterValidatorsFromAssemblyContaining<RegisterValidator>()
                  .RegisterValidatorsFromAssemblyContaining<LoginValidator>()
                  .RegisterValidatorsFromAssemblyContaining<EditValidator>()
                  .RegisterValidatorsFromAssemblyContaining<CreateRoleValidator>();
            });

            var connectionString = builder.Configuration.GetConnectionString("PostgresSqlConnection");
            var connectionStringIdentity = builder.Configuration.GetConnectionString("PostgresSqlIdentityConnection");


            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseNpgsql(connectionStringIdentity);
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            builder.Services.AddIdentity<AppUser, IdentityRole>(x =>
            {
                x.SignIn.RequireConfirmedPhoneNumber = false;
                x.SignIn.RequireConfirmedAccount = false;
                x.SignIn.RequireConfirmedEmail = false;
                x.User.RequireUniqueEmail = true;
                x.Password.RequiredLength = 1;
                x.Password.RequiredUniqueChars = 0;
                x.Password.RequireUppercase = false;
                x.Password.RequireNonAlphanumeric = false;
                x.Password.RequireLowercase = false;
            })
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}");



            app.Run();
        }
    }
}