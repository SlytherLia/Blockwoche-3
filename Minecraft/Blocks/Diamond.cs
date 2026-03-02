using System;

namespace Minecraft
{
    internal class Diamond : Block
    {
        public Diamond(Blocks name, int resistance, ConsoleColor color) : base(name, resistance, color)
        {
        }

        public override int mine()
        {
            if (base.tool == Tools.Hands)
            {
                return base.resistance * 8;
            }
            else if (base.tool == Tools.Pickaxe)
            {
                return base.resistance;
            }
            else if (base.tool == Tools.Shovel)
            {
                return base.resistance * 7;
            }
            else
            {
                return base.resistance * 5;
            }
        }
    }
}

