using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Diagnostics.SymbolStore;
using System.Text;
using static MiniMaxTrees.OneDChess;

namespace MiniMaxTrees
{
    public class OneDChess
    {
        // FORWARD IS DEFINED AS THE NEGATIVE Y DIRECTION ON THE BOARD, WITH THE ONE SQUARE AT THE TOP AND THE EIGHT SQUARE AT THE BOTTOM

        Pieces[] board = new Pieces[8];
        GameState current = new GameState();
        List<KeyValuePair<Pieces, (int, int)>> moves = new List<KeyValuePair<Pieces, (int, int)>>();

        [Flags]
        enum GameState
        {
            Terminal = 0B100,
            Win = 0,
            Tie = 1,
            Loss = 0B10
        }

        [Flags]
        public enum Pieces : byte
        {
            Knight = 0B1,
            Rook = 0B10,
            King = 0B100,
            IsWhite = 0B1000
        }
        
        GameState MyState
        {
            get
            {
                if (current = )
            } 
        }

        public OneDChess(Pieces[] Board)
        {
            board = Board;
        }

        public Node<OneDChess>[] GetChildren()
        {
            //code to determine if terminal state
            // - determine if check
            // - if king's position a possible move of the rook
            // - AND no other move can possibly block the king or take the rook
            // - to do this....
                // MAKE ISINCHECK FUNCTION?
                // if isincheck
                // run through all possible moves
                // if the execution of any of those moves results in !isincheck, MAKE MOVE NO CHECKMATE LOL

            //and then make sure that moves aren't illegal and don't endager the king (HOW??????)
            //- to do this...
                // MAKE ISINCHECK FUNCTION
                // constantly check isincheck on own king when making moves
                // if !isincheck DO NOT MAKE MOVE LOL

            //gets all possible moves as their own positions
            Node<OneDChess>[] children = new Node<OneDChess>[moves.Count];

            for (int i = 0; i < moves.Count; i++)
            {
                Pieces[] newBoard = new Pieces[board.Length];
                CopyArray(board, newBoard);

                Move(newBoard, moves[i].Value.Item1, moves[i].Value.Item2);

                if (IsMoveACheck(moves[i].Value.Item2, moves[i].Key) && IsInCheck(newBoard[moves[i].Value.Item2]))
                {
                    for (int j = 0; j < moves.Count; j++)
                    {
                        if (moves[j].Key.HasFlag(Pieces.IsWhite) == board[moves[j].Value.Item2].HasFlag(Pieces.IsWhite))
                        {
                            Pieces[] testBoard = new Pieces[newBoard.Length];
                            CopyArray(newBoard, testBoard);

                            Move(testBoard, moves[j].Value.Item1, moves[j].Value.Item2); //MIGHT NEED TO CHANGE BOARD PARAMETER?
                            if (!IsInCheck(testBoard[moves[i].Value.Item2]))
                            {
                                //no checkmate
                            }
                        }
                    }
                    // yes checkmate
                }

                children[i] = new Node<OneDChess>(new OneDChess(newBoard));
            }

            return children;
        }

        public void CopyArray(Pieces[] A, Pieces[] B)
        {
            for (int i = 0; i < A.Length; i++)
            {
                B[i] = A[i];
            }
        }

        public void Move(Pieces[] board, int currentPosition, int newPosition) 
        {
            if (IsMoveValid(newPosition, board[currentPosition]) && !IsMoveACheck(newPosition, board[currentPosition]))
            {
                // DO NOT MAKE OWN KING DIE

                board[newPosition] = board[currentPosition];
                board[currentPosition] = 0;
            }
        }

        public void GetValidKnightMoves(int currentPosition, Pieces piece)
        {
            if (IsMoveValid(currentPosition + 2, piece)) moves.Add(new KeyValuePair<Pieces, (int, int)>(piece, (currentPosition, currentPosition + 2)));
            if (IsMoveValid(currentPosition - 2, piece)) moves.Add(new KeyValuePair<Pieces, (int, int)>(piece, (currentPosition, currentPosition - 2)));
        }

        public void GetValidKingMoves(int currentPosition, Pieces piece) 
        {
            if (IsMoveValid(currentPosition + 1, piece)) moves.Add(new KeyValuePair<Pieces, (int, int)>(piece, (currentPosition, currentPosition + 1)));
            if (IsMoveValid(currentPosition - 1, piece)) moves.Add(new KeyValuePair<Pieces, (int, int)>(piece, (currentPosition, currentPosition - 1)));
        }

        public void ValidRookMoves(int currentPosition, Pieces piece)
        { 
            int i = currentPosition+1;
            while (board[i] > 0)
            {
                if(IsMoveValid(i, piece)) moves.Add(new KeyValuePair<Pieces, (int, int)>(piece, (currentPosition, i)));
                i++;
            }

            int j = currentPosition-1;
            while (board[j] > 0)
            {
                if (IsMoveValid(j, piece)) moves.Add(new KeyValuePair<Pieces, (int, int)>(piece, (currentPosition, j)));
                j--;
            }
        }

        public bool IsMoveValid(int positionToMoveTo, Pieces piece)
        {
            return board[positionToMoveTo] == 0 || ((board[positionToMoveTo] > 0) && GetColor((byte)piece, 3) != GetColor((byte)board[positionToMoveTo], 3));
        }

        public bool IsMoveACheck(int positionToMoveTo, Pieces piece) //AS IS THE MOVE IS TAKING THE KING
        {
            return board[positionToMoveTo].HasFlag(Pieces.King) && IsMoveValid(positionToMoveTo, piece);
        }

        public bool IsInCheck(Pieces piece) //MUST TAKE INTO KING
        {
            for (int i = 0; i < moves.Count; i++)
            {
                if (moves[i].Key.HasFlag(Pieces.IsWhite) == piece.HasFlag(Pieces.IsWhite))
                {
                    if (IsMoveACheck(moves[i].Value.Item2, moves[i].Key))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool GetColor(byte byteToConvert, int bitToReturn)
        {
            int mask = 1 << bitToReturn;
            return (byteToConvert & mask) == mask;
        }
    }
}
