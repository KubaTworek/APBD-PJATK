using Microsoft.EntityFrameworkCore;

namespace MvcMovie.Models
{
    public class SeedData
    {
        private readonly MvcMovieContext _context;

        public SeedData(MvcMovieContext context)
        {
            _context = context;
        }

        public void Seed()
        {
                if (_context.Movies.Any())
                {
                    return;
                }

                _context.Movies.AddRange(
                    new Movie
                    {
                        Title = "When Harry Met Sally",
                        ReleaseDate = DateTime.Parse("1989-2-12"),
                        Genre = "Romantic Comedy",
                        Rating = "R",
                        Price = 7.99M
                    },

                    new Movie
                    {
                        Title = "Ghostbusters ",
                        ReleaseDate = DateTime.Parse("1984-3-13"),
                        Genre = "Comedy",
                        Rating = "R",
                        Price = 8.99M
                    },

                    new Movie
                    {
                        Title = "Ghostbusters 2",
                        ReleaseDate = DateTime.Parse("1986-2-23"),
                        Genre = "Comedy",
                        Rating = "A",
                        Price = 9.99M
                    },

                    new Movie
                    {
                        Title = "Rio Bravo",
                        ReleaseDate = DateTime.Parse("1959-4-15"),
                        Genre = "Western",
                        Rating = "B",
                        Price = 3.99M
                    }
                );
                _context.SaveChanges();
           
        }
    }
}
