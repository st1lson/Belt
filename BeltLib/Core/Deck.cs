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

        internal void InitializeDeck()
        {
            foreach (CardType type in Enum.GetValues(typeof(CardType)))
            {
                foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                {
                    DeckCards.Add(new Card(suit, type));
                }
            }
        }
    }
}
