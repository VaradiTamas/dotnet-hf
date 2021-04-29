using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Data.ViewModels
{
    public class ActorVM
    {
        public string FullName { get; set; }
    }

    public class ActorWithMoviesVM
    {
        public string FullName { get; set; }
        public List<string> MovieTitles { get; set; }
    }
}
