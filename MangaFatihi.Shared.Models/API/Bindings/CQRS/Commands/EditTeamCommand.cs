using FluentValidation;
using MangaFatihi.Shared.Models.Constants;
using MangaFatihi.Shared.Models.DataResults;
using Mediator;

namespace MangaFatihi.Shared.Models.Bindings.CQRS.Commands;

public class EditTeamCommand : ICommand<DataResult<object>>
{
    /// <summary>
    /// Ekip unique id'si
    /// </summary>
    public string TeamId { get; set; } = "";

    /// <summary>
    /// Takımın Adı
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>
    /// Takımın Web Site Adresi
    /// </summary>
    public string? WebSiteUrl { get; set; } = "";

    /// <summary>
    /// Takımın profil sayfasındaki arka plan resmi
    /// </summary>
    public string? BackgroundImageUrl { get; set; } = "";

    /// <summary>
    /// Takımın profil resmi
    /// </summary>
    public string? ProfileImageUrl { get; set; } = "";

    /// <summary>
    /// Takımın açıklaması/hakkında bilgisi
    /// </summary>
    public string? Description { get; set; } = "";

    /// <summary>
    /// Takımın Üyeleri
    /// </summary>
    public List<Guid> UserList { get; set; } = new();

}

public class EditTeamCommandValidator : AbstractValidator<EditTeamCommand>
{
    public EditTeamCommandValidator()
    {
        RuleFor(x => x.TeamId)
           .Must(x => Guid.TryParse(x, out _)).WithMessage(string.Format(ApplicationMessages.ErrorDefaultTypeError.GetMessage(), "TeamId"))
           .NotNull().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "TeamId"))
           .NotEmpty().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "TeamId"));

        RuleFor(x => x.Name)
            .NotNull()
            .WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "Name"))
            .WithErrorCode(ApplicationMessages.ErrorDefaultIsNull)
            .NotEmpty()
            .WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "Name"))
            .WithErrorCode(ApplicationMessages.ErrorDefaultIsNull);

    }

}
