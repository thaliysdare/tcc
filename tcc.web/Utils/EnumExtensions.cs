using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace tcc.web.Utils
{
    public static class EnumExtensions
    {
        public static List<SelectListItem> GetEnumSelectList<T>()
        {
            var lista = (Enum.GetValues(typeof(T))
                             .OfType<Enum>()
                             .Select(x => new SelectListItem()
                             {
                                 Text = x.GetDescription(),
                                 Value = (Convert.ToInt32(x)).ToString()
                             })).ToList();
            return lista;
        }

        public static string GetName(this object value)
        {
            return Enum.GetName(value.GetType(), value);
        }

        private static string GetDescricao(this object value)
        {
            var fieldInfo = value.GetType().GetField(value.GetName());
            var descriptionAttribute = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
            return descriptionAttribute == null ? string.Empty : descriptionAttribute.Description;
        }

        public static string GetDescription(this object value)
        {
            try
            {
                var description = value.GetType().GetCustomAttribute<DescriptionAttribute>();
                string valueDescription = !string.IsNullOrWhiteSpace(GetDescricao(value).Trim()) ? GetDescricao(value) : string.Empty;
                return description != null ? description.Description : valueDescription;
            }
            catch
            {
                return string.Empty;
            }
        }

    }
}
