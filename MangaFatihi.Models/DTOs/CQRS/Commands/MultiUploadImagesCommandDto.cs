namespace MangaFatihi.Models.DTOs.CQRS.Commands;

public class MultiUploadImagesCommandDto
{
    public Guid ImageId { get; set; }
    public string ImageName { get; set; } = "";
    public string ImageUrl { get; set; } = "";

}
