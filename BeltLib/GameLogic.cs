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
            Cards = deck.DeckCards;
            _deck = deck;
        }

        public Card SelectCard(List<Card> botCards, List<Card> fieldCards, List<Card> possibleCards)
        {
            if (botCards.Count <= 0)
            {
                return default;
            }

            GameState state = new(botCards.ToArray(), new Card[4], fieldCards.ToArray(), possibleCards);
            GameTree<GameState> gameTree = new(GenerateChildren, GeneratePossibleHandCards, GetCard, possibleCards);
            GameState bestState = gameTree.GetTheBest(state);

            return GetBestCard(state, bestState);
        }

        private Card GetCard() => _deck.DeckCards.FirstOrDefault();

        private static GameState[] GenerateChildren(GameState state)
        {
            var (cardsInHand, possibleEnemyCards, cardsOnField, possibleCards) = state;
            GameState[] children = new GameState[cardsInHand.Length];
            for (int i = 0; i < cardsInHand.Length; i++)
            {
                Card card = cardsInHand[i];
                int index = Array.IndexOf(cardsInHand, card);
                Card[] newFieldCards = new Card[cardsOnField.Length + 1];
                Array.Copy(cardsOnField, newFieldCards, cardsOnField.Length);
                Card[] newHandCards = cardsInHand.Where(x => x != cardsInHand[index]).ToArray();
                newFieldCards[^1] = card;
                children[i] = new GameState(newHandCards, possibleEnemyCards, newFieldCards, possibleCards);
            }

            return children;
        }

        private static GameState[] GeneratePossibleHandCards(GameState state)
        {
            var (cardsInHand, _, cardsOnField, possibleCards) = state;
            GameState[] possibleHandCards = new GameState[100];
            Random random = new();
            for (int i = 0; i < possibleHandCards.Length; i++)
            {
                Card[] cards = new Card[4];
                Card[] possibleCard = possibleCards.ToArray();
                for (int j = 0; j < cards.Length; j++)
                {
                    int index = random.Next(possibleCard.Length);
                    Card card = possibleCard[index];
                    while (cards.Contains(card) || possibleCard[index] is null)
                    {
                        index = random.Next(possibleCard.Length);
                        card = possibleCard[index];
                    }

                    cards[j] = card;
                    possibleCard[index] = null;
                }

                possibleHandCards[i] = new GameState(cardsInHand, cards, cardsOnField, possibleCards);
            }

            return possibleHandCards;
        }

        private static Card GetBestCard(GameState initialState, GameState state)
        {
            return initialState.CardsInHand.FirstOrDefault(card => state.CardsOnField.Contains(card));
        }
    }
}