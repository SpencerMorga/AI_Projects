using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace MiniMaxTrees
{
    public class Square
    {
        int position;
        bool IsOccupied;
        public Square(int position)
        {
            this.position = position;
            IsOccupied = false;
        }
    }
    public class King
    {
        Square currentSquare;
        enum Color
        { 
            White,
            Black
        }

        public King(int color)
        {
            currentSquare = new Square(color == 0 ? 8 : 1);
        }
    }
    public class Knight
    {

    }
    public class Rook
    {
        
    }
    public class OneDChess : IGameState<OneDChess>
    {





        public Node<OneDChess>[] GetChildren()
        {
            
        }
    }
}
