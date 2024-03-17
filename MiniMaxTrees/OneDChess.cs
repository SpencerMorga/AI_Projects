using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Diagnostics.SymbolStore;
using System.Runtime.InteropServices;
using System.Text;
using static MiniMaxTrees.OneDChess;

using PieceMove =  System.Collections.Generic.KeyValuePair<MiniMaxTrees.OneDChess.Pieces, (int, int)>;

namespace MiniMaxTrees
{
    public class OneDChess : IGameState<OneDChess>
    {
        //  
        // FORWARD IS DEFINED AS THE NEGATIVE Y DIRECTION ON THE BOARD, WITH THE ONE SQUARE AT THE TOP AND THE EIGHT SQUARE AT THE BOTTOM

        Pieces[] board = new Pieces[8];
        GameState current = new GameState();
        bool Turn; //true is white, false is black
        List<PieceMove> moves = new List<PieceMove>();
        bool breakAll = false;

        public int Value { get; }

        [Flags]
        public enum Pieces : byte
        {
            Knight = 0B1,
            Rook = 0B10,
            King = 0B100,
            IsWhite = 0B1000
        }
        
        public GameState MyState
        {
            get
            {
                if (IsWin())
                {
                    return current = GameState.Win;
                }
                else if (IsLoss())
                {
                    return current = GameState.Loss;
                }
                else if (IsDraw())
                {
                    return current = GameState.Tie;
                }
                return current = GameState.IsPlaying;
            }
        }

        public OneDChess()
        {
            board = new Pieces[] { Pieces.King & Pieces.IsWhite, (Pieces)0B1001, (Pieces)0B1010, 0B0, 0B0, (Pieces)0B10, (Pieces)0B1, (Pieces)0B100 };
            Value = 0;
            Turn = true;
        }

        public OneDChess(Pieces[] Board, bool turn)
        {
            board = Board;
            Turn = turn;
            Value = 0;
        }

        public OneDChess[] GetChildren()
        {
            FillMoves(); //fills moves with appropriate color based on turn

            OneDChess[] children = new OneDChess[moves.Count];

            for (int i = 0; i < moves.Count; i++)
            {
                Pieces[] newBoard = new Pieces[board.Length]; //initialize newBoard
                CopyArray(board, newBoard);

                Pieces[] testBoard = new Pieces[newBoard.Length]; //initialize testBoard
                CopyArray(newBoard, testBoard);

                if (DoesMoveEndangerKing(ref newBoard, ref testBoard, moves[i])) //if move will endanger king, do not make move 
                {
                    continue;
                }
                
                Move(newBoard, moves[i].Value.Item1, moves[i].Value.Item2);

                children[i] = new OneDChess(newBoard, Turn);
            }
            return children;
        }

        public void FillMoves() //i could optimize but nah
        {
            for (int i = 0; i < board.Length; i++) // Fill Moves
            {
                if (Turn)
                {
                    if (board[i].HasFlag(Pieces.IsWhite))
                    {
                        if (board[i].HasFlag(Pieces.King))
                        {
                            GetValidKingMoves(i, board[i]);
                        }
                        if (board[i].HasFlag(Pieces.Knight))
                        {
                            GetValidKnightMoves(i, board[i]);
                        }
                        if (board[i].HasFlag(Pieces.Rook))
                        {
                            ValidRookMoves(i, board[i]);
                        }
                    }
                }
                else
                {
                    if (!board[i].HasFlag(Pieces.IsWhite))
                    {
                        if (board[i].HasFlag(Pieces.King))
                        {
                            GetValidKingMoves(i, board[i]);
                        }
                        if (board[i].HasFlag(Pieces.Knight))
                        {
                            GetValidKnightMoves(i, board[i]);
                        }
                        if (board[i].HasFlag(Pieces.Rook))
                        {
                            ValidRookMoves(i, board[i]);
                        }
                    }
                }
            }
        }

        public bool CheckGameOver() => IsWin() || IsLoss() || IsDraw();
        public bool IsWin()
        {
            for (int i = 0; i < board.Length; i++)
            {
                if (board[i].HasFlag(Pieces.King) && (board[i].HasFlag(Pieces.IsWhite) != Turn)) //find OPP king
                {
                    if (IsInCheck(board[i]) && !CanBlockCheckmate(i))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool IsLoss()
        {
            for (int i = 0; i < board.Length; i++)
            {
                if (board[i].HasFlag(Pieces.King) && (board[i].HasFlag(Pieces.IsWhite)) == Turn) //find same color king♫
                {
                    if (IsInCheck(board[i]) && !CanBlockCheckmate(i))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool IsDraw()
        {
            if (moves.Count == 0)
            {
                return true;
            }
            return false;
        }

        public bool DoesMoveEndangerKing(ref Pieces[] newBoard, ref Pieces[] testBoard, PieceMove move)
        {
            for (int j = 0; j < 8; j++)
            {
                if (newBoard[j].HasFlag(Pieces.King) && (newBoard[j].HasFlag(Pieces.IsWhite)) == (moves[j].Key.HasFlag(Pieces.IsWhite))) //is the piece same color king?
                {
                    Move(testBoard, move.Value.Item1, move.Value.Item2);

                    if (IsInCheck(testBoard[j]))
                    {
                        CopyArray(newBoard, testBoard); // reset testBoard
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CanBlockCheckmate(int kingPosition)
        {
            Pieces[] testBoard = new Pieces[board.Length]; //initialize testBoard
            CopyArray(board, testBoard);

            for (int j = 0; j < moves.Count; j++)
            {
                if (moves[j].Key.HasFlag(Pieces.IsWhite) == board[moves[j].Value.Item2].HasFlag(Pieces.IsWhite)) // IF PIECE MOVING HAS SAME COLOR AS ATTACKED KING
                {
                    CopyArray(board, testBoard); //reset testBoard

                    Move(testBoard, moves[j].Value.Item1, moves[j].Value.Item2); //make move
                    if (!IsInCheck(testBoard[kingPosition])) //if ischeckmate results in false, the move stays and no checkmate is made
                    {
                        //no checkmate
                        return false;
                        // CopyArray(testBoard, board); // make board have the move that makes uncheckmate
                    }
                }
            }
            return true;
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
                board[newPosition] = board[currentPosition];
                board[currentPosition] = 0;
            }
        }

        public void GetValidKnightMoves(int currentPosition, Pieces piece)
        {
            if (IsMoveValid(currentPosition + 2, piece)) moves.Add(new PieceMove(piece, (currentPosition, currentPosition + 2)));
            if (IsMoveValid(currentPosition - 2, piece)) moves.Add(new PieceMove(piece, (currentPosition, currentPosition - 2)));
        }

        public void GetValidKingMoves(int currentPosition, Pieces piece) 
        {
            if (IsMoveValid(currentPosition + 1, piece)) moves.Add(new PieceMove(piece, (currentPosition, currentPosition + 1)));
            if (IsMoveValid(currentPosition - 1, piece)) moves.Add(new PieceMove(piece, (currentPosition, currentPosition - 1)));
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
