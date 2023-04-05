using MangaFatihi.Models.Base;
using MangaFatihi.Models.DTOs.CQRS.Commands;
using Mediator;

namespace MangaFatihi.Models.Bindings.CQRS.Commands;

public class UploadImageCommand : ICommand<DataResult<UploadImageCommandDto>>
{
    /// <summary>
    /// Dosyanın adresi
    /// </summary>
    public string? File { get; set; } = "";
}
