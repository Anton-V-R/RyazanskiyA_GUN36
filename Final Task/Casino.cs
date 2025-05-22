using System;

using Final_Task.Games.Base;
using Final_Task.Games.Blackjack;
using Final_Task.Games.Dice;
using Final_Task.Models;
using Final_Task.Services;

using Newtonsoft.Json;

namespace Final_Task
{
    public class Casino : IGame, IDisposable
    {
        private const int maxBankValue = 1000000;
        private const string profileKey = "player_profile";
        private readonly PlayerProfile _playerProfile;
        private readonly ISaveLoadService<PlayerProfile> _saveLoadService;

        private int _currentBet;
        private CasinoGameBase _currentGame;
        public Casino(string basePath) 
        {
            _saveLoadService = new FileSystemSaveLoadService(basePath) ?? throw new ArgumentNullException($"Error {basePath}");

            _playerProfile = _saveLoadService.LoadPlayerProfile(profileKey);

            if(_playerProfile.Bank <= 100)
            {
                Console.WriteLine($" {_playerProfile.Name} у вас на счету {_playerProfile.Bank}. Сделайте пополнение.");

                AddMoney();
            }
        }

        public event EventHandler GameDrawn
        {
            add
            {
                ((IGame)_currentGame).GameDrawn += value;
            }

            remove
            {
                ((IGame)_currentGame).GameDrawn -= value;
            }
        }

        public event EventHandler GameLost
        {
            add
            {
                ((IGame)_currentGame).GameLost += value;
            }

            remove
            {
                ((IGame)_currentGame).GameLost -= value;
            }
        }

        public event EventHandler GameWon
        {
            add
            {
                ((IGame)_currentGame).GameWon += value;
            }

            remove
            {
                ((IGame)_currentGame).GameWon -= value;
            }
        }
        public void Dispose()
        {
            UnsubscribeFromGameEvents();
        }
        public void PlayGame() => ((IGame)_currentGame).PlayGame();

        public void StartGame()
        {
            try
            {
                while(_playerProfile.Bank > 0)
                {
                    Console.WriteLine($"\n {_playerProfile.Name}. Текущий банк: {_playerProfile.Bank}");
                    Console.WriteLine("\n Выберите игру:");
                    Console.WriteLine("1. Блэкджек");
                    Console.WriteLine("2. Игра в кости");
                    Console.WriteLine("3. Выход");
                    Console.WriteLine("4. Пополнение банка");

                    byte choice;
                    while(!byte.TryParse(Console.ReadLine(), out choice) || (choice < 1 || choice > 4))
                    {
                        Console.WriteLine("Некорректный ввод. Введите число от 1 до 4");
                    }

                    IGame game;
 
                    switch(choice)
                    {
                        case 1:

                            game = new BlackjackGame(36);
                            break;

                        case 2:

                            Console.WriteLine("Введите количество костей (1-4)");

                            byte dices;
                            while(!byte.TryParse(Console.ReadLine(), out dices) || (dices < 1 || dices > 4))
                            {
                                Console.WriteLine("Некорректный ввод. Введите число от 1 до 4");
                            }

                            game = new DiceGame(dices, 1, 6);
                            break;

                        case 4:
                            AddMoney();

                            continue;

                        default:
                            //SaveProfile();
                            Console.WriteLine("До свидания!");
                            return;
                    }
                    _currentGame = (CasinoGameBase)game;

                    PlaySelectedGame();
                }

                ColorConsoleWriteLine("No money? Kicked!", ConsoleColor.Red);

            }
            finally
            {
                SaveProfile();
            }

        }

        private static void ColorConsoleWriteLine(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        private void AddMoney()
        {
            Console.WriteLine("Введите сумму пополнения банка. (100-1000)");

            int money;
            while(!int.TryParse(Console.ReadLine(), out money) || money < 100 || money > 1000)
            {
                Console.WriteLine($"Некорректное число. Введите число от 100 до 1000");
            }

            _playerProfile.Bank += money;

            if(_playerProfile.Bank > maxBankValue)
            {
                _playerProfile.Bank /= 2;

                Console.WriteLine($"You wasted half of your bank money in casino’s bar");
            }
        }

        private void CheckBankLimit()
        {
            if(_playerProfile.Bank > maxBankValue)
            {
                int excess = _playerProfile.Bank - maxBankValue;
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"Вы разорили казино! Возвращено: {excess}");
                Console.WriteLine($"Новый банк: {_playerProfile.Bank}");
                Console.ResetColor();

            }

        }

        private void HandleGameDraw(object sender, EventArgs e)
        {
            Console.WriteLine("Ничья! Ставка возвращена.");
        }

        private void HandleGameLose(object sender, EventArgs e)
        {
            HandleGameResult(false, _currentBet);
        }

        private void HandleGameResult(bool isWin, int bet)
        {
            if(isWin)
            {
                _playerProfile.Bank += bet;
                ColorConsoleWriteLine($"Вы выиграли {bet}! Новый банк: {_playerProfile.Bank}", ConsoleColor.Green);
            }
            else
            {
                _playerProfile.Bank -= bet;
                ColorConsoleWriteLine($"Вы проиграли {bet}. Новый банк: {_playerProfile.Bank}", ConsoleColor.Red);
            }

            CheckBankLimit();
        }


        private void HandleGameWin(object sender, EventArgs e)
        {
            HandleGameResult(true, _currentBet);
        }

        private void PlaySelectedGame()
        {
            Console.Write($"Введите ставку (максимум {_playerProfile.Bank}): ");

            while(!int.TryParse(Console.ReadLine(), out _currentBet) || _currentBet <= 0 || _currentBet > _playerProfile.Bank)
            {
                Console.WriteLine($"Некорректная ставка. Введите число от 1 до {_playerProfile.Bank}");
            }

            SubscribeToGameEvents();

            _currentGame.PlayGame();

            UnsubscribeFromGameEvents();

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
                Console.WriteLine($"Error saving profile: {ex.Message}");
            }
        }

        // Подписка на события текущей игры
        private void SubscribeToGameEvents()
        {
            if(_currentGame != null)
            {
                _currentGame.GameWon += HandleGameWin;
                _currentGame.GameLost += HandleGameLose;
                _currentGame.GameDrawn += HandleGameDraw;
            }


        }

        // Отписка от событий
        private void UnsubscribeFromGameEvents()
        {
            if(_currentGame != null)
            {
                _currentGame.GameWon -= HandleGameWin;
                _currentGame.GameLost -= HandleGameLose;
                _currentGame.GameDrawn -= HandleGameDraw;
            }
        }
    }


}
