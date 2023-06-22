using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Blog.WebUI.IntegrationTests.Helpers
{
    public static class AttributeHelper
    {
        public static string GetBindProperty<T>(this T obj, string propertyName)
        {
            var prop = typeof(T).GetProperty(propertyName).GetCustomAttribute<BindPropertyAttribute>();

            return prop.Name;
        }
    }
}
