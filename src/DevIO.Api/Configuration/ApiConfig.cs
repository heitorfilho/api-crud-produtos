using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace DevIO.Api.Configuration
{
    public static class ApiConfig
    {
        public static IServiceCollection WebApiConfig(this IServiceCollection services)
        {
            // resolve o problema de A possible object cycle was detected
            //services.AddControllers().AddJsonOptions(x =>
            //   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

            services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
            {
                options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

            //services.AddControllersWithViews()
            //    .AddJsonOptions(options =>
            //    {
            //        options.JsonSerializerOptions.IgnoreNullValues = true;
            //        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
            //    });


            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;

            });

            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(options => 
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            //libera acesso para qualquer fonte - angular
            // aplicado pelo browser e configurado default pelo aspnet para negar qualquer requisição
            services.AddCors(options =>
            {
                //options.AddDefaultPolicy(builder =>
                //{
                //    builder.AllowAnyOrigin()
                //           .AllowAnyMethod()
                //           .AllowAnyHeader();
                //});

                //options.AddDefaultPolicy(builder =>
                //{
                //    builder.AllowAnyOrigin()
                //           WithOrigins("http://localhost:4200")
                //           .AllowAnyMethod()
                //           .AllowAnyHeader();
                //           .AllowCredentials(); // caso envie cookies com as solicitações
                //});

                options.AddPolicy("Development",
                    builder =>
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());

                //options.AddPolicy("Development",
                //    builder => builder.AllowAnyOrigin()
                //        .AllowAnyMethod()
                //        .AllowAnyHeader()
                //        .SetIsOriginAllowedToAllowWildcardSubdomains()
                //        .AllowCredentials());
            });

            return services;
        }

        public static IApplicationBuilder UseMvcConfiguration(this IApplicationBuilder app)
        {
            app.UseHttpsRedirection();
            app.UseCors("Development");
            app.UseMvc();

            return app;
        }
    }
}
