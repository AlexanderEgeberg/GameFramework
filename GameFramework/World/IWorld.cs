using System.Text;
using GameFramework.Entities.Creatures.Interface;

namespace GameFramework.World
{
    public interface IWorld
    {

        public int MaxWidth { get; set; }
        public int MaxHeight { get; set; }

        void PrintPlayground(StringBuilder sb);
    }
}
