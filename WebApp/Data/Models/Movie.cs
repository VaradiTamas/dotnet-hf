using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Data.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? Rate { get; set; }
        public int? NumOfRates { get; set; }
        public string Genre { get; set; }
        public DateTime DateAdded { get; set; }

        //Navigation properties
        public int ProducerId { get; set; }
        public Producer Producer { get; set; }
        public List<Movie_Actor> Movie_Actors { get; set; }
    }
}
