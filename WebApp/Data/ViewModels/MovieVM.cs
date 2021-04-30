using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Data.ViewModels
{
    public class MovieVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int? Rate { get; set; }
        public int? NumOfRates { get; set; }
        public string Genre { get; set; }
        public int ProducerId { get; set; }
        public List<int> ActorIds { get; set; }
    }

    public class MovieWithActorsVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public double? Rate { get; set; }
        public int? NumOfRates { get; set; }
        public string Genre { get; set; }
        public string ProducerName { get; set; }
        public List<string> ActorNames { get; set; }
    }
}
