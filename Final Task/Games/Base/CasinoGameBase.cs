using System;

namespace Final_Task.Games.Base
{
    public abstract class CasinoGameBase : IGame
    {
        public event Action OnDraw;

        public event Action OnLose;

        public event Action OnWin;
        public abstract void PlayGame();

        protected abstract void FactoryMethod();

        protected virtual void OnDrawInvoke() => OnDraw?.Invoke();

        protected virtual void OnLoseInvoke() => OnLose?.Invoke();

        protected virtual void OnWinInvoke() => OnWin?.Invoke();
        protected void PrintResult(string result)
        {
            Console.WriteLine($"Game result: {result}");
        }
    }
}