using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DictionaryApp.Models;

namespace DictionaryApp.Data
{
    public class DictionaryAppContext : DbContext
    {
        public DictionaryAppContext (DbContextOptions<DictionaryAppContext> options)
            : base(options)
        {
        }

        public DbSet<DictionaryApp.Models.Text> Text { get; set; } = default!;
    }
}
