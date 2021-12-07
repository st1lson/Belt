using BeltLib.Enums;

namespace BeltLib.Core
{
    public sealed class Card
    {
        public Suit Suit { get; }
        public CardType Type { get; }

        public Card(Suit suit, CardType type)
        {
            Suit = suit;
            Type = type;
        }
    }
}
