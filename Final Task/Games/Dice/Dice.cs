using Final_Task.Exeptions;
using Final_Task.Utilites;

namespace Final_Task.Games.Dice
{
    /// <summary>
    /// Структура игральной кости
    /// </summary>
    public struct Dice
    {
        private readonly int _max;
        private readonly int _min;
        public Dice(int min, int max)
        {
            if(min < 1 || max > int.MaxValue || min > max)
                throw new WrongDiceNumberException(min > max ? max : min, 1, int.MaxValue);

            _min = min;
            _max = max;
        }

        /// <summary>
        /// Выпавшее число
        /// </summary>
        public int Number => RandomProvider.Next(_min, _max + 1);
    }
}
