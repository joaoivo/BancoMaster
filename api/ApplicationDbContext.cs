using Entities;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext:DbContext{
	public DbSet<EN_Route>? routes_dbSet {get; set;}
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options){

	}
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
		  optionsBuilder.UseSqlServer();
    }
}