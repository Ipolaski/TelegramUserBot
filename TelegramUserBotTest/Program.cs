// See https://aka.ms/new-console-template for more information

using TL;



Dictionary<long, User> Users = [];
Dictionary<long, ChatBase> Chats = [];

Console.WriteLine("Hello, World!");

const int bastionSiegeId = 252148344;

using var tgClient = new WTelegram.Client(Config);
var myClient = await tgClient.LoginUserIfNeeded();

Console.WriteLine($"Пользователь:{myClient}");

var chats = await tgClient.Messages_GetAllChats();
var dialogsPeers = await tgClient.Messages_GetPeerDialogs();
var dialogs = await tgClient.Messages_GetAllDialogs();

InputPeer peer = dialogs.users[bastionSiegeId];

tgClient.OnUpdate += Client_OnUpdate;

await tgClient.SendMessageAsync(peer, "🧝🏽‍♂️ Странствующий торговец");
Thread.Sleep(100);

Console.ReadKey();
Console.ReadKey();
Console.ReadKey();

async Task Client_OnUpdate(UpdatesBase updates)
{
    foreach (var update in updates.UpdateList)
    {
        switch (update)
        {
            case UpdateNewMessage unm:
                if ((unm.message.Peer.ID == bastionSiegeId && unm.message.From == null) || (unm.message.Peer.ID == bastionSiegeId && unm.message.From.ID != myClient.ID))
                    await HandleNewMessage(unm.message);
                break;
            case UpdateEditMessage uem:
                if ((uem.message.Peer.ID == bastionSiegeId && uem.message.From == null) || (uem.message.Peer.ID == bastionSiegeId && uem.message.From.ID != myClient.ID))
                    await HandleEditMessage(uem.message);
                break;

            default:
                break;
        }
    }
}

async Task HandleNewMessage(MessageBase messageBase)
{

    switch (messageBase)
    {
        case Message m:
            Console.WriteLine($"{m.post_author} in {m.peer_id} >  {m.message}");
            InlineButtonPress(m);

            break;
    }

}

Task HandleEditMessage(MessageBase messageBase)
{
    switch (messageBase)
    {
        case TL.Message m:
            Console.WriteLine($"\n----------EDITED MESSAGE------------- \n" +
            $" {m.post_author} in {m.peer_id} >  {m.message}");

            switch (m.reply_markup)
            {
                case ReplyInlineMarkup key:
                    if (m.message.Contains("⚓️ Якорь"))
                    {
                        switch (key.rows[0].buttons[1])
                        {
                            case KeyboardButtonCallback row:
                                tgClient.Messages_GetBotCallbackAnswer(new InputPeerSelf(), m.ID, row.data);
                                break;
                            default:
                                break;
                        }
                    }

                    break;
                default:
                    break;
            }

            break;
    }
    return Task.CompletedTask;
}


void InlineButtonPress(Message message)
{
    if (message.message.Contains("🧝🏽‍♂️ Странствующий торговец"))
    {
        switch (message.reply_markup)
        {
            case ReplyInlineMarkup key:

                switch (key.rows[0].buttons[0])
                {
                    case KeyboardButtonCallback row:
                        tgClient.Messages_GetBotCallbackAnswer(new InputPeerSelf(), message.ID, row.data);
                        Thread.Sleep(100);

                        break;
                    default:
                        break;
                }


                break;
            default:
                break;
        }
        Console.WriteLine();
    }

}

string Config(string arg)
{
    switch (arg)
    {
        case "api_id":
            return "25525193";
        case "api_hash":
            return "6569b5c46c19dcb3c104cebf504027f8";
        case "phone_number":
            return "+----------";
    }
    return null;
}
