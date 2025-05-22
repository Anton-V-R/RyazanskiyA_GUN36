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
        private readonly byte _numberOfCards;
        private const byte _minCardSuit = 0;
        private const byte _maxCardSuit = 4;
        private const byte _minCardRank = 6;
        private const byte _maxCardRank = 14;
        private const byte _winScore = 21;
        private const byte _deltaScore = 10;
        private const byte _minAces = 0;

        private readonly List<Card> _playerCards;
        private Queue<Card> _deck;
        
        /// <summary>
        /// Колода карт
        /// </summary>
        /// <param name="numberOfCards">Количество карт в колоде</param>
        /// <exception cref="ArgumentException"></exception>
        public BlackjackGame(byte numberOfCards)
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

            byte playerScore = CalculateScore(_playerCards);
            byte computerScore = CalculateScore(_computerCards);

            Console.WriteLine($"Your score: {playerScore}");
            Console.WriteLine($"Computer score: {computerScore}");

            while(playerScore == computerScore && playerScore < _winScore && _deck.Count > 0)
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

        private byte CalculateScore(List<Card> cards)
        {
            byte score = 0;
            byte aces = 0;

            foreach(var card in cards)
            {
                if(card.Rank == CardRank.Ace)
                {
                    aces++;
                }

                score += (byte)card.Rank;
            }

            while(score > _winScore && aces > _minAces)
            {
                score -= _deltaScore;
                aces--;
            }

            return score;
        }

        private void CreateDeck()
        {
            var cards = new List<Card>();

            for(byte i = 0; i < _numberOfCards; i++)
            {
                var suit = (CardSuit)RandomProvider.Next(_minCardSuit, _maxCardSuit);
                var rank = (CardRank)RandomProvider.Next(_minCardRank, _maxCardRank);
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

        private void DetermineWinner(byte playerScore, byte computerScore)
        {
            if(playerScore > _winScore && computerScore > _winScore)
            {
                PrintResult("Both players bust");
                InvokeDraw();
            }
            else if(playerScore > _winScore)
            {
                PrintResult("You bust");
                InvokeLose();
            }
            else if(computerScore > _winScore)
            {
                PrintResult("Computer bust");
                InvokeWin();
            }
            else if(playerScore > computerScore)
            {
                PrintResult("You win");
                InvokeWin();
            }
            else if(computerScore > playerScore)
            {
                PrintResult("Computer wins");
                InvokeLose();
            }
            else
            {
                PrintResult("Push");
                InvokeDraw();
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
