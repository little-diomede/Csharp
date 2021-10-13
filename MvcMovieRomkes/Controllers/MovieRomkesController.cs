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
    public class MovieRomkesController : Controller
    {
        private readonly MvcMovieRomkesContext _context;

        public MovieRomkesController(MvcMovieRomkesContext context)
        {
            _context = context;
        }

        // GET: MovieRomkes
        // GET: Movies
        public async Task<IActionResult> Index(string movieGenre, string searchString)
        {
            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from m in _context.MovieRomkes
                                            orderby m.Genre
                                            select m.Genre;

            var movies = from m in _context.MovieRomkes
                         select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => x.Genre == movieGenre);
            }

            var movieRomkesGenreVM = new MovieRomkesGenreViewModel
            {
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Movies = await movies.ToListAsync(),
                
            };

            return View(movieRomkesGenreVM);
        }

        // GET: MovieRomkes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieRomkes = await _context.MovieRomkes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movieRomkes == null)
            {
                return NotFound();
            }

            return View(movieRomkes);
        }

        // GET: MovieRomkes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MovieRomkes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] MovieRomkes movieRomkes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movieRomkes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movieRomkes);
        }

        // GET: MovieRomkes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieRomkes = await _context.MovieRomkes.FindAsync(id);
            if (movieRomkes == null)
            {
                return NotFound();
            }
            return View(movieRomkes);
        }

        // POST: MovieRomkes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price")] MovieRomkes movieRomkes)
        {
            if (id != movieRomkes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movieRomkes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieRomkesExists(movieRomkes.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movieRomkes);
        }

        // GET: MovieRomkes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieRomkes = await _context.MovieRomkes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movieRomkes == null)
            {
                return NotFound();
            }

            return View(movieRomkes);
        }

        // POST: MovieRomkes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, bool notUsed)
        {
            var movieRomkes = await _context.MovieRomkes.FindAsync(id);
            _context.MovieRomkes.Remove(movieRomkes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieRomkesExists(int id)
        {
            return _context.MovieRomkes.Any(e => e.Id == id);
        }
    }
}
