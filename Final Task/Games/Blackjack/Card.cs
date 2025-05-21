using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Final_Task.Models.Enums;

namespace Final_Task.Games.Blackjack
{
    /// <summary>
    /// Структура карты
    /// </summary>
    public struct Card
    {
        /// <summary>
        /// Масть
        /// </summary>
        public CardSuit Suit { get; }
        /// <summary>
        /// Достоинство
        /// </summary>
        public CardRank Rank { get; }

        public Card(CardSuit suit, CardRank rank)
        {
            Suit = suit;
            Rank = rank;
        }
    }
}
