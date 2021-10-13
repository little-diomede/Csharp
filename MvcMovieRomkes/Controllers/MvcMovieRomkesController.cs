using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovieRomkes.Data;
using MvcMovieRomkes.Models;


namespace MvcMovieRomkes.Controllers
{
    public class MvcMovieRomkesController : Controller
    {
        private readonly MvcMovieRomkesContext _context;

        public MvcMovieRomkesController(MvcMovieRomkesContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string searchString)
        {
            var movies = from m in _context.MovieRomkes
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }

            return View(await movies.ToListAsync());
        }

        public IActionResult Welcome(string name, int numTimes = 1)
        {
            ViewData["Message"] = "Hello " + name;
            ViewData["NumTimes"] = numTimes;

            return View();
        }
    }
}
