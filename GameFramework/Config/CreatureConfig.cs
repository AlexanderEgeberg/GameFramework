using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GameFramework.Config
{
    public static class CreatureConfig
    {
        public static List<string> LoadJson()
        {
            List<string> zombieNames = new List<string>();
            //TODO better path?
            Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(File.ReadAllText(@"..\..\..\..\config.json"));
            if (myDeserializedClass != null)
            {
                zombieNames.AddRange(myDeserializedClass.names.Select(zombieName => zombieName.name));
            }
            else
            {
                //if there is no config file then set some default names
                zombieNames.Add("Walker");
                zombieNames.Add("Eater");

            }

            return zombieNames;
        }
    }
    public class Name
    {
        public string name { get; set; }
    }
    public class Root
    {
        public List<Name> names { get; set; }
    }
}



