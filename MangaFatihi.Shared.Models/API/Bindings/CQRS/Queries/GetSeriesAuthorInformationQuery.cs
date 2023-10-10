using FluentValidation;
using MangaFatihi.Shared.Models.DataResults;
using MangaFatihi.Shared.Models.Constants;
using Mediator;
using MangaFatihi.Shared.Models.API.DTOs.CQRS.Queries;

namespace MangaFatihi.Shared.Models.Bindings.CQRS.Queries;

public class GetSeriesAuthorInformationQuery : IQuery<DataResult<GetSeriesAuthorInformationQueryDto>>
{
    /// <summary>
    /// Seri yazarının Unique Id'si
    /// </summary>
    public string SeriesAuthorId { get; set; } = "";
}

public class GetSeriesAuthorInformationQueryValidator : AbstractValidator<GetSeriesAuthorInformationQuery>
{
    public GetSeriesAuthorInformationQueryValidator()
    {
        RuleFor(x => x.SeriesAuthorId)
            .Must(x => Guid.TryParse(x, out _)).WithMessage(string.Format(ApplicationMessages.ErrorDefaultTypeError.GetMessage(), "SeriesArtistId"))
            .NotNull().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "SeriesAuthorId"))
            .NotEmpty().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "SeriesAuthorId"));

    }
}
