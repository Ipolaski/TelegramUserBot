﻿using TelegramUserBotTest.Helpers;
using TelegramUserBotTest.Helpers.Enum.Actions;
using TelegramUserBotTest.Helpers.Enum.Resources;
using TelegramUserBotTest.Helpers.Models;
using TelegramUserBotTest.Helpers.Price;
using TL;
using WTelegram;

namespace TelegramUserBotTest.Strategies
{
    internal class AttackStrategy
    {
        private readonly long _bastionSiegeId;
        private readonly Client _client;
        private User _user;
        private InputPeer? _peer;
        private Timer? _timerAttack;
        private bool _weaponAdd = false;

        public AttackStrategy(Client client, long bastionSiegeId)
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



            _timerAttack = new Timer(async (_) =>
            {
                await _client.SendMessageAsync(_peer, "/home");
                Thread.Sleep(4000);
                await _client.SendMessageAsync(_peer, Battle.Гарнизон.Name());
                Thread.Sleep(4000);
                await _client.SendMessageAsync(_peer, Battle.Разведка.Name());
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
            if (message.message.Contains(Battle.Разведка.Name()) && message.message.Contains("Цель:"))
            {
                await _client.SendMessageAsync(_peer, Battle.Атаковать.Name());
            }
            else if (message.message.Contains(Battle.Разведка.Name()) && message.message.Contains("Искать:"))
            {
                await _client.SendMessageAsync(_peer, Battle.Искать.Name());
            }
            else if (message.message.Contains("Нельзя нападать так часто."))
            {
                await ResetAttack();
            }
            else if (message.message.Contains("➕ Нанять") && _weaponAdd)
            {
                await AddArmy(message.message);
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

                await _client.SendMessageAsync(_peer, Battle.Гарнизон.Name());
                Thread.Sleep(4000);

                await _client.SendMessageAsync(_peer, "🛡 Армия");
                Thread.Sleep(4000);
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

        private async Task AddArmy(string message)
        {
            _weaponAdd = false;

            var hireArmy = HireArmyHelper.GetCountHireArmy(message);
            (int swordsmen, int horsemen, int spearmen) = GetCountByArmy(hireArmy);
            
            await _client.SendMessageAsync(_peer, Army.Мечники.Name());
            Thread.Sleep(4000);
            await _client.SendMessageAsync(_peer, swordsmen.ToString());
            Thread.Sleep(4000);

            await _client.SendMessageAsync(_peer, Army.Копейщики.Name());
            Thread.Sleep(4000);
            await _client.SendMessageAsync(_peer, spearmen.ToString());
            Thread.Sleep(4000);

            await _client.SendMessageAsync(_peer, Army.Всадники.Name());
            Thread.Sleep(4000);
            await _client.SendMessageAsync(_peer, horsemen.ToString());
            Thread.Sleep(4000);

            await _client.SendMessageAsync(_peer, "/home");


        }

        private (int, int, int) GetCountByArmy(HireArmyModel hireArmy)
        {
            int swordsmen = 0;
            int horsemen = 0;
            int spearmen = 0;

            var count = hireArmy.BarracksOccupiedPlaces / 3;

            for (int i = 0; i < count; i++)
            {
                //Мечники
                if (hireArmy.Residents >= ArmyPrice.SwordsmenPrice.Resident &&
                    hireArmy.Provisions >= ArmyPrice.SwordsmenPrice.Provision &&
                    hireArmy.Gold >= ArmyPrice.SwordsmenPrice.Gold)
                {
                    hireArmy.Gold -= ArmyPrice.SwordsmenPrice.Gold;
                    hireArmy.Provisions -= ArmyPrice.SwordsmenPrice.Provision;
                    hireArmy.Residents -= ArmyPrice.SwordsmenPrice.Resident;

                    swordsmen++;
                }
                //Всадники
                if (hireArmy.Residents >= ArmyPrice.HorsemenPrice.Resident &&
                    hireArmy.Provisions >= ArmyPrice.HorsemenPrice.Provision &&
                    hireArmy.Horse >= ArmyPrice.HorsemenPrice.Horse &&
                    hireArmy.Gold >= ArmyPrice.HorsemenPrice.Gold)
                {
                    hireArmy.Gold -= ArmyPrice.HorsemenPrice.Gold;
                    hireArmy.Provisions -= ArmyPrice.HorsemenPrice.Provision;
                    hireArmy.Residents -= ArmyPrice.HorsemenPrice.Resident;
                    hireArmy.Horse -= ArmyPrice.HorsemenPrice.Horse;

                    horsemen++;
                }

                //копейщики
                if (hireArmy.Residents >= ArmyPrice.SpearPrice.Resident &&
                    hireArmy.Gold >= ArmyPrice.SpearPrice.Gold &&
                    hireArmy.Provisions >= ArmyPrice.SpearPrice.Provision &&
                    hireArmy.Wood >= ArmyPrice.SpearPrice.Wood)
                {
                    hireArmy.Gold -= ArmyPrice.SpearPrice.Gold;
                    hireArmy.Provisions -= ArmyPrice.SpearPrice.Provision;
                    hireArmy.Residents -= ArmyPrice.SpearPrice.Resident;
                    hireArmy.Wood -= ArmyPrice.SpearPrice.Wood;

                    spearmen++;
                }
            }

            for (int i = 0; i < (hireArmy.BarracksOccupiedPlaces - count * 3); i++)
            {
                //Мечники
                if (hireArmy.Residents >= ArmyPrice.SwordsmenPrice.Resident &&
                   hireArmy.Provisions >= ArmyPrice.SwordsmenPrice.Provision &&
                   hireArmy.Gold >= ArmyPrice.SwordsmenPrice.Gold)
                {
                    hireArmy.Gold -= ArmyPrice.SwordsmenPrice.Gold;
                    hireArmy.Provisions -= ArmyPrice.SwordsmenPrice.Provision;
                    hireArmy.Residents -= ArmyPrice.SwordsmenPrice.Resident;

                    swordsmen++;
                }
            }

            return (swordsmen, horsemen, spearmen);
        }

        private async Task ResetAttack()
        {
            int period = new Random().Next(960000, 1050000);
            await _client.SendMessageAsync(_peer, $"Атакую снова через {period / 1000 / 60}~ мин.");
            Thread.Sleep(4000);

            _timerAttack!.Dispose();
            _timerAttack = null;

            _timerAttack = new Timer(async (_) =>
            {
                await _client.SendMessageAsync(_peer, "/home");
                Thread.Sleep(4000);
                await _client.SendMessageAsync(_peer, Battle.Гарнизон.Name());
                Thread.Sleep(4000);
                await _client.SendMessageAsync(_peer, Battle.Разведка.Name());
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
