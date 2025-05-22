using System;

namespace Final_Task.Games.Base
{
    public abstract class CasinoGameBase : IGame
    {
        public abstract void PlayGame();

        protected abstract void FactoryMethod();

        public event EventHandler GameWon;
        public event EventHandler GameLost;
        public event EventHandler GameDrawn;

        protected virtual void OnGameWon()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            GameWon?.Invoke(this, EventArgs.Empty);
            Console.ResetColor();
        }

        protected virtual void OnGameLost()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            GameLost?.Invoke(this, EventArgs.Empty);
            Console.ResetColor();
        }

        protected virtual void OnGameDrawn()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            GameDrawn?.Invoke(this, EventArgs.Empty);
            Console.ResetColor();
        }

        protected void PrintResult(string result)
        {
            Console.WriteLine($"Game result: {result}");
        }
    }
}