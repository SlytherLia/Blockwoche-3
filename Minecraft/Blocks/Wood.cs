using System;

namespace Minecraft
{
    internal class Wood : Block
    {
        public Wood(Blocks name, int resistance, ConsoleColor color) : base(name, resistance, color)
        { 

        }

        public override int mine()
        {
            if (base.tool == Tools.Hands)
            {
                return base.resistance * 4;
            }
            else if (base.tool == Tools.Pickaxe)
            {
                return base.resistance * 2;
            }
            else if (base.tool == Tools.Shovel)
            {
                return base.resistance * 3;
            }
            else
            {
                return base.resistance;
            }
        }
    }
}
