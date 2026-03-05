using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Xml.Linq;

namespace Movies
{
    internal class Program
    {
        static string path = "C:/Users/Student/source/repos/Blockwoche 3/Movies/text";
        static List<Movie> movies = new List<Movie>();
        static Dictionary<string, List<string>> movieActorDic = new Dictionary<string, List<string>>();

        static void Main(string[] args)
        {
            getExisting();

            while (true)
            {
                Console.WriteLine("Enter Actor Name (First Name, Last Name)");
                string inputActor = Console.ReadLine();

                if(inputActor == "exit")
                {
                    break;
                }
                if(inputActor == "print")
                {
                    printAll();
                    continue;
                }

                Console.WriteLine("Enter Movie Title");
                string inputMovie = Console.ReadLine();

                if (inputMovie == "exit")
                {
                    break;
                }
                if (inputMovie == "print")
                {
                    printAll(); 
                    continue;
                }

                addActorToMovie(inputActor, inputMovie);
                save();
            }   
        }

        static void addActorToMovie(string inputActor, string inputMovie)
        {
            Movie movie = movies.Find(m => m.title == inputMovie);

            if (movie == null)
            {
                movie = new Movie();
                movie.title = inputActor;
                movies.Add(movie);
            }

            Actor actor = new Actor();
            actor.name = inputActor;
            movie.actors.Add(actor);
        }

        static void printAll()
        {
            foreach (Movie m in movies)
            {

                Console.Write(m.title + " - ");

                foreach (Actor actor in m.actors)
                {
                    Console.Write(actor.name + ", ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\n-----------------------------\n");
        }

        static void save()
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                foreach (Movie movie in movies)
                {
                    string actorList = "";
                    foreach (Actor actor in movie.actors)
                    {
                        actorList += actor.name + ", ";
                    }
                    sw.WriteLine(movie.title + " - " + actorList);
                }
            }
        }

        static void getExisting()
        {
            if (!File.Exists(path))
            {
                return;
            }

            string[] lines = File.ReadAllLines(path);

            foreach (string line in lines)
            {
                string[] parts = line.Split(" - ");

                if (parts.Length < 2)
                {
                    continue;
                }

                Movie movie = new Movie();
                movie.title = parts[0];

                string[] actors = parts[1].Split(", ");

                foreach (string actorname in actors)
                {
                    string name = actorname.Trim();

                    if (name == "")
                    {
                        continue;
                    }
                    Actor actor = new Actor();
                    actor.name = name;

                    movie.actors.Add(actor);
                }

                movies.Add(movie);
            }
        }
    }
}
