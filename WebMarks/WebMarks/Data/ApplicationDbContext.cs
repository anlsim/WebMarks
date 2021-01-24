using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using PinBoard.Models;

namespace PinBoard.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<PinBoard.Models.Pin> Pin { get; set; }
        public DbSet<PinBoard.Models.Board> Board { get; set; }
    }
}
