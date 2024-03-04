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
    public class MiniMaxTree<TGame> where TGame 
    {
        public int Minimax(IGameState<TGame>)
    }
}
