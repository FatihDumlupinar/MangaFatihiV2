using MangaFatihi.Application.CQRS.Commands;
using MangaFatihi.Application.Models.Base;
using MangaFatihi.Application.Models.DTOs.Commands;
using MediatR;

namespace MangaFatihi.Application.Handlers.Commands
{
    public class CreateSeriesCommandHandler : IRequestHandler<CreateSeriesCommand, DataResult<CreateSeriesCommandDto>>
    {
        public Task<DataResult<CreateSeriesCommandDto>> Handle(CreateSeriesCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
