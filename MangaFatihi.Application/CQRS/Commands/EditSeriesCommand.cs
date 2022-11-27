using MangaFatihi.Application.Models.Base;
using MangaFatihi.Application.Models.DTOs.Commands;
using MediatR;

namespace MangaFatihi.Application.CQRS.Commands
{
    public class EditSeriesCommand : IRequest<DataResult<EditSeriesCommandDto>>
    {
    }
}
