using System;
using System.ComponentModel;
using System.Reflection;

namespace helpClass.Utils
{
    /// <summary>
    /// 枚举转描述
    /// </summary>
    public static class EnumExtension
    {
        public static string GetEnumDesc(this Type e, int? value)
        {
            FieldInfo[] fields = e.GetFields();
            for (int i = 1, count = fields.Length; i < count; i++)
            {
                if ((int)Enum.Parse(e, fields[i].Name) == value)
                {
                    DescriptionAttribute[] EnumAttributes = (DescriptionAttribute[])fields[i].GetCustomAttributes(typeof(DescriptionAttribute), false);
                    if (EnumAttributes.Length > 0)
                    {
                        return EnumAttributes[0].Description;
                    }
                }
            }
            return "";
        }
    }
}
