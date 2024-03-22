using TelegramUserBotTest.Helpers.Models;

namespace TelegramUserBotTest.Helpers
{
    internal static class HireArmyHelper
    {
        public static HireArmyModel GetCountHireArmy(string message)
        {
            var hireArmy = new HireArmyModel();

            var barracks = message[(message.IndexOf("Казармы: ") + "Казармы: ".Length)..message.IndexOf("🛏")];
            var swordsman = message[(message.IndexOf("Мечники: ") + "Мечники: ".Length)..message.IndexOf("🗡️")];
            var horseman = message[(message.IndexOf("Всадники: ") + "Всадники: ".Length)..message.IndexOf("🏇🏻")];
            var spearman = message[(message.IndexOf("Копейщики: ") + "Копейщики: ".Length)..message.IndexOf("🍢")];

            var gold = message[(message.IndexOf("Золото: ") + "Золото: ".Length)..message.IndexOf("💰")];
            var wood = message[(message.IndexOf("Дерево: ") + "Дерево: ".Length)..message.IndexOf("🪵")];
            var provisions = message[(message.IndexOf("Провизия: ") + "Провизия: ".Length)..message.IndexOf("🍞")];
            var residens = message[(message.IndexOf("Жители: ") + "Жители: ".Length)..message.IndexOf("👨🏻‍🌾")];
            var horse = message[(message.IndexOf("Лошади: ") + "Лошади: ".Length)..message.IndexOf("🐴")];

            hireArmy.BarracksOccupiedPlaces = Convert.ToInt32(barracks[0..barracks.IndexOf("/")]);
            hireArmy.BarracksAllPlaces = Convert.ToInt32(barracks[(barracks.IndexOf("/") + 1)..barracks.Length]);

            hireArmy.Swordsmen = Convert.ToInt32(swordsman);
            hireArmy.Horsemen = Convert.ToInt32(horseman);
            hireArmy.Spearmen = Convert.ToInt32(spearman);

            hireArmy.Gold = Convert.ToInt32(gold);
            hireArmy.Wood = Convert.ToInt32(wood);
            hireArmy.Provisions = Convert.ToInt32(provisions);
            hireArmy.Residents = Convert.ToInt32(residens);
            hireArmy.Horse = Convert.ToInt32(horse);

            return hireArmy;
        }
    }
}
