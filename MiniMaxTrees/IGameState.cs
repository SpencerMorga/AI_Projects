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
        IsPlaying = 4,
        Win = 0,
        Tie = 1,
        Loss = 2
    }
    public interface IGameState<TSelf> where TSelf : IGameState<TSelf>
    {
        int Value { get; }
        GameState MyState { get; }
        
        TSelf[] GetChildren();
    }
}
