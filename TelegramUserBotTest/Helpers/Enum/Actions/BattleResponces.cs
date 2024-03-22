using System.ComponentModel.DataAnnotations;

namespace TelegramUserBotTest.Helpers.Enum.Actions
{
    enum BattleResponces
    {
        [Display( Name = "Искать" )]
        Искать,

        [Display( Name = "Цель" )]
        Цель,
        
        [Display( Name = "Нельзя нападать так часто" )]
        ОткатАтаки,
        
        [Display( Name = "➕ Нанять" )]
        Нанять,
        
        [Display( Name = "Сначала обзаведись армией" )]
        НетАрмии,
        
        [Display( Name = "Ты проиграл..." )]
        Поражение,
        
        [Display( Name = "Ты победил!" )]
        Победа,
    }
}