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

            hireArmy.BarracksOccupiedPlaces = Convert.ToInt32(barracks[0..barracks.IndexOf("/")].Replace(",", ""));
            hireArmy.BarracksAllPlaces = Convert.ToInt32(barracks[(barracks.IndexOf("/") + 1)..barracks.Length].Replace(",", ""));

            hireArmy.Swordsmen = Convert.ToInt32(swordsman.Replace(",", ""));
            hireArmy.Horsemen = Convert.ToInt32(horseman.Replace(",", ""));
            hireArmy.Spearmen = Convert.ToInt32(spearman.Replace(",", ""));

            hireArmy.Gold = Convert.ToInt32(gold.Replace(",", ""));
            hireArmy.Wood = Convert.ToInt32(wood.Replace(",", ""));
            hireArmy.Provisions = Convert.ToInt32(provisions.Replace(",", ""));
            hireArmy.Residents = Convert.ToInt32(residens.Replace(",", ""));
            hireArmy.Horse = Convert.ToInt32(horse.Replace(",", ""));

            return hireArmy;
        }
    }
}
