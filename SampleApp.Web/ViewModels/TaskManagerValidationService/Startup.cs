using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManagerValidationService.Core.Repositories;
using TaskManagerValidationService.Core.Services;
using TaskManagerValidationService.Core.Validators;
using TaskManagerValidationService.Dal;
using TaskManagerValidationService.Repositories;
using TaskManagerValidationService.Services;
using TaskManagerValidationService.Validators;

namespace TaskManagerValidationService
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            var connection = _configuration
                .GetConnectionString("DefaultConnection");

            services
                .AddDbContext<TaskManagerValidationServiceContext>
                (options => options.UseSqlServer(connection));

            services
                .AddScoped<ITaskRepository, TaskRepository>();
            services
                .AddScoped<ITaskNameValidator, TaskNameValidator>();
            services
                .AddScoped<ITaskManagerService, TaskManagerService>();

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
