using System.Collections.Generic;
using BeltLib.Core;

namespace BeltLib.AlphaBetaPruning
{
    internal record GameState(Card[] CardsInHand, Card[] PossibleEnemyCards, Card[] CardsOnField, List<Card> PossibleCards);
}