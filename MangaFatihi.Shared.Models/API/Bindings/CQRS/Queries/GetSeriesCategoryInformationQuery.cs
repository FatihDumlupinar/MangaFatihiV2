using FluentValidation;
using MangaFatihi.Shared.Models.API.DTOs.CQRS.Queries;
using MangaFatihi.Shared.Models.Constants;
using MangaFatihi.Shared.Models.DataResults;
using Mediator;

namespace MangaFatihi.Shared.Models.Bindings.CQRS.Queries;

public class GetSeriesCategoryInformationQuery : IQuery<DataResult<GetSeriesCategoryInformationQueryDto>>
    {
        /// <summary>
        /// Seri kategorisinin Unique Id'si
        /// </summary>
        public string SeriesCategoryId { get; set; } = "";
    }

    public class GetSeriesCategoryInformationQueryValidator : AbstractValidator<GetSeriesCategoryInformationQuery>
    {
        public GetSeriesCategoryInformationQueryValidator()
        {
            RuleFor(x => x.SeriesCategoryId)
                .Must(x => Guid.TryParse(x, out _)).WithMessage(string.Format(ApplicationMessages.ErrorDefaultTypeError.GetMessage(), "SeriesCategoryId"))
                .NotNull().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "SeriesCategoryId"))
                .NotEmpty().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "SeriesCategoryId"));

        }
    }
