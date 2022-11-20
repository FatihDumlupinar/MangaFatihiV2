﻿using FluentValidation;
using MangaFatihi.Application.Constants;
using MangaFatihi.Application.Models.Base;
using MangaFatihi.Application.Models.DTOs.Queries;
using MediatR;
using System.Runtime.Serialization;

namespace MangaFatihi.Application.CQRS.Queries
{
    public class RefreshTokenLoginQuery : IRequest<DataResult<RefreshTokenLoginQueryDto>>
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
                .Must(x => Guid.TryParse(x, out _)).WithMessage(ApplicationMessages.ErrorRefreshTokenQueryRefreshTokenTypeError.GetMessage())
                .NotNull().WithMessage(ApplicationMessages.ErrorRefreshTokenQueryRefreshTokenIsNull.GetMessage())
                .NotEmpty().WithMessage(ApplicationMessages.ErrorRefreshTokenQueryRefreshTokenIsNull.GetMessage());

        }
    }
}
