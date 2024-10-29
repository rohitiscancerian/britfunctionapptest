using DataAccessLayer;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Reflection;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureAppConfiguration((context, builder) =>
     {
         builder.SetBasePath(context.HostingEnvironment.ContentRootPath)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: false)
                .AddEnvironmentVariables();

     })
    .ConfigureServices((context, services) =>
    {
        var configuration = context.Configuration;
        services.AddApplicationInsightsTelemetryWorkerService();

        services.ConfigureFunctionsApplicationInsights();
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
        services.Configure<ConnectionStrings>(configuration.GetSection("ConnectionStrings"));
        var serviceProvider = services.BuildServiceProvider();
        DataLayerDIConfig.ConnectionString = serviceProvider.GetService<IOptionsSnapshot<ConnectionStrings>>();
        services.AddAPIDataService();
    })
    .Build();

host.Run();
