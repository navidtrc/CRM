using CRM.Infrastructure.Persistance.Core;
using MediatR.Pipeline;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Application.Common
{
    public class CommitCommandPostProcessor<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
    {
        private readonly IUnitOfWork _uow;

        public CommitCommandPostProcessor(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
        {
            if (request is ICommittableRequest)
            {
                var result = await _uow.CompleteAsync(cancellationToken);
            }
        }
    }
}
