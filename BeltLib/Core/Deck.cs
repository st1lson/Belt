using BeltLib.Enums;
using System;
using System.Collections.Generic;

namespace BeltLib.Core
{
    public sealed class Deck
    {
        public List<Card> DeckCards { get; }

        public Deck()
        {
            DeckCards = new List<Card>();
        }

        public void Shuffle()
        {
            Random random = new Random();
            int n = DeckCards.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                (DeckCards[k], DeckCards[n]) = (DeckCards[n], DeckCards[k]);
            }
        }

        internal void InitializeDeck()
        {
            foreach (CardType type in Enum.GetValues(typeof(CardType)))
            {
                foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                {
                    DeckCards.Add(new Card(suit, type));
                }
            }

            Shuffle();
        }
    }
}
