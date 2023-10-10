using MangaFatihi.Domain.Entities;
using MangaFatihi.Shared.Models.API.Authentication;

namespace MangaFatihi.Management.Application.Handlers.Auth
{
    public interface ITokenHandler
    {
        Task<TokenModel> CreateAccessTokenAsync(AppUser appUser, string? ipAddress, CancellationToken cancellationToken = default);

    }
}