using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Transactions;

namespace MiniMaxTrees
{
    internal class GameController : MiniMaxTree<OneDChess>
    {
        [Flags]
        public enum GameState
        {
            IsPlaying = 4,
            Win = 0,
            Tie = 1,
            Loss = 2
        }

        OneDChess current;

        public GameController()
        {
            current = new OneDChess();
        }

        public bool GameOver => current.isTerminal;

        protected override IGameState<OneDChess> Current => current;

        public GameState MyState
        {
            get
            {
                if (current.isTerminal)
                {
                    if (current.Value == 1000)
                    {
                        return GameState.Win;
                    }
                    else if (current.Value == -1000) 
                    {
                        return GameState.Loss;
                    }
                    else
                    {
                        return GameState.Tie;
                    }
                }
                return GameState.IsPlaying;
            }
        }

        public void Move(int currentPos, int targetPos)
        {
            current = current.Move(current.getBoard(), currentPos, targetPos);

            if (current.isTerminal == false) { current = (OneDChess)OptimalMove(!current.Turn); }
        }

        public void Print()
        {
            for (int i = 0; i < 8; i++)
            {
                Console.Write($"[{OneDChess.Translate(current.getBoard()[i])}] ");
            }
            Console.WriteLine();
        }
    }
}
