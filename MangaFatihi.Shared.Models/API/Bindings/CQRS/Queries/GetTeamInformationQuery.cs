using FluentValidation;
using MangaFatihi.Shared.Models.API.DTOs.CQRS.Queries;
using MangaFatihi.Shared.Models.Constants;
using MangaFatihi.Shared.Models.DataResults;
using Mediator;

namespace MangaFatihi.Shared.Models.Bindings.CQRS.Queries;

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
