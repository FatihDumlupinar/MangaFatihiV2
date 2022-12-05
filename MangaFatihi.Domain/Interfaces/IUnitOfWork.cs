using MangaFatihi.Domain.Entities;
using MangaFatihi.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MangaFatihi.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IGenericRepository<Series> Series { get; }
        public IGenericRepository<SeriesAndSeriesArtist> SeriesAndSeriesArtist { get; }
        public IGenericRepository<SeriesAndSeriesAuthor> SeriesAndSeriesAuthor { get; }
        public IGenericRepository<SeriesAndSeriesCategory> SeriesAndSeriesCategory { get; }
        public IGenericRepository<SeriesArtist> SeriesArtist { get; }
        public IGenericRepository<SeriesAuthor> SeriesAuthor { get; }
        public IGenericRepository<SeriesCategory> SeriesCategory { get; }
        public IGenericRepository<SeriesEpisode> SeriesEpisode { get; }
        public IGenericRepository<SeriesEpisodesPage> SeriesEpisodesPage { get; }
        public IGenericRepository<Team> Team { get; }
        public IGenericRepository<RefreshToken> RefreshToken { get; }

        public UserManager<AppUser> UserManager { get; }
        public RoleManager<AppRole> RoleManager { get; }

        Task<int> CommitAsync(CancellationToken cancellationToken = default);

        public DbContext DbContext { get; }

    }
}
