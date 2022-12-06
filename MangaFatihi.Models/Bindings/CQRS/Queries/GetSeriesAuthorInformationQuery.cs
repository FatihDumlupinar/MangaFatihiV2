using FluentValidation;
using MangaFatihi.Domain.Constants;
using MangaFatihi.Models.Base;
using MangaFatihi.Models.DTOs.CQRS.Queries;
using Mediator;

namespace MangaFatihi.Models.Bindings.CQRS.Queries;

public class GetSeriesAuthorInformationQuery: IQuery<DataResult<GetSeriesAuthorInformationQueryDto>>
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
