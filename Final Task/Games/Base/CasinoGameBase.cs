using System;

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

        public abstract void PlayGame();

        protected abstract void FactoryMethod();
        protected virtual void InvokeDraw()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            _onDraw?.Invoke();
            Console.ResetColor();
        }

        protected virtual void InvokeLose()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            _onLose?.Invoke();
            Console.ResetColor();
        }

        protected virtual void InvokeWin()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            _onWin?.Invoke();
            Console.ResetColor();
        }
        protected void PrintResult(string result)
        {
            Console.WriteLine($"Game result: {result}");
        }
    }
}