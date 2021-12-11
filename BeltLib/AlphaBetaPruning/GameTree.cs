using System;
using System.Collections.Generic;
using System.Linq;
using BeltLib.Core;

namespace BeltLib.AlphaBetaPruning
{
    internal sealed class GameTree<T> where T : GameState
    {
        public readonly Func<T, List<T>> GenerateChildren;
        public readonly Func<T, List<T>> GeneratePossibleHandCards;
        public readonly Func<Card> GetCard;
        public GameTree(Func<T, List<T>> generateChildren, Func<T, List<T>> generatePossibleHandCards, Func<Card> getCard)
        {
            GenerateChildren = generateChildren;
            GeneratePossibleHandCards = generatePossibleHandCards;
            GetCard = getCard;
        }

        public T GetTheBest(T state, bool maximize = true)
        {
            List<T> children = GenerateChildren(state);

            if (children.Count == 0)
            {
                return default;
            }

            T bestState = children[0];
            foreach (T child in children)
            {
                int value = MiniMax(child, 1, !maximize);
                if (maximize && value > Evaluate(child))
                {
                    bestState = child;
                }

                if (!maximize && value < Evaluate(child))
                {
                    bestState = child;
                }
            }

            return bestState;
        }

        private int MiniMax(T state, int depth, bool maximize, int alpha = int.MaxValue, int beta = int.MinValue)
        {
            List<T> children = GenerateChildren(state);
            if (depth <= 0 || children.Count == 0)
            {
                return Evaluate(state);
            }

            return default;
        }

        private static int Evaluate(T state)
        {
            return default;
        }
    }
}
