using MinimalApplication.Domain.Entities.Category;
using MinimalApplication.Domain.Mappings;
using MinimalApplication.Infrastructure.Data;
using MinimalApplication.Infrastructure.Repositories;
using MinimalApplication.UseCases.Category;
using MinimalApplication.UseCases.Category.Interface;

namespace MinimalApplication.Infrastructure.IOC;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        //services.AddDbContext<ApplicationDbContext>(options =>
        //                    options.UseSqlite(configuration["ConnectionStrings:Database"])
        //);

        services.AddSingleton(new DatabaseConfig { Name = configuration["ConnectionStrings:Database"] });

        services.AddScoped<DbSession>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        services.AddScoped<ICategoryUC, CategoryUC>();

        
        services.AddAutoMapper(
            typeof(DomainToDTOMapping),
            typeof(ModelToEntityMapping)
            );

        return services;
    }
}
