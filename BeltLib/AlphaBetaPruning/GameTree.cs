using BeltLib.Core;
using BeltLib.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeltLib.AlphaBetaPruning
{
    internal sealed class GameTree<T> where T : GameState
    {
        public readonly Func<T, T[]> GenerateChildren;
        public readonly Func<T, T[]> GeneratePossibleHandCards;
        public readonly Func<Card> GetCard;
        public readonly List<Card> PossibleCards;
        public GameTree(Func<T, T[]> generateChildren, Func<T, T[]> generatePossibleHandCards, Func<Card> getCard, List<Card> possibleCards)
        {
            GenerateChildren = generateChildren;
            GeneratePossibleHandCards = generatePossibleHandCards;
            GetCard = getCard;
            PossibleCards = possibleCards;
        }

        public T GetTheBest(T state, bool maximize = true)
        {
            T[] children = GenerateChildren(state);
            if (children.Length is 0 or 1)
            {
                return children.Length is 0 ? default : children[0];
            }

            T[] possibleHands = GeneratePossibleHandCards(state);
            T bestState = children[0];
            int bestValue = int.MinValue;
            foreach (T child in children)
            {
                int value = MiniMax(child, possibleHands, 2, !maximize);
                if (maximize && value > bestValue)
                {
                    bestValue = value;
                    bestState = child;
                }

                if (!maximize && value < bestValue)
                {
                    bestValue = value;
                    bestState = child;
                }
            }

            return bestState;
        }

        private int MiniMax(T state, T[] possibleHands, int depth, bool maximize, int alpha = int.MaxValue, int beta = int.MinValue)
        {
            if (!maximize)
            {
                possibleHands = GeneratePossibleHandCards(state);
            }

            if (depth <= 0)
            {
                return possibleHands.Sum(Evaluate);
            }

            T[] children = GenerateChildren(state);

            int value = maximize ? int.MinValue : int.MaxValue;

            foreach (T child in children)
            {
                int nextStateValue = MiniMax(child, possibleHands, depth - 1, !maximize, alpha, beta);
                if (maximize)
                {
                    value = Math.Max(value, nextStateValue);
                    alpha = Math.Max(alpha, nextStateValue);
                }
                else
                {
                    value = Math.Min(value, nextStateValue);
                    beta = Math.Min(beta, nextStateValue);
                }

                if (beta <= alpha)
                {
                    break;
                }
            }

            return value;
        }

        private static int Evaluate(T state)
        {
            int increase = 0;
            foreach (Card card in state.CardsInHand)
            {
                if (card is null)
                {
                    continue;
                }

                CardType cardType = card.Type;
                int counter = state.CardsInHand.Count(similarTypes => similarTypes.Type.Equals(cardType));
                if (counter < 3)
                {
                    break;
                }

                Dictionary<CardType, short> dictionary = new();
                foreach (Card fieldCard in state.CardsOnField)
                {
                    CardType fieldCardType = fieldCard.Type;
                    if (fieldCardType >= cardType)
                    {
                        continue;
                    }

                    if (dictionary.ContainsKey(fieldCardType))
                    {
                        dictionary[fieldCardType]++;
                    }
                    else
                    {
                        dictionary.Add(fieldCardType, 1);
                    }
                }

                List<Card> fieldCards = new();
                foreach (var (key, value) in dictionary)
                {
                    if (value < 3)
                    {
                        continue;
                    }

                    foreach (Card fieldCard in state.CardsOnField)
                    {
                        CardType fieldCardType = fieldCard.Type;
                        if (fieldCardType != key || fieldCards.Contains(fieldCard))
                        {
                            continue;
                        }

                        fieldCards.Add(fieldCard);
                        increase++;
                    }
                }
            }

            int decrease = 0;
            foreach (Card possibleEnemyCard in state.PossibleEnemyCards)
            {
                if (possibleEnemyCard is null)
                {
                    continue;
                }

                CardType cardType = possibleEnemyCard.Type;
                int counter = state.CardsInHand.Count(similarTypes => similarTypes.Type.Equals(cardType));
                if (counter < 3)
                {
                    break;
                }

                Dictionary<CardType, short> dictionary = new();
                foreach (Card fieldCard in state.CardsOnField)
                {
                    CardType fieldCardType = fieldCard.Type;
                    if (fieldCardType >= cardType)
                    {
                        continue;
                    }

                    if (dictionary.ContainsKey(fieldCardType))
                    {
                        dictionary[fieldCardType]++;
                    }
                    else
                    {
                        dictionary.Add(fieldCardType, 1);
                    }
                }

                List<Card> fieldCards = new();
                foreach (var (key, value) in dictionary)
                {
                    if (value < 3)
                    {
                        continue;
                    }

                    foreach (Card fieldCard in state.CardsOnField)
                    {
                        CardType fieldCardType = fieldCard.Type;
                        if (fieldCardType != key || fieldCards.Contains(fieldCard))
                        {
                            continue;
                        }

                        fieldCards.Add(fieldCard);
                        decrease++;
                    }
                }
            }

            return increase - decrease;
        }
    }
}
