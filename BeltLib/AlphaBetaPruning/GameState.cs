using System.Collections.Generic;
using BeltLib.Core;

namespace BeltLib.AlphaBetaPruning
{
    internal record GameState(List<Card> CardsInHand, List<Card> CardsOnField);
}