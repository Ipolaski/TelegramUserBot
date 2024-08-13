using TL;
using WTelegram;

namespace TelegramUserBotTest.Strategies
{
    internal class ProductionStartegy
    {
        private readonly long _bastionSiegeId;
        private readonly Client _client;
        private User _user;
        private InputPeer? _peer;
        private Timer? _miningDiamond;

        public ProductionStartegy(Client client, long bastionSiegeId)
        {
            _client = client;
            _bastionSiegeId = bastionSiegeId;
            _user = new User();
        }

        public async Task Start()
        {
            _user = await _client.LoginUserIfNeeded();

            var dialogs = await _client.Messages_GetAllDialogs();
            _peer = dialogs.users[_bastionSiegeId];

            _client.OnUpdate += ClientOnUpdate;
            var dateNow = DateTime.UtcNow;
            var nextDay = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day + 1);

            var dueTime = (nextDay - dateNow).TotalMilliseconds + 10000;

            _miningDiamond = new Timer(async (_) => { await StartMining(); }, null, (int)dueTime, 86400000);

            await StartHunting();
            Thread.Sleep(2000);

            await _client.SendMessageAsync(_peer, $"Обновление попыток добычи алмазов через {dueTime / 1000 / 60 / 60} часов");

            await StartHunting();
        }

        public void Stop()
        {
            _client.OnUpdate -= ClientOnUpdate;

            _miningDiamond!.Dispose();
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
            if (message.message.Contains("⛏ Работа завершена. ") && message.message.Contains("Копать еще: /dig"))
            {
                await StartMining();
            }
            if (message.message.Contains("🐾 Охота завершена"))
            {
                await StartHunting();
            }
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

        private async Task StartMining()
        {
            await _client.SendMessageAsync(_peer, "/dig");
        }

        private async Task StartHunting()
        {
            await _client.SendMessageAsync(_peer, "/hunt");
        }
    }
}
