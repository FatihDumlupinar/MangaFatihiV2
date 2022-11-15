using MangaFatihi.Application.Models.Base;
using MangaFatihi.Application.Models.DTOs.Queries;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace MangaFatihi.Application.CQRS.Queries
{
    public class UserLoginQuery : IRequest<DataResult<UserLoginQueryDto>>
    {
        [Required(ErrorMessage ="Eposta adresi zorunlu!")]
        public string Email { get; set; } = "";
        
        [Required(ErrorMessage = "Şifre zorunlu!")]
        public string Password { get; set; } = "";

        [IgnoreDataMember]
        public string? IpAddress { get; set; } = "";
    }
}
