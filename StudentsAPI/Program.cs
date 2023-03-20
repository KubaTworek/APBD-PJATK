using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using StudentsAPI.DAO;
using StudentsAPI.Services;

namespace StudentsAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder.Services);
            var app = builder.Build();
            Configure(app);
            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<IStudentDAO, StudentDAO>();
            services.AddSingleton<IStudentService, StudentService>();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Students API", Version = "v1" });
            });
        }

        private static void Configure(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Students API v1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
