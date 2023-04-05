using FluentValidation;
using MangaFatihi.Domain.Constants;
using MangaFatihi.Models.Base;
using MangaFatihi.Models.DTOs.CQRS.Commands;
using Mediator;

namespace MangaFatihi.Models.Bindings.CQRS.Queries;

public class GetSeriesEpisodeInformationQuery : IQuery<DataResult<GetSeriesEpisodeInformationQueryDto>>
{
    /// <summary>
    /// Seri kategorisinin Unique Id'si
    /// </summary>
    public string SeriesEpisodeId { get; set; } = "";
}

public class GetSeriesEpisodeInformationQueryValidator : AbstractValidator<GetSeriesEpisodeInformationQuery>
{
    public GetSeriesEpisodeInformationQueryValidator()
    {
        RuleFor(x => x.SeriesEpisodeId)
            .Must(x => Guid.TryParse(x, out _)).WithMessage(string.Format(ApplicationMessages.ErrorDefaultTypeError.GetMessage(), "SeriesEpisodeId"))
            .NotNull().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "SeriesEpisodeId"))
            .NotEmpty().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "SeriesEpisodeId"));

    }
}
