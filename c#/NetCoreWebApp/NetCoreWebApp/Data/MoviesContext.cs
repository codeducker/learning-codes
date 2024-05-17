using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NetCoreWebApp.Models;

namespace NetCoreWebApp.Data
{
    public class MoviesContext : DbContext
    {
        public MoviesContext (DbContextOptions<MoviesContext> options)
            : base(options)
        {
        }

        public DbSet<NetCoreWebApp.Models.Movie> Movie { get; set; } = default!;
    }
}
