using MinimalApplication.Application.Api.Middlewares;
using MinimalApplication.Infrastructure.IOC;

namespace MinimalApplication.Application.Api;

public class Startup
{
    public IConfiguration Configuration { get; }
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureService(IServiceCollection services)
    {
        services.AddInfrastructure(Configuration);
        services.AddControllersWithViews();

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseStatusCodePages();
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        //app.UseMiddleware(typeof(ExceptionsHandlingMiddleware));
    }
}
