using MangaFatihi.Models.Base;
using MangaFatihi.Models.CQRS.Bindings.Commands;
using Mediator;

namespace MangaFatihi.Application.Handlers.CQRS.Commands
{
    public class EditSeriesCommandHandler : ICommandHandler<EditSeriesCommand, DataResult<object>>
    {
        public ValueTask<DataResult<object>> Handle(EditSeriesCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
