using System;
using System.Collections.Generic;
using System.Linq;

using Final_Task.Games.Base;
using Final_Task.Models.Enums;
using Final_Task.Utilites;

namespace Final_Task.Games.Blackjack
{
    public class BlackjackGame : CasinoGameBase
    {
        private readonly List<Card> _computerCards;
        private readonly int _numberOfCards;
        private readonly List<Card> _playerCards;
        private Queue<Card> _deck;
        
        /// <summary>
        /// Колода карт
        /// </summary>
        /// <param name="numberOfCards">Количество карт в колоде</param>
        /// <exception cref="ArgumentException"></exception>
        public BlackjackGame(int numberOfCards)
        {
            if(numberOfCards < 4)
                throw new ArgumentException("Minimum 4 cards required", nameof(numberOfCards));

            _numberOfCards = numberOfCards;
            _playerCards = new List<Card>();
            _computerCards = new List<Card>();

            FactoryMethod();
        }

        public override void PlayGame()
        {
            Console.WriteLine("Starting Blackjack game...");
            PrintCards();

            int playerScore = CalculateScore(_playerCards);
            int computerScore = CalculateScore(_computerCards);

            Console.WriteLine($"Your score: {playerScore}");
            Console.WriteLine($"Computer score: {computerScore}");

            while(playerScore == computerScore && playerScore < 21 && _deck.Count > 0)
            {
                Console.WriteLine("Tie! Dealing additional cards...");
                _playerCards.Add(_deck.Dequeue());
                _computerCards.Add(_deck.Dequeue());

                playerScore = CalculateScore(_playerCards);
                computerScore = CalculateScore(_computerCards);

                PrintCards();
                Console.WriteLine($"Your score: {playerScore}");
                Console.WriteLine($"Computer score: {computerScore}");
            }

            DetermineWinner(playerScore, computerScore);
        }

        protected override void FactoryMethod()
        {
            CreateDeck();
            Shuffle();
            DealInitialCards();
        }

        private int CalculateScore(List<Card> cards)
        {
            int score = 0;
            int aces = 0;

            foreach(var card in cards)
            {
                if(card.Rank >= CardRank.Jack && card.Rank <= CardRank.King)
                    score += 10;
                else if(card.Rank == CardRank.Ace)
                {
                    score += 11;
                    aces++;
                }
                else
                    score += (int)card.Rank;
            }

            while(score > 21 && aces > 0)
            {
                score -= 10;
                aces--;
            }

            return score;
        }

        private void CreateDeck()
        {
            var cards = new List<Card>();

            for(int i = 0; i < _numberOfCards; i++)
            {
                var suit = (CardSuit)RandomProvider.Next(0, 4);
                var rank = (CardRank)RandomProvider.Next(6, 14);
                cards.Add(new Card(suit, rank));
            }

            _deck = new Queue<Card>(cards);
        }

        private void DealInitialCards()
        {
            if(_deck.Count > 0)
            {
                _playerCards.Add(_deck.Dequeue());
                _playerCards.Add(_deck.Dequeue());
                _computerCards.Add(_deck.Dequeue());
                _computerCards.Add(_deck.Dequeue());
            }

        }

        private void DetermineWinner(int playerScore, int computerScore)
        {
            if(playerScore > 21 && computerScore > 21)
            {
                PrintResult("Both players bust");
                OnDrawInvoke();
            }
            else if(playerScore > 21)
            {
                PrintResult("You bust");
                OnLoseInvoke();
            }
            else if(computerScore > 21)
            {
                PrintResult("Computer bust");
                OnWinInvoke();
            }
            else if(playerScore > computerScore)
            {
                PrintResult("You win");
                OnWinInvoke();
            }
            else if(computerScore > playerScore)
            {
                PrintResult("Computer wins");
                OnLoseInvoke();
            }
            else
            {
                PrintResult("Push");
                OnDrawInvoke();
            }
        }

        private void PrintCards()
        {
            Console.WriteLine("\nYour cards:");
            foreach(var card in _playerCards)
                Console.WriteLine($"{card.Rank} of {card.Suit}");

            Console.WriteLine("\nComputer cards:");
            foreach(var card in _computerCards)
                Console.WriteLine($"{card.Rank} of {card.Suit}");
        }

        private void Shuffle()
        {
            var shuffled = _deck.OrderBy(x => RandomProvider.Next(0, int.MaxValue)).ToList();
            _deck = new Queue<Card>(shuffled);
        }
    }
}
