using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Minecraft
{
    internal class Program
    {
        static int woodCount = 0;
        static int dirtCount = 0;
        static int stoneCount = 0;
        static int diamondCount = 0;
        static int zombieCount = 0;
        static int creeperCount = 0;
        static int skeletonCount = 0;
        static int goldenAppleCount = 0;
        static int inputTool;
        static int inputBlock;
        static int randomFight;
        static int randomItem;
        static int roundsCount = 1;
        static int inputWeapon;
        static bool mining = true;
        static bool fighting = false;
        static bool game = true;
        static bool isFightOver = false;
        static Tools usedTool;
        static Blocks usedBlock;
        static Array allTools = Enum.GetValues(typeof(Tools));
        static Array allBlocks = Enum.GetValues(typeof(Blocks));
        static Wood wood = new(Blocks.Wood, 3, ConsoleColor.Yellow);
        static Dirt dirt = new(Blocks.Dirt, 2, ConsoleColor.DarkGreen);
        static Stone stone = new(Blocks.Stone, 4, ConsoleColor.Cyan);
        static Diamond diamond = new(Blocks.Diamond, 7, ConsoleColor.Blue);
        static List<Block> blockList = new List<Block>();
        static Random random = new Random();
        static Player player = new(10);
        static Array allWeapons = Enum.GetValues(typeof(Weapons));
        static Weapons usedWeapon;
        static Enemies attackingEnemy = null;
        static Enemies[] allEnemies = new Enemies[3];
        static Skeleton skeleton = new("Skeleton", 4, 5);
        static Creeper creeper = new("Creeper", 3, 3);
        static Zombie zombie = new("Zombie", 4, 2);

        static void Main(string[] args)
        {
            allEnemies[0] = skeleton;
            allEnemies[1] = creeper;
            allEnemies[2] = zombie;

            blockList.Add(wood);
            blockList.Add(dirt);
            blockList.Add(stone);
            blockList.Add(diamond);

            while (game)
            {
                while (mining)
                {
                    Console.WriteLine("Day: {0} - HP: {1}" , roundsCount, player.HP);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine("Choose a Tool:\n0....Hands\n1....Pickaxe\n2....Axe\n3....Shovel\n4....Exit");

                    try
                    {
                        inputTool = Convert.ToInt32(Console.ReadLine());

                        usedTool = (Tools)allTools.GetValue(inputTool);

                        if (usedTool == Tools.Exit)
                        {
                            game = false;
                            mining = false;
                            break;
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Not a valid input try again");
                        continue;
                    }

                    Console.WriteLine("Choose a Block:\n0....Wood\n1....Dirt\n2....Stone\n3....Diamond\n4....Exit");

                    try
                    {
                        inputBlock = Convert.ToInt32(Console.ReadLine());
                        usedBlock = (Blocks)allBlocks.GetValue(inputBlock);

                        if (usedBlock == Blocks.Exit)
                        {
                            game = false;
                            mining = false;
                            break;
                        } 
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
                            for (int i = 0; i < b.mine(); i++)
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
                                player.HP = 10;
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

                if (fighting)
                
                    skeleton.HP = 20;
                    zombie.HP = 20;
                    zombie.distance = 10;
                    creeper.HP = 20;
                    creeper.distance = 10;

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

                                if (isFightOver)
                                {
                                    attackingEnemy = null;
                                    break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("You attack with a {0}", usedWeapon.ToString());
                                attackingEnemy.HP -= 1;

                                enemyTurn();

                                if (isFightOver)
                                { 
                                    attackingEnemy = null;
                                    break;
                                }
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

            if (player.HP == 0)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("\nYou have no HP left! Game Over!");
                Console.ForegroundColor = ConsoleColor.Gray;
                game = false;
                fighting = false;
                isFightOver = true;
            }
        }

        static void checkIfFightOver()
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
        }

        static void enemyTurn()
        {
            if (attackingEnemy == allEnemies[0] || attackingEnemy.distance == 0)
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

        static void addToFile()
        {
            string path = "C:/Users/Student/source/repos/Blockwoche 3/Minecraft/stats";
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
