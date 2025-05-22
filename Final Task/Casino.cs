using System;

using Final_Task.Games.Base;
using Final_Task.Games.Blackjack;
using Final_Task.Games.Dice;
using Final_Task.Models;
using Final_Task.Services;
using Final_Task.Utilites;

using Newtonsoft.Json;

namespace Final_Task
{
    public class Casino  : IDisposable
    {
        private const int maxBankValue = 1000000;
        private const string profileKey = "player_profile";
        private readonly PlayerProfile _playerProfile;
        private readonly ISaveLoadService<PlayerProfile> _saveLoadService;

        private int _currentBet;
        private IGame _currentGame;
        public Casino(string basePath) 
        {
            _saveLoadService = new FileSystemSaveLoadService(basePath) ?? throw new ArgumentNullException($"Error {basePath}");

            _playerProfile = _saveLoadService.LoadPlayerProfile(profileKey);

            if(_playerProfile.Bank <= 100)
            {
                PrintResult.Info($" {_playerProfile.Name} у вас на счету {_playerProfile.Bank}. Сделайте пополнение.");

                AddMoney();
            }
        }

        public void Dispose()
        {
            UnsubscribeFromEvents();
        }
        public void PlayGame() => _currentGame.PlayGame();

        public void StartGame()
        {
            try
            {
                while(_playerProfile.Bank > 0)
                {
                    PrintResult.Info($"\n {_playerProfile.Name}. Текущий банк: {_playerProfile.Bank}");
                    PrintResult.Info("\n Выберите игру:");
                    PrintResult.Info("1. Блэкджек");
                    PrintResult.Info("2. Игра в кости");
                    PrintResult.Info("3. Выход");
                    PrintResult.Info("4. Пополнение банка");

                    byte choice;
                    while(!byte.TryParse(Console.ReadLine(), out choice) || (choice < 1 || choice > 4))
                    {
                        PrintResult.Info("Некорректный ввод. Введите число от 1 до 4");
                    }

                    IGame game;
 
                    switch(choice)
                    {
                        case 1:

                            game = new BlackjackGame(36);
                            break;

                        case 2:

                            PrintResult.Info("Введите количество костей (1-4)");

                            byte dices;
                            while(!byte.TryParse(Console.ReadLine(), out dices) || (dices < 1 || dices > 4))
                            {
                                PrintResult.Info("Некорректный ввод. Введите число от 1 до 4");
                            }

                            game = new DiceGame(dices, 1, 6);
                            break;

                        case 4:
                            AddMoney();

                            continue;

                        default:

                            PrintResult.Info("До свидания!");
                            return;
                    }

                    _currentGame = game;

                    PlaySelectedGame();
                }

                PrintResult.ColorInfo("No money? Kicked!", ConsoleColor.Red);

            }
            finally
            {
                SaveProfile();
            }

        }

        // Методы для управления подписками
        public void SubscribeToEvents()
        {
            _currentGame.OnWin += HandleWin;
            _currentGame.OnLose += HandleLose;
            _currentGame.OnDraw += HandleDraw;
        }

        public void UnsubscribeFromEvents()
        {
            if(_currentGame != null)
            {
                _currentGame.OnWin -= HandleWin;
                _currentGame.OnLose -= HandleLose;
                _currentGame.OnDraw -= HandleDraw;
            }
        }

        // Явные методы-обработчики
        protected void HandleDraw() => PrintResult.Info($"Ничья! Ставка {_currentBet} возвращена.");

        protected void HandleLose() => HandleGameResult(false, _currentBet);

        protected void HandleWin() => HandleGameResult(true, _currentBet);

        private void AddMoney()
        {
            PrintResult.Info("Введите сумму пополнения банка. (100-1000)");

            int money;
            while(!int.TryParse(Console.ReadLine(), out money) || money < 100 || money > 1000)
            {
                PrintResult.Info($"Некорректное число. Введите число от 100 до 1000");
            }

            _playerProfile.Bank += money;

            if(_playerProfile.Bank > maxBankValue)
            {
                _playerProfile.Bank /= 2;

                PrintResult.Info($"You wasted half of your bank money in casino’s bar");
            }
        }

        private void CheckBankLimit()
        {
            if(_playerProfile.Bank > maxBankValue)
            {
                int excess = _playerProfile.Bank - maxBankValue;
                PrintResult.ColorInfo($"Вы разорили казино! Возвращено: {excess}", ConsoleColor.Magenta);
                PrintResult.Info($"Новый банк: {_playerProfile.Bank}");
            }

        }

        private void HandleGameResult(bool isWin, int bet)
        {
            if(isWin)
            {
                _playerProfile.Bank += bet;
            }
            else
            {
                _playerProfile.Bank -= bet;
            }
            
            PrintResult.Info($"Ставка {bet} Новый банк: {_playerProfile.Bank}");
            CheckBankLimit();
        }

        private void PlaySelectedGame()
        {
            Console.Write($"Введите ставку (максимум {_playerProfile.Bank}): ");

            while(!int.TryParse(Console.ReadLine(), out _currentBet) || _currentBet <= 0 || _currentBet > _playerProfile.Bank)
            {
                PrintResult.Info($"Некорректная ставка. Введите число от 1 до {_playerProfile.Bank}");
            }
            try
            {
                // Явная подписка на события
                SubscribeToEvents();

                _currentGame.PlayGame();
            }
            finally
            {
                UnsubscribeFromEvents();
            }

        }
        private void SaveProfile()
        {
            const string profileKey = "player_profile";
            try
            {
                string json = JsonConvert.SerializeObject(_playerProfile, Formatting.Indented);
                _saveLoadService.SavePlayerProfile(_playerProfile, profileKey);
            }
            catch(JsonException ex)
            {
                PrintResult.Info($"Error saving profile: {ex.Message}");
            }
        }
    }


}
