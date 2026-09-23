using System;
using System.Collections;

namespace Minecraft
{
    public class AlleBloecke : IEnumerable<Block>
    {
        List<Block> blockList;

        public AlleBloecke()
        {
            blockList = new List<Block>();
        }

        public void AddBlock(Block block)
        {
            blockList.Add(block);
        }

        public IEnumerator<Block> GetEnumerator()
        {
            foreach (Block b in blockList)
            {
                yield return b;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
