using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SSar.Contexts.Common.Application.Commands;
using SSar.Contexts.Common.Domain.AggregateRoots;
using SSar.Contexts.Common.Domain.Entities;
using SSar.Contexts.Common.Domain.Notifications;

namespace SSar.Contexts.Common.Data.Extensions
{
    // TODO: Cover this with integration tests

    public static class AggregateResultSaveExtensions
    {
        public static async Task<AggregateResult<TAggregate>> AddIfSucceeded<TAggregate>(
            this AggregateResult<TAggregate> aggregateResult, AppDbContext dbContext) where TAggregate : AggregateRoot
        {
            AggregateResult<TAggregate> saveResult = aggregateResult;

            if (aggregateResult.Succeeded)
            {
                try
                {
                    await dbContext.AddAsync<TAggregate>(aggregateResult.NewAggregate);
                    var count = await dbContext.SaveChangesAsync();

                    if (count == 0)
                    {
                        saveResult =
                            AggregateResult<TAggregate>.Fail(new NotificationList("Database",
                                "Save to database failed: Return count was zero in SaveIfSucceededAndReturn."));
                    }
                }
                catch (Exception ex)
                {
                    saveResult = AggregateResult<TAggregate>.Fail(
                        new NotificationList("Database",
                            "An exception occurred when commiting to the database. See the application log for details."), 
                        ex);
                }
            }

            return saveResult;
        }
    }
}
