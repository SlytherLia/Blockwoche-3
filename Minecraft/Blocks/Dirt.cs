using System;

namespace Minecraft
{
    internal class Dirt : Block
    {
        public Dirt(Blocks name, int resistance, ConsoleColor color) : base(name, resistance, color)
        {

        }

        public override int mine()
        {
            if (base.tool == Tools.Hands)
            {
                return base.resistance * 2;
            }
            else if (base.tool == Tools.Pickaxe)
            {
                return base.resistance * 1;
            }
            else if (base.tool == Tools.Shovel)
            {
                return base.resistance;
            }
            else
            {
                return base.resistance * 1;
            }
        }
    }
}
