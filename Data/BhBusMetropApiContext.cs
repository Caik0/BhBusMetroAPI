using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BhBusMetropApi.Models;

namespace BhBusMetropApi.Data
{
    public class BhBusMetropApiContext : DbContext
    {
        public BhBusMetropApiContext (DbContextOptions<BhBusMetropApiContext> options)
            : base(options)
        {
        }

        public DbSet<BhBusMetropApi.Models.Onibus> Onibus { get; set; } = default!;
    }
}
