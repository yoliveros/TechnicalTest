namespace Infrastructure;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var conn = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<Data.ApplicationDbContext>(options =>
            options.UseSqlServer(conn));

        services.AddTransient<IDbConnection>(_ => new SqlConnection(conn));

        return services;
    }
}
