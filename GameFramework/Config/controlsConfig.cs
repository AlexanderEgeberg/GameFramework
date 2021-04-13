using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using GameFramework.Controls;
using GameFramework.Enum;

namespace GameFramework.Config
{
    public class ControlsConfig
    {
        public static Dictionary<InputKey, IKey> ReadConfiguration()
        {
            #region Noter 
            //SOLID - O: open for extension, but closed for modification
            //To add more  control keys you create a new Key class and add it to IKey dictionary
            //Extension through keys list
            //Assign the key for Left,Right,Forward,Back,Use
            //If wanna add more keys to the game, create another IKey class
            //If you wanna reassign a key change the button property
            //Could be loaded from config file
            #endregion

            Dictionary<InputKey, IKey> defaultKeyboard = new Dictionary<InputKey, IKey>()
            {
                {InputKey.LEFT,new LeftKey('a')},
                {InputKey.RIGHT,new RightKey('d')},
                {InputKey.FORWARD,new ForwardKey('w')},
                {InputKey.BACK,new BackKey('s')},
                {InputKey.USE,new UseKey('q')},
            };
            Dictionary<InputKey, IKey> keyboard = new Dictionary<InputKey, IKey>();


            XmlDocument configDoc = new XmlDocument();
            //TODO better path?
            configDoc.Load(@"..\..\..\..\controlsConfig.conf");

           Dictionary<InputKey, XmlNode> nodes = new Dictionary<InputKey, XmlNode>();
           nodes.Add(InputKey.FORWARD, configDoc.DocumentElement.SelectSingleNode("Forward"));
           nodes.Add(InputKey.BACK, configDoc.DocumentElement.SelectSingleNode("Back"));
           nodes.Add(InputKey.LEFT, configDoc.DocumentElement.SelectSingleNode("Left"));
           nodes.Add(InputKey.RIGHT, configDoc.DocumentElement.SelectSingleNode("Right"));
           nodes.Add(InputKey.USE, configDoc.DocumentElement.SelectSingleNode("Use"));

           if (nodes.Values.All(x => x.InnerText != ""))
            {
                keyboard.Clear();
                string forwardKey = nodes[InputKey.FORWARD].InnerText.Trim();
                keyboard.Add(InputKey.FORWARD, new ForwardKey(forwardKey[0]));
                string backKey = nodes[InputKey.BACK].InnerText.Trim();
                keyboard.Add(InputKey.BACK, new BackKey(backKey[0]));
                string leftKey = nodes[InputKey.LEFT].InnerText.Trim();
                keyboard.Add(InputKey.LEFT, new LeftKey(leftKey[0]));
                string rightKey = nodes[InputKey.RIGHT].InnerText.Trim();
                keyboard.Add(InputKey.RIGHT, new RightKey(rightKey[0]));
                string useKey = nodes[InputKey.USE].InnerText.Trim();
                keyboard.Add(InputKey.USE, new UseKey(useKey[0]));

                return keyboard;
            }

            //Default if nothing to read
            return defaultKeyboard;
        }
    }
}
