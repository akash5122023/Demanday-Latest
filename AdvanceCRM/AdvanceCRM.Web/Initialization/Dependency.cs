namespace Serenity.Extensions.DependencyInjection
{
    using Microsoft.Extensions.DependencyInjection;
    using System;

    /// <summary>
    /// Provides access to the application's <see cref="IServiceProvider"/> for resolving dependencies.
    /// </summary>
    public static class Dependency
    {
        /// <summary>
        /// Gets the current service provider.
        /// </summary>
        public static IServiceProvider Provider { get; private set; }

        /// <summary>
        /// Sets the service provider. Should be called during application initialization.
        /// </summary>
        public static void SetProvider(IServiceProvider provider)
        {
            Provider = provider;
        }

        /// <summary>
        /// Resolves a service of type <typeparamref name="T"/> from the current provider.
        /// </summary>
        public static T Resolve<T>() where T : notnull
        {
            if (Provider == null)
                throw new InvalidOperationException("Service provider is not initialized.");

            return Provider.GetRequiredService<T>();
        }

        /// <summary>
        /// Attempts to resolve a service of type <typeparamref name="T"/> from the current provider.
        /// Returns <c>null</c> when the provider is not set or the service is not registered.
        /// </summary>
        public static T? TryResolve<T>() where T : class
        {
            if (Provider == null)
                return default;

            return Provider.GetService<T>();
        }
    }
}
