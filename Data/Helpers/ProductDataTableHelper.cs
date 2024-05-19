using ProductAPI.Domains;
using System.ComponentModel;
using System.Reflection;

namespace ProductAPI.Data.Helpers;

public static class ProductDataTableHelper
{
    public static IEnumerable<string> GetColumnNames(this Product entity)
    {
        var props = entity
            .GetType()
            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Where(p => p.GetCustomAttribute<DisplayNameAttribute>() is not null)
            .Select(p => p.GetCustomAttribute<DisplayNameAttribute>());

        foreach (var property in props)
        {
            yield return property!.DisplayName;
        }
    }
}
