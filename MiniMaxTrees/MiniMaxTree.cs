using System;
using System.Collections.Generic;
using System.Text;

namespace MiniMaxTrees
{
    public class Node<TGameState> where TGameState : IGameState<TGameState>
    {
        public Node<TGameState>[] Children;
        public Node<TGameState> Parent;
        IGameState<TGameState> gameState;
        int value;
        
        public Node(int Value, IGameState<TGameState> GameState)
        {
            value = Value;
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

        public void Add()
    }
}
