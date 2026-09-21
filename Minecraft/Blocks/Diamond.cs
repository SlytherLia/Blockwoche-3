using System;

namespace Minecraft
{
    public class Diamond : Block
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

        public override int GiveLoot()
        {
            switch(base.tool)
            {
                case Tools.Hands:
                    return 0;
                case Tools.Pickaxe:
                    return 3;
                case Tools.Shovel:
                    return 1;
                default:
                    return 2;
            }
        }
    }
}

