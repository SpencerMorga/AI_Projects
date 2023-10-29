using System;
using System.Collections.Generic;
using System.Text;

namespace MiniMaxTrees
{
    public interface IGameState<T> where T : IGameState<T>
    {
        [Flags]
        enum GameState
        {
            Terminal = 4,
            Win = 0,
            Tie = 1,
            Loss = 2
        }

        Node<T>[] GetChildren();


    }

    class ExampleGameState : IGameState<ExampleGameState>
    {
        public Node<ExampleGameState>[] GetChildren()
        {
            throw new NotImplementedException();
        }
    }

    class OtherGameState : IGameState<OtherGameState>
    {
        public Node<OtherGameState>[] GetChildren()
        {
            throw new NotImplementedException();
        }
    }
}
