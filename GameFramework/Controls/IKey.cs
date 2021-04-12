using GameFramework.Enum;

namespace GameFramework.Controls
{
    public interface IKey
    {
        public char Button { get; set; }
        bool CheckKey(char c);
        InputKey ReturnKey();
    }
}