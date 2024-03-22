using System.ComponentModel.DataAnnotations;

namespace TelegramUserBotTest.Helpers.Enum.Resources
{
    enum Army
    {
        [Display(Name = "🗡️ Мечники")]
        Мечники = 0,

        [Display(Name = "🏇🏻 Всадники")]
        Всадники = 1,

        [Display(Name = "🍢 Копейщики")]
        Копейщики = 2,
    }
}