using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static TService GetRequiredScopedService<TService>(this IServiceCollection services)
        {
            return services.CreateScopedServiceProvider().GetRequiredService<TService>();
        }
        public static object GetRequiredScopedService(this IServiceCollection services, Type type)
        {
            return services.CreateScopedServiceProvider().GetRequiredService(type);
        }
        public static IServiceProvider CreateScopedServiceProvider(this IServiceCollection services)
        {
            return services.BuildServiceProvider().CreateScope().ServiceProvider;
        }
    }
}