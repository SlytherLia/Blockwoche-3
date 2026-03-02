using System;

namespace Minecraft
{
    public class Creeper : Enemies
    {
        public Creeper(string name, int HP, int distance) : base(name, HP, distance)
        {
        }

        public override int attack() 
        {
            this.HP = 0;
            return 5; 
        }
    }
}
