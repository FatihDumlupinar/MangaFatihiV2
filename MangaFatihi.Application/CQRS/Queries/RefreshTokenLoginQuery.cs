using MangaFatihi.Application.Models.Base;
using MangaFatihi.Application.Models.DTOs.Queries;
using MediatR;
using System.Runtime.Serialization;

namespace MangaFatihi.Application.CQRS.Queries
{
    public class RefreshTokenLoginQuery : IRequest<DataResult<RefreshTokenLoginQueryDto>>
    {
        public string RefreshToken { get; set; } = "";

        public string AccessToken { get; set; } = "";

        [IgnoreDataMember]
        public string? IpAddress { get; set; } = "";
    }
}
