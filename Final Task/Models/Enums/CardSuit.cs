using System;
using System.ComponentModel;

namespace Final_Task.Models.Enums
{
    /// <summary>
    /// Перечисление мастей карт
    /// </summary>
    public enum CardSuit : byte
    {
        [Description("♦")]
        Diamonds,   // Бубны
        [Description("♥")]
        Hearts,     // Червы
        [Description("♠")]
        Spades,     // Пики
        [Description("♣")]
        Clubs       // Трефы
    }

}
