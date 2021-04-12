using System.Text;
using GameFramework.Entities.Creatures.Interface;

namespace GameFramework.World
{
    public interface IWorld
    {

        public int MaxWidth { get; set; }
        public int MaxHeight { get; set; }
        public IPlayer Player { get; set; }

        void PrintPlayground(StringBuilder sb);

        void PrintRowString(int r, StringBuilder sb);

        void PrintColRowChar(int row, int col, StringBuilder sb);
    }
}
