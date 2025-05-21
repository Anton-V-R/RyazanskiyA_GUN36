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
        private readonly int _maxValue;
        private readonly int _minValue;
        private readonly int _numberOfDice;
        private List<Dice> _diceCollection;

        /// <summary>
        /// Игра в кости
        /// </summary>
        /// <param name="dices">Количество костей</param>
        /// <param name="minDiceValue">мин значение</param>
        /// <param name="maxDiceValue">макс значение</param>
        /// <exception cref="ArgumentException">Ошибка ставки</exception>
        public DiceGame(int numberOfDice, int minValue, int maxValue)
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

            int playerScore = RollDice("Player");
            int computerScore = RollDice("Computer");

            Console.WriteLine($"\n  Результат - Player: {playerScore}, Computer: {computerScore}");

            if(playerScore > computerScore)
            {
                PrintResult("Вы выиграли");
                OnWinInvoke();
            }
            else if(computerScore > playerScore)
            {
                PrintResult("Вы проиграли!");
                OnLoseInvoke();
            }
            else
            {
                PrintResult("Ничья!");
                OnDrawInvoke();
            }
        }

        protected override void FactoryMethod()
        {
            _diceCollection = new List<Dice>(_numberOfDice);

            for(int i = 0; i < _numberOfDice; i++)
            {
                _diceCollection.Add(new Dice(_minValue, _maxValue));
            }
        }
        private int RollDice(string playerName)
        {
            Console.WriteLine($"\n{playerName} бросает:");
            int total = 0;

            foreach(var dice in _diceCollection)
            {
                int roll = dice.Number;
                Console.WriteLine($"выпало: {roll}");
                total += roll;
            }

            Console.WriteLine($"Всего: {total}");
            return total;
        }
    }
}
