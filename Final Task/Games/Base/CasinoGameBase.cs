using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace Final_Task.Games.Base
{
    public abstract class CasinoGameBase : IGame
    {
        public event Action OnWin;
        public event Action OnLose;
        public event Action OnDraw;

        public abstract void PlayGame();

        protected virtual void OnWinInvoke() => OnWin?.Invoke();
        protected virtual void OnLoseInvoke() => OnLose?.Invoke();
        protected virtual void OnDrawInvoke() => OnDraw?.Invoke();

        protected abstract void FactoryMethod();

        protected void PrintResult(string result)
        {
            Console.WriteLine($"Game result: {result}");
        }
    }
}