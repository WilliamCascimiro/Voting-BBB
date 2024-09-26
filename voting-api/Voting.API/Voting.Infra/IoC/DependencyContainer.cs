using Voting.Application.Services;
using Voting.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Voting.Infra.Queue;
using Voting.API.Voting.Infra.DataBase.Context;
using Voting.API.Voting.Infra.DataBase.Repositories;

namespace Voting.Infra.IoC
{
    public static class DependencyContainer
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDependencyInjectionRepository();
            services.AddDependencyInjectionService();
            services.AddAutoMapperConfig();
            services.AddContext(configuration);

            return services;
        }

        private static IServiceCollection AddDependencyInjectionRepository(this IServiceCollection services)
        {
            services.AddScoped<BBBDbContext>();
            services.AddScoped<IVoteRepository, VoteRepository>();

            services.AddSingleton<IRabbitMqProducer, Producer>();

            return services;
        }

        private static IServiceCollection AddDependencyInjectionService(this IServiceCollection services)
        {
            services.AddScoped<IVoteService, VoteService>();
            return services;
        }

        private static IServiceCollection AddAutoMapperConfig(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            return services;
        }

        private static IServiceCollection AddContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BBBDbContext>(options => 
                options.UseSqlServer(
                    configuration.GetConnectionString("BBBDb"))
            );

            return services;
        }
    }
}
