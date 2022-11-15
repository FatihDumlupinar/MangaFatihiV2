using MangaFatihi.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace MangaFatihi.Domain.Entities
{
    public class StaticSeriesType : BaseEntity
    {
        [Required]
        [MinLength(0)]
        public int No { get; set; } = 0;

        [Required]
        public string Name { get; set; } = "";
    }
}
