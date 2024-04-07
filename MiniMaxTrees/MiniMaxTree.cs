using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniMaxTrees
{
    public class Node<TGame> where TGame : IGameState<TGame>
    {
        public Node<TGame>[] Children;
        public IGameState<TGame> game;

        public Node(IGameState<TGame> Game)
        {
            game = Game;
        }
    }

    //winning +1, losing -1

    public class MiniMaxTree<TGame> where TGame : IGameState<TGame>
    {
        protected virtual IGameState<TGame> Current { get; }

        Random rand = new Random();

        public IGameState<TGame> OptimalMove(bool isMax)
        {
            //eval moves - evaluates moves with minmax function
            var evalMoves = Current.GetChildren().ToList().Select(move => (state: move, value: Minimax(move, !isMax))).ToArray();

            //ranked moves - ranks moves based on turn (isMax : high-low, !isMax : low-high)
            var rankMoves = isMax ? evalMoves.OrderByDescending(move => move.value)  : evalMoves.OrderBy(move => move.value);

            //optimal moves - select all moves with value equal to the first
            var optMoves = rankMoves.Where(moves => (rankMoves.First().value == moves.value)).ToList();

            return optMoves[rand.Next(optMoves.Count)].state;
        }
        public int Minimax(IGameState<TGame> state, bool isMax, int min = int.MinValue, int max = int.MaxValue, int depth = 0)
        {
            //take in a game state, but use NODES to propagate the tree. during my recursion, my state parameter will be derived from the next child
            // store THE VALUE in the node, seperately from the state...?

            if (!state.isTerminal)
            {
                return state.Value;
            }

            if (isMax)
            {
                int value = int.MinValue;

                foreach (var move in state.GetChildren())
                {
                    value = Math.Max(value, Minimax(move, false, min, max, depth + 1));
                    min = Math.Max(min, value);
                    if (min >= max)
                    {
                        break;
                    }
                }

                return value;
            }
            else
            {
                int value = int.MaxValue;

                foreach (var move in state.GetChildren())
                {
                    value = Math.Min(value, Minimax(move, true, min, max, depth + 1));
                    max = Math.Min(max, value);
                    if (min >= max)
                    {
                        break;
                    }
                }

                return value;
            }
        }
    }
}
