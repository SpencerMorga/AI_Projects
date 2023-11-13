using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using static MiniMaxTrees.IGameState<MiniMaxTrees.OneDChess>;

namespace MiniMaxTrees
{
    public class OneDChess : IGameState<OneDChess>
    {
        Pieces[] board = new Pieces[8];
        GameState current = new GameState();
        enum Pieces : byte
        { 
            Knight = 1,
            Rook = 2,
            King = 3,
            IsWhite = 4
        }

        GameState MyState
        {
            get
            {
                if (current =)
            } 
        }

        public Node<OneDChess>[] GetChildren()
        {
            //gets all possible moves as their own positions
            for (int i = 0; i < board.Length; i++)
            {
                
            }
        }

        public int[] ValidKnightMoves(int position, Pieces piece)
        {
            int forwards = position + 2;
            int backwards = position - 2;
            return board[forwards] > 0 ? GetColor((byte)piece, 3) == GetColor((byte)board[forwards], 3) ?  :
        } // array of places you can, 

        

        private bool GetColor(byte byteToConvert, int bitToReturn)
        {
            int mask = 1 << bitToReturn;
            return (byteToConvert & mask) == mask;
        }


    }
}
