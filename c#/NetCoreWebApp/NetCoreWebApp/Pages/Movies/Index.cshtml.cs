using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NetCoreWebApp.Data;
using NetCoreWebApp.Models;

namespace NetCoreWebApp.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly NetCoreWebApp.Data.MoviesContext _context;

        public IndexModel(NetCoreWebApp.Data.MoviesContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? DescriptionString { get; set; }

        public SelectList? SelectList { get; set; }   //页面select下拉选项

        public async Task OnGetAsync()
        {
            IQueryable<string> genreQuery = from m in _context.Movie
                orderby m.Description
                select m.Description;

            var movies = from m in _context.Movie select m;
            if (!string.IsNullOrEmpty(SearchString))
            {
                movies = movies.Where(s=>s.Title.Contains(SearchString));
            }

            if (!string.IsNullOrEmpty(DescriptionString))
            {
                movies = movies.Where(x => x.Description == DescriptionString);
            }

            SelectList = new SelectList(await genreQuery.Distinct().ToListAsync());
            // IList<string> items = new List<string>();
            // items.Add("Hello");
            // SelectList = new SelectList(items);
            if (movies != null)
            {
                Movie = await movies.ToListAsync();
            }
        }
    }
}
