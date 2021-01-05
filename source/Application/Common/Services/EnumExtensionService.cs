using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Common.Services
{
    public static class EnumExtensionService
    {
        public static string ToName(this Enum value)
        {
            var attribute = value.GetAttribute<DisplayAttribute>();
            return attribute == null ? value.ToString() : attribute.Name;
        }

        private static T GetAttribute<T>(this Enum value)
            where T : Attribute
        {
            var type = value.GetType();
            var memberInfo = type.GetMember(value.ToString());
            var attributes = memberInfo.Length > 0 ? memberInfo[0].GetCustomAttributes(typeof(T), false) : null;
            return attributes != null ? attributes.Length > 0 ? (T)attributes[0] : null : null;
        }
    }
}
