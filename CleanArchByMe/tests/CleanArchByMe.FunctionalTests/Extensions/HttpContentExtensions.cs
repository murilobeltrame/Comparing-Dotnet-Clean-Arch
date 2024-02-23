using Newtonsoft.Json;

namespace CleanArchByMe.FunctionalTests.Extensions;

public static class HttpContentExtensions
{
    public static async Task<T?> Deserialize<T>(this HttpContent content) where T : class => 
        JsonConvert.DeserializeObject<T>(await content.ReadAsStringAsync());
}
