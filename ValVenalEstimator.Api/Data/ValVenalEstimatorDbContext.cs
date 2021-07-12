using Microsoft.EntityFrameworkCore;
using ValVenalEstimator.Api.Models;

namespace ValVenalEstimator.Api.Data
{
    public class ValVenalEstimatorDbContext : DbContext
    {
        public ValVenalEstimatorDbContext(DbContextOptions<ValVenalEstimatorDbContext> options) : base(options)
        {          
        }
        public DbSet<Place> Places { get; set; }
        public DbSet<Prefecture> Prefectures { get; set; }
        public DbSet<Zone> Zones { get; set; }
    }
}