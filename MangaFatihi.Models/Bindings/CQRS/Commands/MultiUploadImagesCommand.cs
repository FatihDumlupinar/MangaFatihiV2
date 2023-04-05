using MangaFatihi.Models.Base;
using MangaFatihi.Models.DTOs.CQRS.Commands;
using Mediator;

namespace MangaFatihi.Models.Bindings.CQRS.Commands;

public class MultiUploadImagesCommand : ICommand<DataResult<MultiUploadImagesCommandDto>>
{
    /// <summary>
    /// Dosyaların adresleri
    /// </summary>
    public List<string> Files { get; set; } = new();
}
