using System;
using System.Collections.Generic;
using GameFramework.Enum;

namespace GameFramework.Controls
{

    public class Controls : IControls
    {
        private List<IKey> _keys;

        public Controls(List<IKey> keys)
        {
            _keys = keys;
        }

        //gets user key pressed in enum
        public InputKey ReadNextKey()
        {
            var ok = true;
            while (ok)
            {
                ConsoleKeyInfo info = Console.ReadKey();
                char c = info.KeyChar;
                foreach (var key in _keys)
                {
                    if (key.CheckKey(c))
                    {
                        ok = false;
                        return key.ReturnKey();
                    }
                }
            }
            return InputKey.NONE;
        }
    }
}
