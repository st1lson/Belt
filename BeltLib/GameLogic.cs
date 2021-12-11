using BeltLib.AlphaBetaPruning;
using BeltLib.Core;
using System;
using System.Collections.Generic;
using System.Linq;

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

            GameState state = new(botCards.ToArray(), fieldCards.ToArray());
            GameTree<GameState> gameTree = new(GenerateChildren, GeneratePossibleHandCards, GetCard);

            GameState bestState = gameTree.GetTheBest(state);

            return SelectCard(state, bestState);
        }

        private Card GetCard() => _deck.DeckCards.FirstOrDefault();

        private static GameState[] GenerateChildren(GameState state)
        {
            GameState[] children = new GameState[state.CardsInHand.Length];
            (Card[] handCards, Card[] fieldCards) = state;
            for (int i = 0; i < handCards.Length; i++)
            {
                Card card = handCards[i];
                int index = Array.IndexOf(handCards, card);
                Card[] newHandCards = new Card[handCards.Length];
                Card[] newFieldCards = new Card[fieldCards.Length + 1];
                Array.Copy(handCards, newHandCards, handCards.Length);
                Array.Copy(fieldCards, newFieldCards, fieldCards.Length);
                newHandCards = newHandCards.Where(x => x != newHandCards[index]).ToArray();
                newFieldCards[^1] = card;
                children[i] = new GameState(newHandCards, newFieldCards);
            }

            return children;
        }

        private static GameState[] GeneratePossibleHandCards(GameState state)
        {
            return default;
        }

        private static Card SelectCard(GameState initialState, GameState state)
        {
            return initialState.CardsInHand.FirstOrDefault(card => state.CardsOnField.Contains(card));
        }
    }
}