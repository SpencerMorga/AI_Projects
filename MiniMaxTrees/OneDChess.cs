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
            if (position - 2 < 0) //if cannot move behind
            {
                if (board[position+2] == 0 && GetColor((byte)board[position+2], 3) == GetColor((byte)piece, 3)) //if forward space is taken and is same color
                {
                    return new int[] { 0 };
                }
            }
            else
            {
                if (board[position-2] == 0)
                {
                    if (board[position + 2] == 0)
                    {
                        return new int[] { 0 };
                    }
                }
            }
        }

        private bool GetColor(byte byteToConvert, int bitToReturn)
        {
            int mask = 1 << bitToReturn;
            return (byteToConvert & mask) == mask;
        }


    }
}
