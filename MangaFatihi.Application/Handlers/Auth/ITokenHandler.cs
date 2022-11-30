using MangaFatihi.Domain.Entities;
using MangaFatihi.Models.Auth;

namespace MangaFatihi.Application.Handlers.Auth
{
    public interface ITokenHandler
    {
        Task<TokenModel> CreateAccessTokenAsync(AppUser appUser, string? ipAddress, CancellationToken cancellationToken = default);

    }
}