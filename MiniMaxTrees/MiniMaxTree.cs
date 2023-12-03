using System;
using System.Collections.Generic;
using System.Text;

namespace MiniMaxTrees
{
    public class Node<OneDChess>
    {
        public Node<OneDChess>[] Children;
        public Node<OneDChess> Parent;
        public OneDChess chess;
        
        
        public Node(OneDChess Chess)
        {
            chess = Chess;
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
