using System;

namespace Final_Task.Exeptions
{
    /// <summary>
    /// Кастомное исключение для некорректных значений кости
    /// </summary>
    public class WrongDiceNumberException : Exception
    {
        public WrongDiceNumberException(int number, int min, int max)
            : base($"Некорректное значение кости: {number}. Допустимый диапазон: от {min} до {max}")
        {
        }
    }
}
