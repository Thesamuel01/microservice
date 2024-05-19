using ProductAPI.Domains;
using System.ComponentModel;
using System.Data;
using System.Reflection;

namespace ProductAPI.Data.Helpers;

public static class ProductMapper
{
    public static IEnumerable<T> Map<T>(DataTable table) where T : Product, new()
    {
        var result = new List<T>();

        foreach (DataRow row in table.Rows)
        {
            var product = new T();

            foreach (DataColumn col in table.Columns)
            {
                var columnName = col.ColumnName;
                var propName = GetPropertyName<T>(columnName);

                if (propName != null)
                {
                    var propInfo = product
                        .GetType()
                        .GetProperties()
                        .Where(p => p.GetCustomAttribute<DisplayNameAttribute>() is not null)
                        .First(p => p.GetCustomAttribute<DisplayNameAttribute>()!.DisplayName == columnName);

                    if (propInfo != null && row[columnName] != DBNull.Value)
                    {
                        propInfo.SetValue(product, Convert.ChangeType(row[columnName], propInfo.PropertyType));
                    }
                }
            }

            result.Add(product);
        }

        return result;
    }

    private static string GetPropertyName<T>(string columnName)
    {
        var properties = typeof(T).GetProperties();
        foreach (var prop in properties)
        {
            var displayNameAttr = (DisplayNameAttribute)Attribute.GetCustomAttribute(prop, typeof(DisplayNameAttribute));
            if (displayNameAttr != null && displayNameAttr.DisplayName == columnName)
            {
                return displayNameAttr.DisplayName;
            }
        }
        return null;
    }
}
