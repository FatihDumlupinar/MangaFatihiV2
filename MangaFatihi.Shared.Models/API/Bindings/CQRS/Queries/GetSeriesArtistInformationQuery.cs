using FluentValidation;
using MangaFatihi.Shared.Models.API.DTOs.CQRS.Queries;
using MangaFatihi.Shared.Models.Constants;
using MangaFatihi.Shared.Models.DataResults;
using Mediator;

namespace MangaFatihi.Shared.Models.Bindings.CQRS.Queries;

public class GetSeriesArtistInformationQuery : IQuery<DataResult<GetSeriesArtistInformationQueryDto>>
{
    /// <summary>
    /// Seri sanatçısının unique id'si
    /// </summary>
    public string SeriesArtistId { get; set; } = "";
}

public class GetSeriesArtistInformationQueryValidator : AbstractValidator<GetSeriesArtistInformationQuery>
{
    public GetSeriesArtistInformationQueryValidator()
    {
        RuleFor(x => x.SeriesArtistId)
            .Must(x => Guid.TryParse(x, out _)).WithMessage(string.Format(ApplicationMessages.ErrorDefaultTypeError.GetMessage(), "SeriesArtistId"))
            .NotNull().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "SeriesArtistId"))
            .NotEmpty().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "SeriesArtistId"));

    }
}
