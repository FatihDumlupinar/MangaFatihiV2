using MangaFatihi.Models.Base;
using MangaFatihi.Models.CQRS.Bindings.Commands;
using Mediator;

namespace MangaFatihi.Application.Handlers.CQRS.Commands
{
    public class DeleteSeriesCommandHandler : ICommandHandler<DeleteSeriesCommand, DataResult<object>>
    {
        public ValueTask<DataResult<object>> Handle(DeleteSeriesCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
