using BeltLib.Core;
using System;

namespace BeltLib.AlphaBetaPruning
{
    internal sealed class GameTree<T> where T : GameState
    {
        public readonly Func<T, T[]> GenerateChildren;
        public readonly Func<T, T[]> GeneratePossibleHandCards;
        public readonly Func<Card> GetCard;
        public GameTree(Func<T, T[]> generateChildren, Func<T, T[]> generatePossibleHandCards, Func<Card> getCard)
        {
            GenerateChildren = generateChildren;
            GeneratePossibleHandCards = generatePossibleHandCards;
            GetCard = getCard;
        }

        public T GetTheBest(T state, bool maximize = true)
        {
            T[] children = GenerateChildren(state);
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
            T[] children = GenerateChildren(state);
            if (depth <= 0 || children.Length == 0)
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
