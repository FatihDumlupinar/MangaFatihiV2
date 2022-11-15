using MangaFatihi.Application.Models.Auth;
using MangaFatihi.Domain.Entities;

namespace MangaFatihi.Application.Handlers.Auth
{
    public interface ITokenHandler
    {
        Task<TokenModel> CreateAccessTokenAsync(AppUser appUser, string? ipAddress, CancellationToken cancellationToken = default);

    }
}