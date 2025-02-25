using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FrameWork;

public static class EnumExtensionMethods
{
    public static string GetEnumDisplayName(this Enum enumType)
    {
        var member = enumType.GetType().GetMember(enumType.ToString()).FirstOrDefault();
        if (member != null)
        {
            var displayAttribute = member.GetCustomAttribute<DisplayAttribute>();
            if (displayAttribute != null)
            {
                return displayAttribute.Name;
            }
        }
        return enumType.ToString();
    }
}
