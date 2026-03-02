using System;

namespace Minecraft
{
	internal abstract class Block
	{
		public Blocks name { get; }
		public int resistance;
		public ConsoleColor color;
		public Tools tool {  get; set; }

		public Block(Blocks name, int resistance, ConsoleColor color)
		{
			this.name = name;
			this.resistance = resistance;
			this.color = color;
		}

		public String getName() { return name.ToString(); }

		public ConsoleColor getColor() { return color; }

		public virtual int mine()
		{
			return resistance;
		}
	}
}
