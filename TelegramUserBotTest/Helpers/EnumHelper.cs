﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TelegramUserBotTest.Enum.Resources;
using TelegramUserBotTest.Helpers.Enum.Actions;
using TelegramUserBotTest.Helpers.Enum.Resources;

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
            var attributes = field.GetCustomAttributes( typeof( DisplayAttribute ), false );
            return attributes.Length == 0
                ? enumValue.ToString()
                : ((DisplayAttribute)attributes[0]).Name;
        }

        internal static string Description( this Army enumValue )
        {
            var enumType = enumValue.GetType();
            var field = enumType.GetField( enumValue.ToString() );
            var attributes = field.GetCustomAttributes( typeof( DisplayAttribute ), false );
            return attributes.Length == 0
                ? enumValue.ToString()
                : ((DisplayAttribute)attributes[0]).Name;
        }

        internal static string Description( this Building enumValue )
        {
            var enumType = enumValue.GetType();
            var field = enumType.GetField( enumValue.ToString() );
            var attributes = field.GetCustomAttributes( typeof( DisplayAttribute ), false );
            return attributes.Length == 0
                ? enumValue.ToString()
                : ((DisplayAttribute)attributes[0]).Name;
        }

        internal static string Description( this LivePower enumValue )
        {
            var enumType = enumValue.GetType();
            var field = enumType.GetField( enumValue.ToString() );
            var attributes = field.GetCustomAttributes( typeof( DisplayAttribute ), false );
            return attributes.Length == 0
                ? enumValue.ToString()
                : ((DisplayAttribute)attributes[0]).Name;
        }
    }
}
