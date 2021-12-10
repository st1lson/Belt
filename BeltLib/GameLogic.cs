using BeltLib.Core;
using System.Collections.Generic;

namespace BeltLib
{
    public class GameLogic
    {
        public List<Card> Cards { get; }
        private readonly Deck _deck;

        public GameLogic()
        {
            _deck = new Deck();
        }

        public GameLogic(Deck deck)
        {
            _deck = deck;
        }
    }
}