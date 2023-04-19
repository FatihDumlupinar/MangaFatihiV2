using MangaFatihi.Models.Commonns;

namespace MangaFatihi.Models.DTOs.CQRS.Commands;

public class MultiUploadSeriesEpisodeImagesCommandDto
{
    public List<UploadSeriesEpisodeModel> List { get; set; }

}
