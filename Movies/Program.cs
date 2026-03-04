using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Xml.Linq;

namespace Movies
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = "C:/Users/Student/source/repos/Blockwoche 3/Movies/text";
            string inputActor;
            string inputMovie ="";

            List<Movie> moviesFromFile = new List<Movie>();
            Dictionary<Movie, string> movieActorDic = new Dictionary<Movie, string>();

            getExisting();

            while (true)
            {
                Console.WriteLine("Enter Actor Name (First Name, Last Name)");
                inputActor = Console.ReadLine();

                checkInput();
                if (inputActor == "print")
                {
                    continue;
                }


                Console.WriteLine("Enter Movie Title");
                inputMovie = Console.ReadLine();
               
                checkInput();
                if(inputMovie == "print")
                {                   
                    continue;
                }

                save();
            }
           
            void checkInput()
            {
                if(inputMovie == "exit" || inputActor == "exit")
                {
                    Environment.Exit(0);
                }
                if(inputMovie == "print" || inputActor == "print")
                {
                    printAll();
                }
            } 

            void createMovie(Actor actor)
            {
                Movie movie = new Movie();
                movie.title = inputMovie;

                movie.allActors.Add(actor);
            }

            void printAll()
            {
                getExisting();
                foreach (Movie m in moviesFromFile)
                {

                    Console.WriteLine(m.title + " - " + movieActorDic[m]);
                }

                Console.WriteLine("\n-----------------------------\n");
            }

            void save()
            {
                bool check = false;
                Movie tempMovie = null;

                foreach(Movie m in moviesFromFile)
                {
                    if(m.title == inputMovie)
                    {
                        tempMovie = m;
                        check = true;
                    }
                }

                if(check)
                {
                    string temp = movieActorDic[tempMovie];
                    temp += inputActor + ", ";
                    movieActorDic[tempMovie] = temp;

                    using (StreamWriter sw = new StreamWriter(path, false))
                    {
                        foreach (Movie m in moviesFromFile)
                        {
                            string tempstring = m.title + " - " + movieActorDic[m];
                            sw.WriteLine(tempstring);
                        }
                    }
                }
                else
                {
                    using (StreamWriter sw = new StreamWriter(path, true))
                    {
                        string temp = inputMovie + " - " + inputActor + ", ";
                        sw.WriteLine(temp);
                    }
                }
            }

            void getExisting()
            {
                moviesFromFile = new List<Movie>();
                movieActorDic = new Dictionary<Movie, string>();

                string[] tempArray = File.ReadAllLines(path);

                for(int i = 0; i < tempArray.Length; i++)
                {
                    Movie movie = new Movie();
                    string[] temp = tempArray[i].Split('-');
                    movie.title = temp[0];
                    moviesFromFile.Add(movie);
                    if(temp.Length < 1) { return; }
                    movieActorDic.Add(movie, temp[1]);
                }
            }
        }
    }
}
