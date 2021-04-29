using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Data.Models
{
    public class Producer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //Navigation properties
        public List<Movie> Movies { get; set; }
    }
}
