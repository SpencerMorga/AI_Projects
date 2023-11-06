using System;
using System.Collections.Generic;
using System.Text;

namespace MiniMaxTrees
{
    public class Node<TGameState> where TGameState : IGameState<TGameState>
    {
        public Node<TGameState>[] Children;
        public Node<TGameState> Parent;
        public IGameState<TGameState> gameState;
        
        public Node(IGameState<TGameState> GameState)
        {
            gameState = GameState;
        }
    }
    public class MiniMaxTree<TGameState> where TGameState : IGameState<TGameState>
    {
        public Node<TGameState> root;

        public MiniMaxTree(Node<TGameState> Root)
        {
            root = Root;
        }
    }
}
