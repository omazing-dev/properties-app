using PropertyApp.Infraestructure.Configurations;
using PropertyApp.Infraestructure.Context;

namespace PropertyApp.Web.Configurations
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            var mongoConfig = new MongoDBConfig();
            configuration.GetSection("MongoDb").Bind(mongoConfig);

            services.AddSingleton(mongoConfig);
            services.AddSingleton<MongoDbContext>(sp =>
                 new MongoDbContext(mongoConfig.ConnectionString, mongoConfig.DatabaseName));


            return services;
        }
    }
}
