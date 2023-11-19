using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Diagnostics.SymbolStore;
using System.Text;
using static MiniMaxTrees.IGameState<MiniMaxTrees.OneDChess>;

namespace MiniMaxTrees
{
    public class OneDChess : IGameState<OneDChess> 
    {
        // FORWARD IS DEFINED AS THE NEGATIVE Y DIRECTION ON THE BOARD, WITH THE ONE SQUARE AT THE TOP AND THE EIGHT SQUARE AT THE BOTTOM
        Pieces[] board = new Pieces[8];
        GameState current = new GameState();
        List<KeyValuePair<Pieces, int>> moves = new List<KeyValuePair<Pieces, int>>();
        public enum Pieces : byte
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

        public void GetValidKnightMoves(int currentPosition, Pieces piece)
        {
            if (IsMoveValid(currentPosition + 2, piece)) moves.Add(new KeyValuePair<Pieces, int>(piece, currentPosition + 2));
            if (IsMoveValid(currentPosition - 2, piece)) moves.Add(new KeyValuePair<Pieces, int>(piece, currentPosition - 2));
        }

        public void GetValidKingMoves(int currentPosition, Pieces piece) 
        {
            if (IsMoveValid(currentPosition + 1, piece)) moves.Add(new KeyValuePair<Pieces, int>(piece, currentPosition + 2));
            if (IsMoveValid(currentPosition - 1, piece)) moves.Add(new KeyValuePair<Pieces, int>(piece, currentPosition - 2));
        }

        public void ValidRookMoves(int currentPosition, Pieces piece)
        { 
            int i = currentPosition;
            while (board[i] > 0)
            {
                if(IsMoveValid(i, piece)) moves.Add(new KeyValuePair<Pieces, int>(piece, i));
                i++;
            }

            int j = currentPosition;
            while (board[j] > 0)
            {
                if (IsMoveValid(j, piece)) moves.Add(new KeyValuePair<Pieces, int>(piece, j));
                j++;
            }
        }

        public bool IsMoveValid(int positionToMoveTo, Pieces piece)
        {
            return board[positionToMoveTo] == 0 || ((board[positionToMoveTo] > 0) && GetColor((byte)piece, 3) != GetColor((byte)board[positionToMoveTo], 3));
        }

        private bool GetColor(byte byteToConvert, int bitToReturn)
        {
            int mask = 1 << bitToReturn;
            return (byteToConvert & mask) == mask;
        }


    }
}
