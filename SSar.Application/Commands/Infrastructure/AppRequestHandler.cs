using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace SSar.Application.Commands.Infrastructure
{
    public abstract class AppRequestHandler<TRequest, TResponse>
        : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            TResponse result = await HandleCore(request, cancellationToken);

            return result;
        }

        protected abstract Task<TResponse> HandleCore(TRequest request, CancellationToken cancellationToken);

        //protected async Task<AggregateResult<TAggregate>> Execute<TDbContext, TAggregate>(
        //    TDbContext dbContext, Guid id, Func<TAggregate, AggregateResult<TAggregate>> func)
        //    where TAggregate : AggregateRoot
        //    where TDbContext : DbContext
        //{
        //    AggregateResult<TAggregate> result;

        //    try
        //    {
        //        var aggregate = await dbContext.Set<TAggregate>().FindAsync(id);
        //        result = func(aggregate);
        //        await dbContext.SaveChangesAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        result = AggregateResult<TAggregate>.FromException(ex, "An exception occurred in HandleCore.Execute while executing a command.");
        //    }

        //    return result;
        //}

        //protected async Task<AggregateResult<TAggregate>> Create<TDbContext, TAggregate>(
        //    TDbContext dbContext, Guid id, Func<AggregateResult<TAggregate>> func)
        //    where TAggregate : AggregateRoot
        //    where TDbContext : DbContext
        //{
        //    AggregateResult<TAggregate> result;

        //    try
        //    {
        //        result = func();
        //        dbContext.Set<TAggregate>().Add(result.Aggregate);
        //        await dbContext.SaveChangesAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        result = AggregateResult<TAggregate>.FromException(ex, "An exception occurred in HandleCore.Create while executing a command.");
        //    }

        //    return result;
        //}
    }
}
