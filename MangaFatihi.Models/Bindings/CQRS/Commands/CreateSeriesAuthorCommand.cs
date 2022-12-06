using FluentValidation;
using MangaFatihi.Domain.Constants;
using MangaFatihi.Models.Base;
using Mediator;

namespace MangaFatihi.Models.Bindings.CQRS.Commands;

public class CreateSeriesAuthorCommand : ICommand<DataResult<object>>
{
    /// <summary>
    /// Seri yazarının adı ve soyadı
    /// </summary>
    public string FullName { get; set; } = "";

    /// <summary>
    /// İlişkilendirilmek istenen serilerin unique id'si
    /// </summary>
    public List<Guid>? SeriesIds { get; set; } = new();

}
public class CreateSeriesAuthorCommandValidator : AbstractValidator<CreateSeriesAuthorCommand>
{
    public CreateSeriesAuthorCommandValidator()
    {
        RuleFor(x => x.FullName)
            .NotNull().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "FullName"))
            .NotEmpty().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "FullName"));

    }
}
