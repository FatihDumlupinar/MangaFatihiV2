using FluentValidation;
using MangaFatihi.Shared.Models.DataResults;
using MangaFatihi.Shared.Models.Constants;
using Mediator;

namespace MangaFatihi.Shared.Models.Bindings.CQRS.Commands;

public class DeleteMultiPageCommand : ICommand<DataResult<object>>
{
    /// <summary>
    /// Sayfaların Unique Id'leri
    /// </summary>
    public List<Guid> PageIds { get; set; } = new();
}
public class DeleteMultiPageCommandValidator : AbstractValidator<DeleteMultiPageCommand>
{
    public DeleteMultiPageCommandValidator()
    {
        RuleFor(x => x.PageIds)
            .NotNull().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "PageIds"))
            .NotEmpty().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "PageIds"))
            .Must(i => i == null || !i.Any()).WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "PageIds"));

    }
}