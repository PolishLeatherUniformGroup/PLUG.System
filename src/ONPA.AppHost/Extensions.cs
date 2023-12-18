using AutoMapper.Configuration.Conventions;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage(Justification = "Configuration")]
internal static class Extensions
{
    public static IResourceBuilder<TDestination> WithEnvironmentForServiceBinding<TDestination, TSource>(
        this IResourceBuilder<TDestination> builder,
        string name,
        IResourceBuilder<TSource> source,
        string bindingName = "http")
        where TDestination : IResourceWithEnvironment
        where TSource : IResourceWithBindings
    {
        return builder.WithEnvironment(context =>
        {
            if (context.PublisherName == "manifest")
            {
                context.EnvironmentVariables[name] = source.GetEndpoint(bindingName).UriString;
                return;
            }

            if (!source.Resource.TryGetServiceBindings(out var bindings))
            {
                return;
            }

            var binding = bindings.FirstOrDefault(b => b.Name == bindingName);

            if (binding?.Port == null)
            {
                return;
            }

            context.EnvironmentVariables[name] = $"{binding.UriScheme}://localhost:{binding.Port}";
        });
    }

    public static IResourceBuilder<TDestination> WithHttpClient<TDestination, TSource>(
        this IResourceBuilder<TDestination> builder,
        string clientName,
        IResourceBuilder<TSource> source)
        where TDestination : IResourceWithEnvironment
        where TSource : IResourceWithBindings
    {
        return builder.WithEnvironment(context =>
        {
            var nameOfClient = $"Services__{clientName}__Uri";
            if (!source.Resource.TryGetServiceBindings(out var bindings))
            {
                return;
            }
            var binding = bindings.FirstOrDefault(x => x.Name == source.Resource.Name);
            if (binding?.Port == null)
            {
                return;
            }
            context.EnvironmentVariables[nameOfClient] = $"{binding.UriScheme}://localhost:{binding.Port}";
        });
    }

}