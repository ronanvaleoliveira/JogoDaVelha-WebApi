using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace JogoDaVelha.Api.Swagger
{
    public static class SwaggerConfiguration
    {
        public static void Config(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Jogo da Velha",
                    Version = "v1",
                    Description = "API com ASP.Net Core 3.1",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "Ronan Vale de Oliveira",
                        Email = "ronanvaleoliveira@gmail.com",
                        Url = new Uri("https://www.linkedin.com/in/ronanvale/")
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
        }
    }
}
