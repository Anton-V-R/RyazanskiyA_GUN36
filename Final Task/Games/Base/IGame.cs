using System;

namespace Final_Task.Games.Base
{
    public interface IGame
    {
        //event Action OnDraw;

        //event Action OnLose;

        //event Action OnWin;

        event EventHandler GameWon;
        event EventHandler GameLost;
        event EventHandler GameDrawn;

        void PlayGame();
    }
}