using System;
using GameFramework.Enum;

namespace GameFramework.Controls
{

    //Keys
    public class LeftKey : IKey
    {
        public char Button { get; set; }
        public LeftKey(char button)
        {
            Button = button;
        }
        public bool CheckKey(char c)
        {
            if (c == Button)
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
    public class RightKey : IKey
    {
        public char Button { get; set; }
        public RightKey(char button)
        {
            Button = button;
        }
        public bool CheckKey(char c)
        {
            if (c == Button)
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
    public class ForwardKey : IKey
    {
        public char Button { get; set; }
        public ForwardKey(char button)
        {
            Button = button;
        }
        public bool CheckKey(char c)
        {
            if (c == Button)
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
    public class BackKey : IKey
    {
        public char Button { get; set; }
        public BackKey(char button)
        {
            Button = button;
        }
        public bool CheckKey(char c)
        {
            if (c == Button)
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
    public class UseKey : IKey
    {
        public char Button { get; set; }
        public UseKey(char button)
        {
            Button = button;
        }
        public bool CheckKey(char c)
        {
            if (c == Button)
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
}