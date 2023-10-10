using FluentValidation;
using MangaFatihi.Shared.Models.DataResults;
using MangaFatihi.Shared.Models.Constants;
using Mediator;

namespace MangaFatihi.Shared.Models.Bindings.CQRS.Commands;

public class CreateSeriesArtistCommand : ICommand<DataResult<object>>
{
    /// <summary>
    /// Seri sanatçısının adı ve soyadı
    /// </summary>
    public string FullName { get; set; } = "";

    /// <summary>
    /// İlişkilendirilmek istenen serilerin unique id'si
    /// </summary>
    public List<Guid>? SeriesIds { get; set; } = new();

}

public class CreateSeriesArtistCommandValidator : AbstractValidator<CreateSeriesArtistCommand>
{
    public CreateSeriesArtistCommandValidator()
    {
        RuleFor(x => x.FullName)
            .NotNull().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "FullName"))
            .NotEmpty().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "FullName"));

    }
}

