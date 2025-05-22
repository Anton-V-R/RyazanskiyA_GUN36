using System;
using System.Collections.Generic;

using Final_Task.Games.Base;

namespace Final_Task.Games.Dice
{
    /// <summary>
    /// Реализация игры в кости
    /// </summary>
    public class DiceGame : CasinoGameBase
    {
        private readonly byte _maxValue;
        private readonly byte _minValue;
        private readonly byte _numberOfDice;
        private List<Dice> _diceCollection;

        /// <summary>
        /// Игра в кости
        /// </summary>
        /// <param name="dices">Количество костей</param>
        /// <param name="minDiceValue">мин значение</param>
        /// <param name="maxDiceValue">макс значение</param>
        /// <exception cref="ArgumentException">Ошибка ставки</exception>
        public DiceGame(byte numberOfDice, byte minValue, byte maxValue)
        {
            if(numberOfDice <= 0)
                throw new ArgumentException("Количество костей должно быть положительным", nameof(numberOfDice));

            if(minValue >= maxValue)
                throw new ArgumentException("Минимальное значение больше Максимального", nameof(minValue));

            _numberOfDice = numberOfDice;
            _minValue = minValue;
            _maxValue = maxValue;

            FactoryMethod();
        }

        /// <summary>
        /// Запуск игры
        /// </summary>
        /// <param name="playerBet">Ставка</param>
        public override void PlayGame()
        {
            Console.WriteLine($"Количество костей {_numberOfDice} значения ({_minValue}-{_maxValue})");

            byte playerScore = RollDice("Player");
            byte computerScore = RollDice("Computer");

            Console.WriteLine($"\n  Результат - Player: {playerScore}, Computer: {computerScore}");

            if(playerScore > computerScore)
            {
                PrintResult("Вы выиграли");
                OnGameWon();
            }
            else if(computerScore > playerScore)
            {
                PrintResult("Вы проиграли!");
                OnGameLost();
            }
            else
            {
                PrintResult("Ничья!");
                OnGameDrawn();
            }
        }

        protected override void FactoryMethod()
        {
            _diceCollection = new List<Dice>(_numberOfDice);

            for(byte i = 0; i < _numberOfDice; i++)
            {
                _diceCollection.Add(new Dice(_minValue, _maxValue));
            }
        }
        private byte RollDice(string playerName)
        {
            Console.WriteLine($"\n{playerName} бросает:");
            byte total = 0;

            foreach(var dice in _diceCollection)
            {
                byte roll = dice.Number;
                Console.WriteLine($"выпало: {roll}");
                total += roll;
            }

            Console.WriteLine($"Всего: {total}");
            return total;
        }
    }
}
