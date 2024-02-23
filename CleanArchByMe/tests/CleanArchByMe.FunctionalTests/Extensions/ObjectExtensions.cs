using System.Text;

using Newtonsoft.Json;

namespace CleanArchByMe.FunctionalTests.Extensions;

public static class ObjectExtensions
{
    public static StringContent ToJsonStringContent<T>(this T obj) =>
        new(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
}
