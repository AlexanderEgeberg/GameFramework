using GameFramework.Enum;

namespace GameFramework.Controls
{

    //Keys
    public class aKey : IKey
    {
        public bool CheckKey(char c)
        {
            if (c == 'a' || c == 'A')
            {
                return true;
            }
            return false;
        }

        public InputKey ReturnKey()
        {
            return InputKey.LEFT;
        }

    }

    public class dKey : IKey
    {
        public bool CheckKey(char c)
        {
            if (c == 'd' || c == 'D')
            {
                return true;
            }
            return false;
        }

        public InputKey ReturnKey()
        {
            return InputKey.RIGHT;
        }

    }
    public class sKey : IKey
    {
        public bool CheckKey(char c)
        {
            if (c == 's' || c == 'S')
            {
                return true;
            }
            return false;
        }

        public InputKey ReturnKey()
        {
            return InputKey.BACK;
        }

    }
    public class wKey : IKey
    {
        public bool CheckKey(char c)
        {
            if (c == 'w' || c == 'W')
            {
                return true;
            }
            return false;
        }

        public InputKey ReturnKey()
        {
            return InputKey.FORWARD;
        }

    }
    public class eKey : IKey
    {
        public bool CheckKey(char c)
        {
            if (c == 'e' || c == 'E')
            {
                return true;
            }
            return false;
        }

        public InputKey ReturnKey()
        {
            return InputKey.USE;
        }

    }
    public class uKey : IKey
    {
        public bool CheckKey(char c)
        {
            if (c == 'u' || c == 'U')
            {
                return true;
            }
            return false;
        }

        public InputKey ReturnKey()
        {
            return InputKey.USE;
        }

    }


    public class jKey : IKey
    {
        public bool CheckKey(char c)
        {
            if (c == 'j' || c == 'J')
            {
                return true;
            }
            return false;
        }

        public InputKey ReturnKey()
        {
            return InputKey.LEFT;
        }

    }


    public class iKey : IKey
    {
        public bool CheckKey(char c)
        {
            if (c == 'i' || c == 'I')
            {
                return true;
            }
            return false;
        }

        public InputKey ReturnKey()
        {
            return InputKey.FORWARD;
        }

    }


    public class kKey : IKey
    {
        public bool CheckKey(char c)
        {
            if (c == 'k' || c == 'K')
            {
                return true;
            }
            return false;
        }

        public InputKey ReturnKey()
        {
            return InputKey.BACK;
        }

    }


    public class lKey : IKey
    {
        public bool CheckKey(char c)
        {
            if (c == 'l' || c == 'L')
            {
                return true;
            }
            return false;
        }

        public InputKey ReturnKey()
        {
            return InputKey.RIGHT;
        }

    }
}