using mccotter_net_api.Models;
using Microsoft.EntityFrameworkCore;

namespace mccotter_net_api.DataAccess
{
    public class PostgreSqlContext: DbContext
    {
        public PostgreSqlContext(DbContextOptions<PostgreSqlContext> options) : base(options)  
        {  
        }  

        public DbSet<Disc> discs { get; set; }  

        protected override void OnModelCreating(ModelBuilder builder)  
        {  
            base.OnModelCreating(builder);  
        }  

        public override int SaveChanges()  
        {  
            ChangeTracker.DetectChanges();  
            return base.SaveChanges();  
        } 

    }
}