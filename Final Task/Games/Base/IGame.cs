using System;

namespace Final_Task.Games.Base
{
    public interface IGame
    {
        event Action OnDraw;

        event Action OnLose;

        event Action OnWin;
        void PlayGame();
    }
}