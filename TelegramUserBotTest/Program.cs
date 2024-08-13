using TelegramUserBotTest.Strategies;

const int bastionSiegeId = 252148344;

using var tgClient = new WTelegram.Client(Config);
var myClient = await tgClient.LoginUserIfNeeded();

//var prSt = new ProductionStartegy(tgClient, bastionSiegeId);
//await prSt.Start();

var tgclient = new AttackStrategy(tgClient, bastionSiegeId);
await tgclient.Start();

Console.WriteLine("Бот запущен");

string menu = "";
while (menu != "q")
{
    switch (menu)
    {
        case "stop":
            Console.WriteLine("Бот остановлен");
            tgclient.Stop();
            break;
    }
}

tgclient.Stop();
Console.WriteLine("Бот остановлен");


string? Config(string arg)
{
    switch (arg)
    {
        case "api_id":
            return "25525193";
        case "api_hash":
            return "6569b5c46c19dcb3c104cebf504027f8";
        case "phone_number":
            return "+79871964024";
    }
    return null;
}
