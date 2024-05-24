using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Diagnostics.SymbolStore;
using System.Runtime.InteropServices;
using System.Text;
using static MiniMaxTrees.OneDChess;

using PieceMove = System.Collections.Generic.KeyValuePair<MiniMaxTrees.OneDChess.Pieces, (int, int)>;

namespace MiniMaxTrees
{
    public class OneDChess : IGameState<OneDChess>
    {
        //  
        // FORWARD IS DEFINED AS THE NEGATIVE Y DIRECTION ON THE BOARD, WITH THE ONE SQUARE AT THE TOP AND THE EIGHT SQUARE AT THE BOTTOM

        Pieces[] board = new Pieces[8];

        public Pieces[] getBoard() { return board; }

        public string ReadableTurn => Turn == true ? "Black" : "White";
        public bool Turn { get; set; } //true is black, false is white !=
        List<PieceMove> moves = new List<PieceMove>();
        bool breakAll = false;

        public static char Translate(Pieces piece)
        {
            if (piece.HasFlag(Pieces.IsWhite))
            {
                if (piece.HasFlag(Pieces.Knight)) return 'N';
                else if (piece.HasFlag(Pieces.Rook)) return 'R';
                else if (piece.HasFlag(Pieces.King)) return 'K';
            }
            else if (!piece.HasFlag(Pieces.IsWhite))
            {
                if (piece.HasFlag(Pieces.Knight)) return 'n';
                else if (piece.HasFlag(Pieces.Rook)) return 'r';
                else if (piece.HasFlag(Pieces.King)) return 'k';
            }

            return ' ';
        }

        public bool isTerminal { get; private set; }
        public int Value { get; }

        [Flags]
        public enum Pieces : byte
        {
            Knight = 0B1,
            Rook = 0B10,
            King = 0B100,
            IsWhite = 0B1000
        }

        public OneDChess()
        {
            board = new Pieces[] { (Pieces)0B1100, (Pieces)0B1001, (Pieces)0B1010, 0B0, 0B0, (Pieces)0B10, (Pieces)0B1, (Pieces)0B100 };
            Value = 0;
            Turn = false;
            isTerminal = false;
        }

        public OneDChess(Pieces[] Board, bool turn)
        {
            board = Board;
            Turn = turn;
            Value = 0;
            isTerminal = false;
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

                LocalMove(newBoard, moves[i].Value.Item1, moves[i].Value.Item2);

                children[i] = new OneDChess(newBoard, !Turn);
            }
            return children;
        }

