using MangaFatihi.Domain.Entities;
using MangaFatihi.Domain.Entities.Identity;
using MangaFatihi.Shared.Domain.Entities.SeriesArtists;
using MangaFatihi.Shared.Domain.Entities.SeriesAuthors;
using MangaFatihi.Shared.Domain.Entities.SeriesCategories;
using MangaFatihi.Shared.Domain.Entities.SeriesEnty;
using MangaFatihi.Shared.Domain.Entities.SeriesEpisodes;
using MangaFatihi.Shared.Domain.Entities.Teams;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MangaFatihi.Shared.Domain.Interfaces
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
        public IGenericRepository<TeamAndAppUser> TeamAndAppUser { get; }
        public IGenericRepository<RefreshToken> RefreshToken { get; }

        public UserManager<AppUser> UserManager { get; }
        public RoleManager<AppRole> RoleManager { get; }

        public IHttpContextAccessor HttpContextAccessor { get; }

        Task<int> CommitAsync(CancellationToken cancellationToken = default);

        public DbContext DbContext { get; }

    }
}
