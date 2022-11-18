using Microsoft.EntityFrameworkCore;

namespace Ok.Tech.Infra.Data.Contexts
{
  public class ApiDbContext : DbContext
  {
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApiDbContext).Assembly);
      base.OnModelCreating(modelBuilder);
    }
  }
}
