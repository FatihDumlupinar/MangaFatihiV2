﻿using FluentValidation;
using MangaFatihi.Application.Models.DTOs.Queries;
using MangaFatihi.Domain.Constants;
using MangaFatihi.Models.Base;
using Mediator;
using System.Runtime.Serialization;

namespace MangaFatihi.Models.CQRS.Bindings.Queries
{
    public class RefreshTokenLoginQuery : IQuery<DataResult<RefreshTokenLoginQueryDto>>
    {
        public string RefreshToken { get; set; } = "";

        [IgnoreDataMember]
        public string? IpAddress { get; set; } = "";
    }

    public class RefreshTokenLoginQueryValidator : AbstractValidator<RefreshTokenLoginQuery>
    {
        public RefreshTokenLoginQueryValidator()
        {
            RuleFor(x => x.RefreshToken)
                .Must(x => Guid.TryParse(x, out _)).WithMessage(string.Format(ApplicationMessages.ErrorDefaultTypeError.GetMessage(), "RefreshToken"))
                .NotNull().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "RefreshToken"))
                .NotEmpty().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "RefreshToken"));

        }
    }
}