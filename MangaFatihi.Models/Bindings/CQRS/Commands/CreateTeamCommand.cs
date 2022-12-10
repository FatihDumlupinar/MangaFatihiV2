using FluentValidation;
using MangaFatihi.Domain.Constants;
using MangaFatihi.Models.Base;
using Mediator;

namespace MangaFatihi.Models.Bindings.CQRS.Commands;

public class CreateTeamCommand : ICommand<DataResult<object>>
{
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
    public List<Guid>? UserList { get; set; } = new();

}

public class CreateTeamCommandValidator : AbstractValidator<CreateTeamCommand>
{
    public CreateTeamCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "Name"))
            .WithErrorCode(ApplicationMessages.ErrorDefaultIsNull)
            .NotEmpty()
            .WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "Name"))
            .WithErrorCode(ApplicationMessages.ErrorDefaultIsNull);

    }

}