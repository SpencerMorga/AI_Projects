using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMaxTrees

{
    [Flags]
    public enum GameState
    {
        Terminal = 0B100,
        Win = 0,
        Tie = 1,
        Loss = 0B10
    }
    public interface IGameState<TSelf> where TSelf : IGameState<TSelf>
    {
        GameState MyState { get; }
        TSelf[] GetChildren();
    }
}
