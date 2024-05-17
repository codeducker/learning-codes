using Microsoft.EntityFrameworkCore;
using NetCoreWebApp.Data;

namespace NetCoreWebApp.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MoviesContext(
                       serviceProvider.GetRequiredService<
                           DbContextOptions<MoviesContext>>()))
            {
                if (context == null || context.Movie == null)
                {
                    throw new ArgumentNullException("Null MoviesContext");
                }

                // Look for any movies.
                if (context.Movie.Any())
                {
                    return;   // DB has been seeded
                }

                context.Movie.AddRange(
                    new Movie
                    {
                        Title = "When Harry Met Sally",
                        ReleaseTime = DateTime.Parse("1989-2-12 14:30:32"),
                        Description = "Romantic Comedy",
                        Price = 7.99D
                    },

                    new Movie
                    {
                        Title = "Ghostbusters ",
                        ReleaseTime = DateTime.Parse("1984-3-13 14:30:32"),
                        Description = "Comedy",
                        Price = 8.99D
                    },

                    new Movie
                    {
                        Title = "Ghostbusters 2",
                        ReleaseTime = DateTime.Parse("1986-2-23 14:30:32"),
                        Description = "Comedy",
                        Price = 9.99D
                    },

                    new Movie
                    {
                        Title = "Rio Bravo",
                        ReleaseTime = DateTime.Parse("1959-4-15 14:30:32"),
                        Description = "Western",
                        Price = 3.99D
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
