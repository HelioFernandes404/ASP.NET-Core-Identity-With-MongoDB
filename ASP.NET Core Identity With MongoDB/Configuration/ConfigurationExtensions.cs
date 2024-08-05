using AspNetCoreIdentityWithMongodb.Data;
using AspNetCoreIdentityWithMongodb.Models;

namespace AspNetCoreIdentityWithMongodb.Configuration;

public static class ConfigurationExtensions
{
    public static void ConfigureIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        var mongoDbSettings = configuration.GetSection(nameof(MongoDbConfig)).Get<MongoDbConfig>();
        if (mongoDbSettings == null)
        {
            throw new InvalidOperationException("MongoDbSettings cannot be null");
        }

        services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 2;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
            options.Password.RequiredUniqueChars = 0;
        })
       .AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>(mongoDbSettings.ConnectionString, mongoDbSettings.Name);
    }
}