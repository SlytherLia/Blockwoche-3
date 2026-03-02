using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Minecraft
{
    internal class Program
    {
        static void Main(string[] args)
        { 
            int roundsCount = 1;
            bool mining = true;
            bool fighting = false;
            bool game = true;
            bool isFightOver = false;
            Random random = new Random();
            Enemies attackingEnemy;
            Player player = new(10);
            Skeleton skeleton = new("Skeleton", 4, 5);
            Creeper creeper = new("Creeper", 3, 3);
            Zombies zombie = new("Zombie", 4, 2);

            int woodCount = 0;
            int dirtCount = 0;
            int stoneCount = 0;
            int diamondCount = 0;
            int zombieCount = 0;
            int creeperCount = 0;
            int skeletonCount = 0;
            int goldenAppleCount = 0;

            while (game)
            {
                while (mining)
                {
                    int inputTool;
                    int inputBlock;
                    int randomFight;
                    int randomItem;
                    Tools usedTool;
                    Blocks usedBlock;
                    Array allTools = Enum.GetValues(typeof(Tools));
                    Array allBlocks = Enum.GetValues(typeof(Blocks));
                    Wood wood = new(Blocks.Wood, 3, ConsoleColor.Yellow);
                    Dirt dirt = new(Blocks.Dirt, 2, ConsoleColor.DarkGreen);
                    Stone stone = new(Blocks.Stone, 4, ConsoleColor.Cyan);
                    Diamond diamond = new(Blocks.Diamond, 7, ConsoleColor.Blue);
                    List<Block> blockList = new List<Block>();

                    blockList.Add(wood);
                    blockList.Add(dirt);
                    blockList.Add(stone);
                    blockList.Add(diamond);

                    Console.WriteLine("Day: {0}", roundsCount);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine("Choose a Tool:\n0....Hands\n1....Pickaxe\n2....Axe\n3....Shovel\n10....Exit");
                    try
                    {
                        inputTool = Convert.ToInt32(Console.ReadLine());

                        if (inputTool == 10)
                        {
                            game = false;
                            mining = false;
                            break;
                        }
                        usedTool = (Tools)allTools.GetValue(inputTool);
                    }
                    catch
                    {
                        Console.WriteLine("Not a valid input try again");
                        continue;
                    }

                    Console.WriteLine("Choose a Block:\n0....Wood\n1....Dirt\n2....Stone\n3....Diamond\n10....Exit");

                    try
                    {
                        inputBlock = Convert.ToInt32(Console.ReadLine());

                        if (inputBlock == 10)
                        {
                            game = false;
                            mining = false;
                            break;
                        }

                        usedBlock = (Blocks)allBlocks.GetValue(inputBlock);
                    }
                    catch
                    {
                        Console.WriteLine("Not a valid input try again");
                        continue;
                    }

                    foreach (Block b in blockList)
                    {
                        if (b.getName() == usedBlock.ToString())
                        {
                            randomItem = random.Next(0, 3);

                            b.tool = usedTool;
                            Console.ForegroundColor = b.getColor();
                            for(int i = 0; i < b.mine(); i++)
                            {
                                Console.WriteLine("Mine....");
                                Thread.Sleep(1000);
                            }

                            Console.WriteLine("It takes {0} seconds to mine {1} with {2}", b.mine(), b.getName(), usedTool.ToString());
                            if (usedBlock == Blocks.Diamond) diamondCount++;
                            else if (usedBlock == Blocks.Dirt) dirtCount++;
                            else if (usedBlock == Blocks.Stone) stoneCount++;
                            else if (usedBlock == Blocks.Wood) woodCount++;

                            if (randomItem == 0)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                Console.WriteLine("Congrats you found a golden Apple! Your HP is now restored!");
                                Console.ForegroundColor = ConsoleColor.Gray;
                            }
                        } 
                    }
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("------------------------------------------");

                    randomFight = random.Next(0, 4);

                    if (randomFight == 0)
                    {
                        mining = false;
                        fighting = true;
                    }

                    roundsCount++;
                }

                while (fighting)
                {
                    int inputWeapon;
                    Array allWeapons = Enum.GetValues(typeof(Weapons));
                    Enemies[] allEnemies = new Enemies[3];
                    Weapons usedWeapon;
                    skeleton = new("Skeleton", 4, 5);
                    creeper = new("Creeper", 3, 3);
                    zombie = new("Zombie", 4, 2);

                    allEnemies[0] = skeleton;
                    allEnemies[1] = creeper;
                    allEnemies[2] = zombie;

                    int rand = random.Next(0, 2);
                    attackingEnemy = allEnemies[rand];
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n{0} is attacking.", attackingEnemy.name);

                    while (true)
                    {
                        Console.WriteLine("{0} has {1} HP and is {2} Blocks away!", attackingEnemy.name, attackingEnemy.HP, attackingEnemy.distance);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine("Choose wich weapon to attack with\n0....Sword\n1....Bow");

                        try
                        {
                            inputWeapon = Convert.ToInt32(Console.ReadLine());
                            usedWeapon = (Weapons)allWeapons.GetValue(inputWeapon);

                            if (attackingEnemy.distance > 0 && usedWeapon == Weapons.Sword)
                            {
                                Console.WriteLine("Enemy out of reach. You can't attack.");
                                enemyTurn();
                                if (isFightOver) break;
                            }
                            else
                            {
                                Console.WriteLine("You attack with a {0}", usedWeapon.ToString());
                                attackingEnemy.HP -= 1;

                                enemyTurn();
                                if (isFightOver) break;
                            }
                            attackingEnemy.move();
                        }
                        catch
                        {
                            Console.WriteLine("Not a valid input try again");
                            continue;
                        }
                    }
                }
            }


            addToFile();
            Console.WriteLine("Thanks for playing!\nBye Bye!");


            void checkIfFightOver()
            {
                if (attackingEnemy.HP == 0)
                {
                    if (attackingEnemy.name == skeleton.name) skeletonCount++;
                    else if (attackingEnemy.name == creeper.name) creeperCount++;
                    else if (attackingEnemy.name == zombie.name) zombieCount++;

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("You have won the Fight!\n------------------------------------------");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    fighting = false;
                    mining = true;
                    roundsCount++;
                    isFightOver = true;
                }

                if (player.HP == 0)
                {
                    Console.ForegroundColor= ConsoleColor.Magenta;
                    Console.WriteLine("\nYou have no HP left! Game Over!");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    game = false;
                    fighting = false;
                    isFightOver = true;
                }
            }

            void enemyTurn()
            {
                if (attackingEnemy == skeleton || attackingEnemy.distance == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    player.HP -= attackingEnemy.attack();
                    Console.WriteLine("{0} is attacking! Remaining HP: {1}", attackingEnemy.name, player.HP);
                    Console.ForegroundColor = ConsoleColor.Gray;

                    checkIfFightOver();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("You are lucky! {0} is too far away to attack!", attackingEnemy.name);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    checkIfFightOver();
                }
            }

            void addToFile()
            {
                string path = "C:/Users/Student/source/repos/Stats.txt";
                int totalBlocks = diamondCount + stoneCount + dirtCount + woodCount;
                int totalMonsters = zombieCount + creeperCount + skeletonCount;

                string[] text = {"Total Blocks mined: " + totalBlocks,
                                 "Diamonds: " + diamondCount,
                                 "Stone: " + stoneCount,
                                 "Dirt: " + dirtCount,
                                 "Wood: " + woodCount,
                                 "Total Monsters slayen: " + totalMonsters,
                                 "Zombies: " + zombieCount,
                                 "Creeper: " + creeperCount,
                                 "Skeletons: " + skeletonCount,
                                 "Golden Apples Found: " + goldenAppleCount,
                                 "----------------------------------------------------" };

                File.AppendAllLines(path, text);
            }
        }
    }
}
