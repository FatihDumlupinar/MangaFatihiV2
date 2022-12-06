using FluentValidation;
using MangaFatihi.Domain.Constants;
using MangaFatihi.Models.Base;
using Mediator;

namespace MangaFatihi.Models.Bindings.CQRS.Commands;

public class CreateSeriesCategoryCommand : ICommand<DataResult<object>>
{
    /// <summary>
    /// Seri kategorisinin adı
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>
    /// İlişkilendirilmek istenen serilerin unique id'si
    /// </summary>
    public List<Guid>? SeriesIds { get; set; } = new();

}
public class CreateSeriesCategoryCommandValidator : AbstractValidator<CreateSeriesCategoryCommand>
{
    public CreateSeriesCategoryCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotNull().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "Name"))
            .NotEmpty().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "Name"));

    }
}
