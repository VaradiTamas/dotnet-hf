using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Data.ViewModels
{
    public class ProducerVM
    {
        public string Name { get; set; }
    }

    //contains a list of the names of actors & movie titles
    public class ProducerWithMoviesAndActorsVM
    {
        public string Name { get; set; }
        public List<MovieActorVM> MovieActors { get; set; }
    }

    public class MovieActorVM
    {
        public string MovieTitle { get; set; }
        public List<string> MovieActors { get; set; }
    }
}
