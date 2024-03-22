using System.ComponentModel.DataAnnotations;

namespace TelegramUserBotTest.Helpers.Enum.Actions
{
    enum Battle
    {
        [Display(Name = "⚔️ Гарнизон")]
        Гарнизон = 0,

        [Display(Name = "🗺 Разведка")]
        Разведка = 1,

        [Display(Name = "🔍 Искать")]
        Искать = 2,

        [Display(Name = "⚔️ Атаковать")]
        Атаковать = 3,

        [Display(Name = "⚔️ Бой")]
        Бой = 4
    }
}
