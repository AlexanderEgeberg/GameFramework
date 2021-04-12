using System;
using System.Collections.Generic;
using GameFramework.Enum;

namespace GameFramework.Controls
{

    public class Controls : IControls
    {
        public Dictionary<InputKey, IKey> Keys { get; set; }

        public Controls(Dictionary<InputKey, IKey> keys)
        {
            Keys = keys;
        }

        //gets user key pressed in enum
        public InputKey ReadNextKey()
        {
            while (true)
            {
                ConsoleKeyInfo info = Console.ReadKey();
                char c = info.KeyChar;
                foreach (var key in Keys)
                {
                    if (key.Value.CheckKey(c))
                    {
                       return key.Value.ReturnKey();
                    }
                }
            }
        }
    }
}
