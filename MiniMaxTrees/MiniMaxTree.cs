using System;
using System.Collections.Generic;
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
        public int Minimax(IGameState<TGame> state, bool isMax, int min = int.MinValue, int max = int.MaxValue, int depth = 0)
        {
            //take in a game state, but use NODES to propagate the tree. during my recursion, my state parameter will be derived from the next child
            // store THE VALUE in the node, seperately from the state...?

            if (state.MyState != GameState.IsPlaying)
            {
                return state.Value;
            }

            if (isMax)
            {
                int value = int.MinValue;

                foreach (var move in state.GetChildren())
                {
                    value = Math.Max(value, Minimax(move, !isMax, min, max, depth + 1));
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
