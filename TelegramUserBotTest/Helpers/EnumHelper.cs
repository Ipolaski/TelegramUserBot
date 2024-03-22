using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TelegramUserBotTest.Enum.Resources;
using TelegramUserBotTest.Helpers.Enum.Actions;
using TelegramUserBotTest.Helpers.Enum.Resources;

namespace TelegramUserBotTest.Helpers
{
    public static class EnumHelper
    {
        internal static string Name( this Battle enumValue )
        {
            var enumType = enumValue.GetType();
            var field = enumType.GetField( enumValue.ToString() );
            var attributes = field.GetCustomAttributes( typeof( DisplayAttribute ), false );
            return attributes.Length == 0
                ? enumValue.ToString()
                : ((DisplayAttribute)attributes[0]).Name;
        }

        internal static string Name( this Navigation enumValue )
        {
            var enumType = enumValue.GetType();
            var field = enumType.GetField( enumValue.ToString() );
            var attributes = field.GetCustomAttributes( typeof( DisplayAttribute ), false );
            return attributes.Length == 0
                ? enumValue.ToString()
                : ((DisplayAttribute)attributes[0]).Name;
        }

        internal static string Name( this Army enumValue )
        {
            var enumType = enumValue.GetType();
            var field = enumType.GetField( enumValue.ToString() );
            var attributes = field.GetCustomAttributes( typeof( DisplayAttribute ), false );
            return attributes.Length == 0
                ? enumValue.ToString()
                : ((DisplayAttribute)attributes[0]).Name;
        }

        internal static string Name( this Building enumValue )
        {
            var enumType = enumValue.GetType();
            var field = enumType.GetField( enumValue.ToString() );
            var attributes = field.GetCustomAttributes( typeof( DisplayAttribute ), false );
            return attributes.Length == 0
                ? enumValue.ToString()
                : ((DisplayAttribute)attributes[0]).Name;
        }

        internal static string Name( this LivePower enumValue )
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
