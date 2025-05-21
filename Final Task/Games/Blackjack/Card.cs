using Final_Task.Models.Enums;

namespace Final_Task.Games.Blackjack
{
    /// <summary>
    /// Структура карты
    /// </summary>
    public struct Card
    {
        public Card(CardSuit suit, CardRank rank)
        {
            Suit = suit;
            Rank = rank;
        }

        /// <summary>
        /// Достоинство
        /// </summary>
        public CardRank Rank { get; }

        /// <summary>
        /// Масть
        /// </summary>
        public CardSuit Suit { get; }
    }
}
