using BeltLib.Core;
using System.Collections.Generic;
using System.Linq;
using BeltLib.AlphaBetaPruning;

namespace BeltLib
{
    public class GameLogic
    {
        public List<Card> Cards { get; }
        private readonly Deck _deck;

        public GameLogic(Deck deck)
        {
            _deck = deck;
            Cards = _deck.DeckCards;
        }

        public Card SelectCard(List<Card> botCards, List<Card> fieldCards)
        {
            if (botCards.Count < 1)
            {
                return default;
            }

            GameState state = new(botCards, fieldCards);
            GameTree<GameState> gameTree = new(GenerateChildren, GeneratePossibleHandCards, GetCard);

            GameState bestState = gameTree.GetTheBest(state);

            return SelectCard(state, bestState);
        }

        private Card GetCard() => _deck.DeckCards.FirstOrDefault();

        private static List<GameState> GenerateChildren(GameState state)
        {
            return default;
        }

        private static List<GameState> GeneratePossibleHandCards(GameState state)
        {
            return default;
        }

        private static Card SelectCard(GameState initialState, GameState state)
        {
            return initialState.CardsInHand.FirstOrDefault(card => state.CardsOnField.Contains(card));
        }
    }
}