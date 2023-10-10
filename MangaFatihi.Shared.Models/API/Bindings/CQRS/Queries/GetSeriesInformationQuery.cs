using FluentValidation;
using Mediator;
using MangaFatihi.Shared.Models.Constants;
using MangaFatihi.Shared.Models.DataResults;
using MangaFatihi.Shared.Models.API.DTOs.CQRS.Queries;

namespace MangaFatihi.Shared.Models.Bindings.CQRS.Queries;

public class GetSeriesInformationQuery : IQuery<DataResult<GetSeriesInformationQueryDto>>
{
    /// <summary>
    /// Serinin unique id'si
    /// </summary>
    public string SeriesId { get; set; } = "";
}

public class GetSeriesInformationQueryValidator : AbstractValidator<GetSeriesInformationQuery>
{
    public GetSeriesInformationQueryValidator()
    {
        RuleFor(x => x.SeriesId)
            .Must(x => Guid.TryParse(x, out _)).WithMessage(string.Format(ApplicationMessages.ErrorDefaultTypeError.GetMessage(), "SeriesId"))
            .NotNull().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "SeriesId"))
            .NotEmpty().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "SeriesId"));

    }
}

