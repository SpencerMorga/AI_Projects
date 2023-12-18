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
        
        public Node(OneDChess game)
        {
            chess = game;
        }
    }
    public class MiniMaxTree<OneDChess>
    {
        public Node<OneDChess> root;

        public MiniMaxTree(Node<OneDChess> Root)
        {
            root = Root;
        }
    }
}
