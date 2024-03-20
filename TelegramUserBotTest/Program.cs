// See https://aka.ms/new-console-template for more information

using TL;

Console.WriteLine("Hello, World!");

const int bastionSiegeId = 252148344;

using var tgClient = new WTelegram.Client(Config);
var myClient = await tgClient.LoginUserIfNeeded();

Console.WriteLine($"Пользователь:{myClient}");

var chats = await tgClient.Messages_GetAllChats();
var dialogsPeers = await tgClient.Messages_GetPeerDialogs();
var dialogs = await tgClient.Messages_GetAllDialogs();
//dialogs.Dialogs.Where(x => x.Peer.ID == bastionSiegeId).FirstOrDefault();

InputPeer peer = dialogs.users[bastionSiegeId];

await tgClient.SendMessageAsync(peer, "🏝 Гавань");

Console.ReadKey();

string Config(string arg)
{
    switch (arg)
    {
        case "api_id":
            return "25525193";
        case "api_hash":
            return "6569b5c46c19dcb3c104cebf504027f8";
        case "phone_number":
            return "+79991094660";
    }
    return null;
}