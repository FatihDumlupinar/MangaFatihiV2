using FluentValidation;
using MangaFatihi.Domain.Constants;
using MangaFatihi.Models.Base;
using Mediator;

namespace MangaFatihi.Models.Bindings.CQRS.Commands;

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