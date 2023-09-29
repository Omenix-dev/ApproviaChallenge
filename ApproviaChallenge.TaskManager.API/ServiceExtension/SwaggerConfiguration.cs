using Microsoft.OpenApi.Models;
using System.Reflection;

namespace ApproviaChallenge.TaskManager.API.ServiceExtension
{
    public static class SwaggerConfiguration
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Approvia challenge",
                    Version = "v1",
                    Description = "Task manager for scheduling event",
                    Contact = new OpenApiContact
                    {
                        Name = "Approovia",
                        Email = "info@approovia.com "
                    },
                    License = new OpenApiLicense
                    {
                        Name = "TaskManager.API",
                        Url = new Uri("https://approovia.task/licenses/MIT")
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }
    }
}
