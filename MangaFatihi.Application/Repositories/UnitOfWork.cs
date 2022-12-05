﻿using MangaFatihi.Domain.Entities;
using MangaFatihi.Domain.Entities.Identity;
using MangaFatihi.Domain.Interfaces;
using MangaFatihi.Persistence.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MangaFatihi.Application.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Ctor&Fields

        protected readonly WriteDbContext _writeDbContext;
        protected readonly ReadOnlyDbContext _readOnlyDbContext;

        private readonly IGenericRepository<Series> _series;
        private readonly IGenericRepository<SeriesArtist> _seriesArtist;
        private readonly IGenericRepository<SeriesAuthor> _seriesAuthor;
        private readonly IGenericRepository<SeriesCategory> _seriesCategory;
        private readonly IGenericRepository<SeriesEpisode> _seriesEpisode;
        private readonly IGenericRepository<SeriesEpisodesPage> _seriesEpisodesPage;
        private readonly IGenericRepository<Team> _team;
        private readonly IGenericRepository<RefreshToken> _refreshToken;
        private readonly IGenericRepository<SeriesAndSeriesArtist> _seriesAndSeriesArtist;
        private readonly IGenericRepository<SeriesAndSeriesAuthor> _seriesAndSeriesAuthor;
        private readonly IGenericRepository<SeriesAndSeriesCategory> _seriesAndSeriesCategory;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public UnitOfWork(WriteDbContext writeDbContext, ReadOnlyDbContext readOnlyDbContext, IGenericRepository<Series> series, IGenericRepository<SeriesArtist> seriesArtist, IGenericRepository<SeriesAuthor> seriesAuthor, IGenericRepository<SeriesCategory> seriesCategory, IGenericRepository<SeriesEpisode> seriesEpisode, IGenericRepository<SeriesEpisodesPage> seriesEpisodesPage, IGenericRepository<Team> team, IGenericRepository<RefreshToken> refreshToken, IGenericRepository<SeriesAndSeriesArtist> seriesAndSeriesArtist, IGenericRepository<SeriesAndSeriesAuthor> seriesAndSeriesAuthor, IGenericRepository<SeriesAndSeriesCategory> seriesAndSeriesCategory, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _writeDbContext = writeDbContext;
            _readOnlyDbContext = readOnlyDbContext;
            _series = series;
            _seriesArtist = seriesArtist;
            _seriesAuthor = seriesAuthor;
            _seriesCategory = seriesCategory;
            _seriesEpisode = seriesEpisode;
            _seriesEpisodesPage = seriesEpisodesPage;
            _team = team;
            _refreshToken = refreshToken;
            _seriesAndSeriesArtist = seriesAndSeriesArtist;
            _seriesAndSeriesAuthor = seriesAndSeriesAuthor;
            _seriesAndSeriesCategory = seriesAndSeriesCategory;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        #endregion

        #region Properties

        public IGenericRepository<Series> Series => _series;

        public IGenericRepository<SeriesArtist> SeriesArtist => _seriesArtist;

        public IGenericRepository<SeriesAuthor> SeriesAuthor => _seriesAuthor;

        public IGenericRepository<SeriesCategory> SeriesCategory => _seriesCategory;

        public IGenericRepository<SeriesEpisode> SeriesEpisode => _seriesEpisode;

        public IGenericRepository<SeriesEpisodesPage> SeriesEpisodesPage => _seriesEpisodesPage;

        public IGenericRepository<Team> Team => _team;

        public IGenericRepository<RefreshToken> RefreshToken => _refreshToken;

        public IGenericRepository<SeriesAndSeriesArtist> SeriesAndSeriesArtist => _seriesAndSeriesArtist;

        public IGenericRepository<SeriesAndSeriesAuthor> SeriesAndSeriesAuthor => _seriesAndSeriesAuthor;

        public IGenericRepository<SeriesAndSeriesCategory> SeriesAndSeriesCategory => _seriesAndSeriesCategory;

        public DbContext DbContext => _readOnlyDbContext;

        public UserManager<AppUser> UserManager => _userManager;

        public RoleManager<AppRole> RoleManager => _roleManager;


        #endregion

        /// <summary>
        /// Veritabanı için olan değişiklikleri veritabanına gönderir.
        /// </summary>
        /// <returns>Kaç verinin etkilendiğini geriye döndürür</returns>
        public Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            return _writeDbContext.SaveChangesAsync(cancellationToken);
        }

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _writeDbContext.Dispose();
                    _readOnlyDbContext.Dispose();
                }

                disposedValue = true;
            }
        }

        /// <summary>
        /// Veritabanı için oluşturulmuş DbContext'leri Dispose eder. (Dispose = kullanılan kaynağı serbest bırakır.)
        /// </summary>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
