using Microsoft.EntityFrameworkCore;
using world.Model;

namespace world.Data
{
	public class ApplicationDbContext:DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
		{

		}
		public DbSet<Country> Countries{ get; set; }
	}
}
