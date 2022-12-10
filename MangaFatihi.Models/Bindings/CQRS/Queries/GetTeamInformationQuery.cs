using FluentValidation;
using MangaFatihi.Domain.Constants;
using MangaFatihi.Models.Base;
using MangaFatihi.Models.DTOs.CQRS.Queries;
using Mediator;

namespace MangaFatihi.Models.Bindings.CQRS.Queries;

public class GetTeamInformationQuery : IQuery<DataResult<GetTeamInformationQueryDto>>
{
    /// <summary>
    /// Ekip unique id'si
    /// </summary>
    public string TeamId { get; set; } = "";
}

public class GetTeamInformationQueryValidator : AbstractValidator<GetTeamInformationQuery>
{
    public GetTeamInformationQueryValidator()
    {
        RuleFor(x => x.TeamId)
            .Must(x => Guid.TryParse(x, out _)).WithMessage(string.Format(ApplicationMessages.ErrorDefaultTypeError.GetMessage(), "TeamId"))
            .NotNull().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "TeamId"))
            .NotEmpty().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "TeamId"));

    }
}
