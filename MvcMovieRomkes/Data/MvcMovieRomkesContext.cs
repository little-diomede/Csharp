using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcMovieRomkes.Models;

namespace MvcMovieRomkes.Data
{
    public class MvcMovieRomkesContext : DbContext
    {
        public MvcMovieRomkesContext (DbContextOptions<MvcMovieRomkesContext> options)
            : base(options)
        {
        }

        public DbSet<MvcMovieRomkes.Models.MovieRomkes> MovieRomkes { get; set; }
    }
}
