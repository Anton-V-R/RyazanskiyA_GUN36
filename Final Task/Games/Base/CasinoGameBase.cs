using System;

using Final_Task.Utilites;

namespace Final_Task.Games.Base
{
    public abstract class CasinoGameBase : IGame
    {
        private Action _onDraw;

        private Action _onLose;

        private Action _onWin;

        public event Action OnDraw
        {
            add => _onDraw += value;
            remove => _onDraw -= value;
        }

        public event Action OnLose
        {
            add => _onLose += value;
            remove => _onLose -= value;
        }

        public event Action OnWin
        {
            add => _onWin += value;
            remove => _onWin -= value;
        }

        public virtual void Draw()
        {
            PrintResult.ColorInfo("Ничья!", ConsoleColor.Yellow);
            _onDraw?.Invoke();
        }

        public virtual void Lose()
        {
            PrintResult.ColorInfo("Вы проиграли!", ConsoleColor.Magenta);
            _onLose?.Invoke();
        }

        public abstract void PlayGame();

        public virtual void Win()
        {
            PrintResult.ColorInfo("Вы выиграли", ConsoleColor.Green);
            _onWin?.Invoke();
        }

        protected abstract void FactoryMethod();
    }
}