using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace AdvanceCRM
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            var assembly = typeof(Startup).Assembly;
            var repositoryTypes = assembly.GetTypes()
                .Where(t => !t.IsAbstract && t.IsClass && t.Name.EndsWith("Repository", StringComparison.Ordinal));

            foreach (var type in repositoryTypes)
            {
                services.AddTransient(type);
            }

            return services;
        }
    }
}
