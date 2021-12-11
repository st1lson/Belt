#nullable enable
using BeltLib.Enums;
using System.Drawing;

namespace BeltLib.Core
{
    public class Card
    {
        public Suit Suit { get; }
        public CardType Type { get; }
        public Bitmap? CardFace { get; }
        public Bitmap? CardBack { get; }

        public Card(Suit suit, CardType type)
        {
            Suit = suit;
            Type = type;
        }

        public Card(Suit suit, CardType type, Bitmap cardFace, Bitmap cardBack)
        {
            Suit = suit;
            Type = type;
            CardFace = cardFace;
            CardBack = cardBack;
        }

        public override string ToString()
        {
            return $"{Type} {Suit}";
        }
    }
}
