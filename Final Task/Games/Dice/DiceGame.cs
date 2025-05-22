using System;
using System.Collections.Generic;

using Final_Task.Games.Base;
using Final_Task.Utilites;

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
            PrintResult.Info($"Количество костей {_numberOfDice} значения ({_minValue}-{_maxValue})");

            byte playerScore = RollDice("Player");
            byte computerScore = RollDice("Computer");

            PrintResult.Info($"\n  Результат - Player: {playerScore}, Computer: {computerScore}");

            if(playerScore > computerScore)
            {
                Win();
            }
            else if(computerScore > playerScore)
            {
                Lose();
            }
            else
            {
                Draw();
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
            PrintResult.Info($"\n{playerName} бросает:");
            byte total = 0;

            foreach(var dice in _diceCollection)
            {
                byte roll = dice.Number;
                PrintResult.Info($"выпало: {roll}");
                total += roll;
            }

            PrintResult.Info($"Всего: {total}");
            return total;
        }
    }
}
