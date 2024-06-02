using Elmah.Io.Extensions.Logging;
using HealthChecks.SqlServer;

namespace DevIO.Api.Configuration
{
    public static class LoggerConfig
    {
        public static IServiceCollection AddLoggingConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddElmahIo(o =>
            {
                o.ApiKey = "ab06727caf11496dbbac1331cbc21186";
                o.LogId = new Guid("14218bcb-b663-4a9b-be6f-157e306350dc");
            });

            services.AddHealthChecks()
                .AddElmahIoPublisher(options =>
                {
                    options.ApiKey = "ab06727caf11496dbbac1331cbc21186";
                    options.LogId = new Guid("14218bcb-b663-4a9b-be6f-157e306350dc");
                    options.HeartbeatId = "API Fornecedores";

                })
                //.AddCheck("Produtos", new SqlServerHealthCheck(configuration.GetConnectionString("DefaultConnection"))
                .AddSqlServer(configuration.GetConnectionString("DefaultConnection"), name: "BancoSQL");

            return services;
        }

        public static IApplicationBuilder UseLoggingConfiguration(this IApplicationBuilder app)
        {
            app.UseElmahIo();

            return app;
        }
    }
}
// Configure loglevel
//services.AddLogging(builder =>
//{
//    builder.AddElmahIo(o =>
//    {
//        o.ApiKey = "ab06727caf11496dbbac1331cbc21186";
//        o.LogId = new Guid("14218bcb-b663-4a9b-be6f-157e306350dc");
//    });
//    // nivel de logging
//    builder.AddFilter<ElmahIoLoggerProvider>(null, LogLevel.Warning); //LogLevel.Information
//});

