using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Data.Models
{
    public class Movie_Actor
    {
        public int Id { get; set; }

        //Navigation properties to movie
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        //Navigation properties to actor
        public int ActorId { get; set; }
        public Actor Actor { get; set; }
    }
}
