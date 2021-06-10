﻿using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace DDF.Common
{
    public static class Extensions
    {
        /// <summary>
        ///     A generic extension method that aids in reflecting 
        ///     and retrieving any attribute that is applied to an `Enum`.
        /// </summary>
        public static string GetAttribute(this System.Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()?
                            .GetCustomAttribute<DisplayAttribute>()?
                            .GetName()
                            ?? string.Empty;
        }
    }
}