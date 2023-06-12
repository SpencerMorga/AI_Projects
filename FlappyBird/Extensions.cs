using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBird
{
    internal static class Extensions
    {
        public static bool JustPressed(this KeyboardState ks, KeyboardState prevKs, Keys checkingKey) => ks.IsKeyDown(checkingKey) && prevKs.IsKeyUp(checkingKey); 
    }
}
