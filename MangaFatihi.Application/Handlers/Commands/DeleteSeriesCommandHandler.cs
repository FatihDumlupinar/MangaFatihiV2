using MangaFatihi.Application.CQRS.Commands;
using MangaFatihi.Application.Models.Base;
using MangaFatihi.Application.Models.DTOs.Commands;
using MediatR;

namespace MangaFatihi.Application.Handlers.Commands
{
    public class DeleteSeriesCommandHandler : IRequestHandler<DeleteSeriesCommand, DataResult<DeleteSeriesCommandDto>>
    {
        public Task<DataResult<DeleteSeriesCommandDto>> Handle(DeleteSeriesCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
