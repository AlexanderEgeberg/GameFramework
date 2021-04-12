using System.Collections.Generic;
using GameFramework.Enum;

namespace GameFramework.Controls
{
    public interface IControls
    {
        public Dictionary<InputKey, IKey> Keys { get; set; }
        public InputKey ReadNextKey();
    }
}