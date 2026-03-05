using System;

namespace Movies
{
	internal class Movie
	{
		public string title { set; get; }
        public Actor actor { set; get; }
		public List<Actor> actors { get; set; } = new List<Actor>();

    }
}
