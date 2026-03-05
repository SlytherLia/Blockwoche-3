using System;

namespace Minecraft
{
	public abstract class Enemies
	{
		public string name { get; }
		public int HP {get; set;}
		public int distance { get; set; }

        public Enemies(string name, int HP, int distance) 
		{
			this.name = name;
			this.HP = HP;
			this.distance = distance;
		}

		public virtual int attack() { return 1; }

		public virtual int move() { return distance - 1; }
	}
}
