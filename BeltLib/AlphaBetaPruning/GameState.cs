using BeltLib.Core;
using System.Collections.Generic;

namespace BeltLib.AlphaBetaPruning
{
    internal record GameState(Card[] CardsInHand, Card[] PossibleEnemyCards, Card[] CardsOnField, List<Card> PossibleCards);
}