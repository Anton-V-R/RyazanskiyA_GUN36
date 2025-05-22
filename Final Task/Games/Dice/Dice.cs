using Final_Task.Exeptions;
using Final_Task.Utilites;

namespace Final_Task.Games.Dice
{
    /// <summary>
    /// Структура игральной кости
    /// </summary>
    public readonly struct Dice
    {
        private readonly byte _max;
        private readonly byte _min;
        public Dice(byte min, byte max)
        {
            if(min < 1 || max > byte.MaxValue || min > max)
                throw new WrongDiceNumberException(min > max ? max : min, 1, byte.MaxValue);

            _min = min;
            _max = max;
        }

        /// <summary>
        /// Выпавшее число
        /// </summary>
        public byte Number => (byte)RandomProvider.Next(_min, _max + 1);
    }
}
