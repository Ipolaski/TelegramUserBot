using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TelegramUserBotTest.Enum.Actions;
using TelegramUserBotTest.Enum.Resources;

namespace TelegramUserBotTest.Helpers
{
    public static class EnumHelper
    {
        internal static string Description( this Battle enumValue )
        {
            var enumType = enumValue.GetType();
            var field = enumType.GetField( enumValue.ToString() );
            var attributes = field.GetCustomAttributes( typeof( DescriptionAttribute ), false );
            return attributes.Length == 0
                ? enumValue.ToString()
                : ((DescriptionAttribute)attributes[0]).Description;
        }

        internal static string Description( this Navigation enumValue )
        {
            var enumType = enumValue.GetType();
            var field = enumType.GetField( enumValue.ToString() );
            var attributes = field.GetCustomAttributes( typeof( DescriptionAttribute ), false );
            return attributes.Length == 0
                ? enumValue.ToString()
                : ((DescriptionAttribute)attributes[0]).Description;
        }

        internal static string Description( this Army enumValue )
        {
            var enumType = enumValue.GetType();
            var field = enumType.GetField( enumValue.ToString() );
            var attributes = field.GetCustomAttributes( typeof( DescriptionAttribute ), false );
            return attributes.Length == 0
                ? enumValue.ToString()
                : ((DescriptionAttribute)attributes[0]).Description;
        }

        internal static string Description( this Building enumValue )
        {
            var enumType = enumValue.GetType();
            var field = enumType.GetField( enumValue.ToString() );
            var attributes = field.GetCustomAttributes( typeof( DescriptionAttribute ), false );
            return attributes.Length == 0
                ? enumValue.ToString()
                : ((DescriptionAttribute)attributes[0]).Description;
        }

        internal static string Description( this LivePower enumValue )
        {
            var enumType = enumValue.GetType();
            var field = enumType.GetField( enumValue.ToString() );
            var attributes = field.GetCustomAttributes( typeof( DescriptionAttribute ), false );
            return attributes.Length == 0
                ? enumValue.ToString()
                : ((DescriptionAttribute)attributes[0]).Description;
        }
    }
}
