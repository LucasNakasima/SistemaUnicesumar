using SitemaUnicesumar.Data;
using SitemaUnicesumar.Enum;
using SitemaUnicesumar.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace SitemaUnicesumar.Helpers
{
    public class EnumHelper : Controller
    {
        public static string GetEnumDescription(ETypeReserve value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }
    }
}