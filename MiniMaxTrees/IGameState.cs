using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMaxTrees

{
    
    public interface IGameState<TSelf> where TSelf : IGameState<TSelf>
    {
        int Value { get; }
        bool isTerminal { get; }
        //GameState MyState { get; }
        
        TSelf[] GetChildren();
    }
}
