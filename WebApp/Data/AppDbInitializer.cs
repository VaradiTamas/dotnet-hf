using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data.Models;

namespace WebApp.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                if (!context.Movies.Any())
                {
                    context.Movies.AddRange(
                        new Movie()
                        {
                            Title = "1st Movie Title",
                            Description = "1st Movie Description",
                            Rate = 4,
                            NumOfRates = 1,
                            Genre = "Comedy",
                            DateAdded = DateTime.Now
                        },
                        new Movie()
                        {
                            Title = "2nd Movie Title",
                            Description = "2nd Movie Description",
                            Genre = "Horror",
                            DateAdded = DateTime.Now
                        }
                    );

                    context.SaveChanges();
                }
            }
        }
    }
}
