using System;

namespace Minecraft
{
    public class Skeleton : Enemies
    {
        public Skeleton(string name, int HP, int distance) : base(name, HP, distance)
        {
        }

        public override int attack() { return 2; }

        public override int move() { return 0; }
    }
}
