using DAL.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DAL.Extensions;
public static class AzureServiceExtensions
{
    public static void AddTableStorageServices(this IServiceCollection services,
                                                   Action<AzureTableClientOptions> setupOptions,
                                                   ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
    {
        services.Configure<AzureTableClientOptions>(setupOptions);
        switch (serviceLifetime)
        {
            case ServiceLifetime.Singleton:
                services.AddSingleton<IAzureTableClient>(provider =>
                {
                    var options = provider.GetRequiredService<IOptions<AzureTableClientOptions>>();
                    return new AzureTableClient(options.Value);
                });
                break;
            case ServiceLifetime.Scoped:
                services.AddScoped<IAzureTableClient>(provider =>
                {
                    var options = provider.GetRequiredService<IOptions<AzureTableClientOptions>>();
                    return new AzureTableClient(options.Value);
                });
                break;
            case ServiceLifetime.Transient:
                services.AddTransient<IAzureTableClient>(provider =>
                {
                    var options = provider.GetRequiredService<IOptions<AzureTableClientOptions>>();
                    return new AzureTableClient(options.Value);
                });
                break;
        }
    }
    public static async Task<IList<TElement>> ToListAsync<TElement>(this IAsyncEnumerable<TElement> elements)
            where TElement : class
    {
        var elementsList = new List<TElement>();
        await foreach (var element in elements.ConfigureAwait(false))
            elementsList.Add(element);

        return elementsList;
    }
}
