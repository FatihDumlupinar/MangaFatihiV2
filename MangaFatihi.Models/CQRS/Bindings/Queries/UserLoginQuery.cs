using FluentValidation;
using MangaFatihi.Application.Models.DTOs.Queries;
using MangaFatihi.Domain.Constants;
using MangaFatihi.Models.Base;
using Mediator;
using System.Runtime.Serialization;

namespace MangaFatihi.Models.CQRS.Bindings.Queries
{
    public class UserLoginQuery : IQuery<DataResult<UserLoginQueryDto>>
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
