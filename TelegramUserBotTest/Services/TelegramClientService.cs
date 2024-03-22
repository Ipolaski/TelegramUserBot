using TelegramUserBotTest.Helpers;
using TelegramUserBotTest.Helpers.Enum.Actions;
using TelegramUserBotTest.Helpers.Enum.Resources;
using TL;
using WTelegram;

namespace TelegramUserBotTest.Servcices
{
    internal class TelegramClientService
    {
        private readonly long _bastionSiegeId;
        private readonly Client _client;
        private User _user;
        private InputPeer? _peer;
        private Timer? _timerAttack;
        private bool _weaponAdd = false;

        public TelegramClientService(long bastionSiegeId, Func<string, string> configProvier)
        {
            _bastionSiegeId = bastionSiegeId;
            _client = new Client(configProvier);
            _user = new User();
        }

        public async Task Start()
        {
            _user = await _client.LoginUserIfNeeded();

            var dialogs = await _client.Messages_GetAllDialogs();
            _peer = dialogs.users[_bastionSiegeId];

            _client.OnUpdate += ClientOnUpdate;



            _timerAttack = new Timer(async (_) =>
            {
                await _client.SendMessageAsync(_peer, "/home");
                Thread.Sleep(4000);
                await _client.SendMessageAsync(_peer, Battle.Гарнизон.Description());
            }, null, 0, 60000);
        }

        public void Stop()
        {
            _client.OnUpdate -= ClientOnUpdate;

            _timerAttack!.Dispose();
        }

        private async Task HandleNewUpdate(MessageBase message)
        {
            switch (message)
            {
                case Message mess:
                    await GetCommand(mess);
                    break;

                default:
                    break;
            }

        }

        private async Task GetCommand(Message message)
        {
            if (message.message.Contains(Battle.Гарнизон.Description()) && !_weaponAdd)
            {
                await _client.SendMessageAsync(_peer, Battle.Разведка.Description());
            }
            else if (message.message.Contains(Battle.Разведка.Description()) && message.message.Contains("Цель:"))
            {
                await _client.SendMessageAsync(_peer, Battle.Атаковать.Description());
            }
            else if (message.message.Contains(Battle.Разведка.Description()) && message.message.Contains("Искать:"))
            {
                await _client.SendMessageAsync(_peer, Battle.Искать.Description());
            }
            else if (message.message.Contains("Нельзя нападать так часто."))
            {
                await ResetAttack();
            }


            else if (message.message.Contains("Ты победил!"))
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("ПОБЕДА");
                Console.ResetColor();

                await ResetAttack();
            }
            else if (message.message.Contains("Сначала обзаведись армией"))
            {
                await ResetAttack();

                _weaponAdd = true;

                await _client.SendMessageAsync(_peer, "/home");
                Thread.Sleep(4000);

                await _client.SendMessageAsync(_peer, Battle.Гарнизон.Description());
                Thread.Sleep(4000);

                await _client.SendMessageAsync(_peer, "🛡 Армия");
                Thread.Sleep(4000);

                await _client.SendMessageAsync(_peer, Army.Мечники.Description());
                Thread.Sleep(4000);
                await _client.SendMessageAsync(_peer, "10");
                Thread.Sleep(4000);

                await _client.SendMessageAsync(_peer, Army.Копейщики.Description());
                Thread.Sleep(4000);
                await _client.SendMessageAsync(_peer, "10");
                Thread.Sleep(4000);

                await _client.SendMessageAsync(_peer, Army.Копейщики.Description());
                Thread.Sleep(4000);
                await _client.SendMessageAsync(_peer, "10");
                Thread.Sleep(4000);

                await _client.SendMessageAsync(_peer, "/home");

                _weaponAdd = false;
            }
            else if (message.message.Contains("Ты проиграл..."))
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Поражение");
                Console.ResetColor();

                await ResetAttack();
            }


        }

        private async Task ResetAttack()
        {
            int period = new Random().Next(960000, 1200000);
            await _client.SendMessageAsync(_peer, $"Атакую снова через {period / 1000 / 60}~ мин.");
            Thread.Sleep(4000);

            _timerAttack!.Dispose();
            _timerAttack = null;

            _timerAttack = new Timer(async (_) =>
            {
                await _client.SendMessageAsync(_peer, "/home");
                Thread.Sleep(4000);
                await _client.SendMessageAsync(_peer, Battle.Гарнизон.Description());
            }, null, period, period);
        }

        private async Task ClientOnUpdate(UpdatesBase updates)
        {
            foreach (var update in updates.UpdateList)
            {
                switch (update)
                {
                    case UpdateNewMessage unm:
                        if ((unm.message.Peer.ID == _bastionSiegeId && unm.message.From == null) || (unm.message.Peer.ID == _bastionSiegeId && unm.message.From.ID != _user.ID))
                            await HandleNewUpdate(unm.message);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
