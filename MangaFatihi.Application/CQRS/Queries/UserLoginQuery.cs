using FluentValidation;
using MangaFatihi.Application.Constants;
using MangaFatihi.Application.Models.Base;
using MangaFatihi.Application.Models.DTOs.Queries;
using MediatR;
using System.Runtime.Serialization;

namespace MangaFatihi.Application.CQRS.Queries
{
    public class UserLoginQuery : IRequest<DataResult<UserLoginQueryDto>>
    {
        public string Email { get; set; } = "";
        
        public string Password { get; set; } = "";

        [IgnoreDataMember]
        public string? IpAddress { get; set; } = "";
    }

    public class UserLoginQueryValidator : AbstractValidator<UserLoginQuery>
    {
        public UserLoginQueryValidator()
        {
            RuleFor(x => x.Email)
                .NotNull().WithMessage(ApplicationMessages.ErrorUserLoginQueryEmailIsNull.GetMessage())
                .NotEmpty().WithMessage(ApplicationMessages.ErrorUserLoginQueryEmailIsNull.GetMessage());

            RuleFor(x => x.Password)
                .NotNull().WithMessage(ApplicationMessages.ErrorUserLoginQueryPasswordIsNull.GetMessage())
                .NotEmpty().WithMessage(ApplicationMessages.ErrorUserLoginQueryPasswordIsNull.GetMessage());

        }
    }
}
