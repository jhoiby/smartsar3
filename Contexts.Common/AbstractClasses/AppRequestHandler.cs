using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SSar.Contexts.Common.Notifications;

namespace SSar.Contexts.Common.AbstractClasses
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

        protected async Task<OperationResult> Execute<TDbContext, TAggregate>(
            TDbContext dbContext, Guid id, Func<TAggregate, OperationResult> func)
            where TAggregate : AggregateRoot
            where TDbContext : DbContext
        {
            OperationResult result;

            try
            {
                var aggregate = await dbContext.Set<TAggregate>().FindAsync(id);
                result = func(aggregate);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                result = OperationResult.FromException(ex, "An error occurred while executing a command.");
            }

            return result;
        }

        protected async Task<OperationResult> Create<TDbContext, TAggregate>(
            TDbContext dbContext, Guid id, Func<OperationResult> func)
            where TAggregate : AggregateRoot
            where TDbContext : DbContext
        {
            OperationResult result;

            try
            {
                result = func();
                dbContext.Set<TAggregate>().Add(result.Data);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                result = OperationResult.FromException(ex, "An error occurred while executing a command.");
            }

            return result;
        }
    }
}
