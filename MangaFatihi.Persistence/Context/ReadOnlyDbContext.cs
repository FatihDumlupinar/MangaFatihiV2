using Microsoft.EntityFrameworkCore;

namespace MangaFatihi.Persistence.Context
{
    public class ReadOnlyDbContext : BaseDbContext
    {
        public ReadOnlyDbContext(DbContextOptions<ReadOnlyDbContext> options) : base(options)
        {
        }
    }
}
