using ApplicationCore_BilgeAdam.DTO_s.ClassroomDTO;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure_BilgeAdam.AutoMapper;
using Infrastructure_BilgeAdam.Context;
using Infrastructure_BilgeAdam.DependencyResolvers.Autofac;
using Infrastructure_BilgeAdam.FluentValidator.ClassroomValidators;
using Infrastructure_BilgeAdam.FluentValidator.TeacherValidator;
using Microsoft.EntityFrameworkCore;

namespace WEB_BilgeAdam
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

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
                  .RegisterValidatorsFromAssemblyContaining<CreateTeacherValidator>();
            });

            var connectionString = builder.Configuration.GetConnectionString("PostgresSqlConnection");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}