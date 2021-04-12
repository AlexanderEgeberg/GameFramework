using GameFramework.Enum;

namespace GameFramework.Controls
{
    public interface IKey
    {
        bool CheckKey(char c);
        InputKey ReturnKey();
    }
}