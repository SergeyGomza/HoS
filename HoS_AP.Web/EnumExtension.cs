using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoS_AP.Web
{
    using System.ComponentModel.DataAnnotations;
    using System.Reflection;
    using System.Web.Mvc;

    public static class EnumExtension
    {
        public static MvcHtmlString GetEnumName(this HtmlHelper htmlHelper, Enum enumValue)
        {
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            var descriptionAttributes = fieldInfo.GetCustomAttributes(
                typeof(DisplayAttribute), false) as DisplayAttribute[];

            if (descriptionAttributes != null)
            {
                if (descriptionAttributes[0].ResourceType != null)
                    return new MvcHtmlString(LookupResource(descriptionAttributes[0].ResourceType, descriptionAttributes[0].Name));
            }

            if (descriptionAttributes == null) return new MvcHtmlString(string.Empty);

            return new MvcHtmlString(descriptionAttributes.Length > 0 ? descriptionAttributes[0].Name : enumValue.ToString());
        }

        private static string LookupResource(Type resourceManagerProvider, string resourceKey)
        {
            foreach (PropertyInfo staticProperty in resourceManagerProvider.GetProperties(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (staticProperty.PropertyType == typeof(System.Resources.ResourceManager))
                {
                    System.Resources.ResourceManager resourceManager = (System.Resources.ResourceManager)staticProperty.GetValue(null, null);
                    return resourceManager.GetString(resourceKey);
                }
            }

            return resourceKey; // Fallback with the key name
        }
    }
}