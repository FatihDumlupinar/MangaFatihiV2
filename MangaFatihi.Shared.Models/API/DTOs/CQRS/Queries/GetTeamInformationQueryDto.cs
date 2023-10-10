using MangaFatihi.Shared.Models.API.Commons.TeamUsers;

namespace MangaFatihi.Shared.Models.API.DTOs.CQRS.Queries;

public class GetTeamInformationQueryDto
{
    /// <summary>
    /// Takımın Unique Id'si
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Takımın adı
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>
    /// Takımın üyeleri
    /// </summary>
    public List<TeamUserListModel> UserList { get; set; } = new();

}
