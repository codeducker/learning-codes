using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcWebApplication.Models;

namespace MvcWebApplication.Data
{
    public class MovieContext : DbContext
    {
        public MovieContext (DbContextOptions<MovieContext> options)
            : base(options)
        {
        }

        public DbSet<MvcWebApplication.Models.Movie> Movie { get; set; } = default!;
    }
}
