using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Data.Models
{
    public class Actor
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        //Navigations properties
        public List<Movie_Actor> Movie_Actors { get; set; }
    }
}
