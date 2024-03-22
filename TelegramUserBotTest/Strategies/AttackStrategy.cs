using TelegramUserBotTest.Helpers;
using TelegramUserBotTest.Helpers.Enum.Actions;
using TelegramUserBotTest.Helpers.Enum.Resources;
using TL;
using WTelegram;
//using static TdLib.TdApi;

namespace TelegramUserBotTest.Strategies
{
    internal class AttackStrategy
    {
        private readonly long _bastionSiegeId;
        private readonly Client _client;
        //private User _user;
        private InputPeer? _peer;
        private Timer? _timerAttack;
        //private bool _weaponAdd = false;

        public AttackStrategy( Client client, long bastionSiegeId = 0 )
        {
            _client = client;
            _bastionSiegeId = bastionSiegeId;
            //_user = new User();
        }

        public async Task Start()
        {
            //_user = await _client.LoginUserIfNeeded();
            await _client.LoginUserIfNeeded();

            var dialogs = await _client.Messages_GetAllDialogs();
            _peer = dialogs.users[_bastionSiegeId];

            _client.OnUpdate += ClientOnUpdate;

            _timerAttack = new Timer( 
                async ( _ ) =>
                {
                    await _client.SendMessageAsync( _peer, Navigation.ГлавноеМеню.Name() );
                    Thread.Sleep( 4000 );
                    await _client.SendMessageAsync( _peer, Battle.Гарнизон.Name() );
                    Thread.Sleep( 4000 );
                    await _client.SendMessageAsync( _peer, Battle.Разведка.Name() );
                },
                null,
                0,
                60000 );
        }

        public void Stop()
        {
            _client.OnUpdate -= ClientOnUpdate;

            _timerAttack!.Dispose();
        }

        private async Task ClientOnUpdate( UpdatesBase updates )
        {
            foreach (UpdateNewMessage update in updates.UpdateList.Where( upd => upd.GetType().Name == "UpdateNewMessage" ))
                if (update.message.Peer.ID == _bastionSiegeId && update.message.From == null)
                    await HandleNewUpdate( update.message as Message );
        }

        private async Task HandleNewUpdate( Message message )
        {
            await GetCommand( message );
        }

        private async Task GetCommand( Message message )
        {
            var matchOperationNumber = 0;

            var battleResponse = Enum.GetNames( typeof( BattleResponces ) ).ToList();
            for (int i = 0; i < battleResponse.Count && matchOperationNumber == 0; i++)
                if (message.message.Contains( battleResponse[i] ))
                    matchOperationNumber = i;

            switch (matchOperationNumber)
            {
                case 0:
                    await _client.SendMessageAsync( _peer, Battle.Искать.Name() );
                    break;
                case 1:
                    await _client.SendMessageAsync( _peer, Battle.Атаковать.Name() );
                    break;
                case 2:
                    await ResetAttack();
                    break;
                case 3:
                    await AddArmy( message.message );
                    break;
                case 4:
                    await RecruitArmy();
                    break;
                case 5:
                    await Loose();
                    break;
                case 6:
                    await Win();
                    break;
            }
        }

        async Task Loose()
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine( "Поражение" );
            Console.ResetColor();

            await ResetAttack();
        }

        async Task Win()
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine( "ПОБЕДА" );
            Console.ResetColor();

            await ResetAttack();
        }

        async Task RecruitArmy()
        {
            await ResetAttack();

            //_weaponAdd = true;

            await _client.SendMessageAsync( _peer, Navigation.ГлавноеМеню.Name() );
            Thread.Sleep( 4000 );

            await _client.SendMessageAsync( _peer, Battle.Гарнизон.Name() );
            Thread.Sleep( 4000 );

            await _client.SendMessageAsync( _peer, Battle.Армия.Name() );
            Thread.Sleep( 4000 );
        }

        async Task AddArmy( string message )
        {
            //HireArmyHelper.GetCountHireArmy(message);
            //_weaponAdd = false;
            foreach (var unit in Enum.GetNames( typeof( BattleResponces ) ).ToList())
            {
                await _client.SendMessageAsync( _peer, unit );
                Thread.Sleep( 4000 );
                await _client.SendMessageAsync( _peer, "15" );
                Thread.Sleep( 4000 );
            }

            //await _client.SendMessageAsync( _peer, Army.Мечники.Name() );
            //Thread.Sleep( 4000 );
            //await _client.SendMessageAsync( _peer, "15" );
            //Thread.Sleep( 4000 );

            //await _client.SendMessageAsync( _peer, Army.Копейщики.Name() );
            //Thread.Sleep( 4000 );
            //await _client.SendMessageAsync( _peer, "15" );
            //Thread.Sleep( 4000 );

            //await _client.SendMessageAsync( _peer, Army.Всадники.Name() );
            //Thread.Sleep( 4000 );
            //await _client.SendMessageAsync( _peer, "14" );
            //Thread.Sleep( 4000 );

            await _client.SendMessageAsync( _peer, Navigation.ГлавноеМеню.Name() );
        }

        async Task ResetAttack()
        {
            int period = new Random().Next( 960000, 1200000 );
            await _client.SendMessageAsync( _peer, $"Атакую снова через {period / 1000 / 60}~ мин." );
            Thread.Sleep( 4000 );

            _timerAttack!.Dispose();
            _timerAttack = null;

            _timerAttack = new Timer( async ( _ ) =>
            {
                await _client.SendMessageAsync( _peer, Navigation.ГлавноеМеню.Name() );
                Thread.Sleep( 4000 );
                await _client.SendMessageAsync( _peer, Battle.Гарнизон.Name() );
                Thread.Sleep( 4000 );
                await _client.SendMessageAsync( _peer, Battle.Разведка.Name() );
            }, null, period, period );
        }


    }
}