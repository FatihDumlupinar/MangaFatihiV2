using MangaFatihi.Application.CQRS.Commands;
using MangaFatihi.Application.Models.Base;
using MangaFatihi.Application.Models.DTOs.Commands;
using MediatR;

namespace MangaFatihi.Application.Handlers.Commands
{
    public class EditSeriesCommandHandler : IRequestHandler<EditSeriesCommand, DataResult<EditSeriesCommandDto>>
    {
        public Task<DataResult<EditSeriesCommandDto>> Handle(EditSeriesCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
