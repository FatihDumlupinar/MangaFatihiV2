using FluentValidation;
using MangaFatihi.Shared.Models.API.DTOs.CQRS.Queries;
using MangaFatihi.Shared.Models.Constants;
using MangaFatihi.Shared.Models.DataResults;
using Mediator;

namespace MangaFatihi.Shared.Models.Bindings.CQRS.Queries;

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