        public void FillMoves() //i could optimize but nah
        {
            for (int i = 0; i < board.Length; i++) // Fill Moves
            {
                //if (Turn != board[i].HasFlag(Pieces.IsWhite))
                //{
                //    if (board[i].HasFlag(Pieces.King))
                //    {
                //        //GetValidKingMoves(i, board[i]);
                //    }
                //    if (board[i].HasFlag(Pieces.Knight))
                //    {
                //       // GetValidKnightMoves(i, board[i]);
                //    }
                //    if (board[i].HasFlag(Pieces.Rook))
                //    {
                //      //  ValidRookMoves(i, board[i]);
                //    }
                //}

                if (!Turn) //make pretty
                { //turn = false && white
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
                { //turn is true && !white
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
                        isTerminal = true;
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
                        isTerminal = true;
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
                isTerminal = true;
                return true;
            }
            return false;
        }

        public bool DoesMoveEndangerKing(ref Pieces[] newBoard, ref Pieces[] testBoard, PieceMove move) //DOES NOT WORK??? 
        {
            for (int j = 0; j < 8; j++)
            {
                if (newBoard[j].HasFlag(Pieces.King) && (newBoard[j].HasFlag(Pieces.IsWhite)) == (move.Key.HasFlag(Pieces.IsWhite))) //is the piece same color king?
                {
                    LocalMove(testBoard, move.Value.Item1, move.Value.Item2);

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

                    LocalMove(testBoard, moves[j].Value.Item1, moves[j].Value.Item2); //make move
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

        private void LocalMove(Pieces[] board, int currentPosition, int newPosition)
        {
            if (IsMoveValid(newPosition, board[currentPosition]) && !IsOwnKingInCheck())
            {
                board[newPosition] = board[currentPosition];
                board[currentPosition] = 0;
            }
        }

        public bool IsOwnKingInCheck()
        {
            for (int i = 0; i < board.Length; i++)
            {
                if (board[i].HasFlag(Pieces.King) && (board[i].HasFlag(Pieces.IsWhite) == Turn))
                {
                    if (IsInCheck(board[i]))
                    {
                        Console.WriteLine(" !!!! CHECK !!!! ");
                        return true;
                    }
                }
            }
            return false;
        }

        public OneDChess Move(Pieces[] board, int currentPositon, int newPosition)
        {
            //just in case if i want to, make sure moves contains this current move

            Pieces[] newBoard = new Pieces[board.Length];
            CopyArray(board, newBoard);

            OneDChess newGame = new OneDChess(newBoard, Turn);
            newGame.LocalMove(newBoard, currentPositon, newPosition);

            return newGame;
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
            int i = currentPosition + 1;
            while (i < 8)
            {
                if (board[i] != 0)
                {
                    if (board[i].HasFlag(Pieces.IsWhite) == piece.HasFlag(Pieces.IsWhite)) //if hits same color piece
                    {
                        break;
                    }
                    else if ((board[i].HasFlag(Pieces.IsWhite) == piece.HasFlag(Pieces.IsWhite)) && !board[i].HasFlag(Pieces.King)) //if hits takeable opposite color piece
                    {
                        if (IsMoveValid(i, piece)) moves.Add(new PieceMove(piece, (currentPosition, i)));
                        break;
                    }
                }

                if (IsMoveValid(i, piece)) moves.Add(new PieceMove(piece, (currentPosition, i)));
                i++;
            }

            int j = currentPosition - 1;
            while (j > 0)
            {
                if (board[j] != 0)
                {
                    if (board[j].HasFlag(Pieces.IsWhite) == piece.HasFlag(Pieces.IsWhite)) //if hits same color piece
                    {
                        break;
                    }
                    else if ((board[j].HasFlag(Pieces.IsWhite) == piece.HasFlag(Pieces.IsWhite)) && !board[j].HasFlag(Pieces.King)) //if hits takeable opposite color piece
                    {
                        if (IsMoveValid(j, piece)) moves.Add(new PieceMove(piece, (currentPosition, j)));
                        break;
                    }
                }

                if (IsMoveValid(j, piece)) moves.Add(new PieceMove(piece, (currentPosition, j)));
                j++;
            }
        }


        public bool IsMoveValid(int positionToMoveTo, Pieces piece)
        {
            //positionToMoveTo -= 1;
            if (positionToMoveTo < 0 || positionToMoveTo >= 8) return false;

            return board[positionToMoveTo] == 0 || ((board[positionToMoveTo] > 0) && GetColor((byte)piece, 3) != GetColor((byte)board[positionToMoveTo], 3));



        }

        public bool IsMoveACheck(int positionToMoveTo, Pieces piece) //AS IS THE MOVE IS TAKING THE KING
        {

            return board[positionToMoveTo].HasFlag(Pieces.King) && IsMoveValid(positionToMoveTo, piece);

        }

        public bool IsInCheck(Pieces king) //MUST TAKE INTO KING
        {
            for (int i = 0; i < moves.Count; i++)
            {
                if (moves[i].Key.HasFlag(Pieces.IsWhite) != king.HasFlag(Pieces.IsWhite))
                {
                    if (IsMoveACheck(moves[i].Value.Item2, moves[i].Key))
                    {
                        Console.WriteLine("CHECK");
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
