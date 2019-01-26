using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using SSar.Contexts.Membership.Domain.Entities.ExamplePersons;

namespace SSar.Contexts.Membership.Application.DomainEventHandlers
{
    public class LogExamplePersonCreated : INotificationHandler<ExamplePersonCreated>
    {
        private ILogger<LogExamplePersonCreated> _logger;

        public LogExamplePersonCreated(ILogger<LogExamplePersonCreated> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(ExamplePersonCreated notification, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
                _logger.LogDebug(
                    $"ExamplePerson created: Name = {notification.Name} , Email = {notification.EmailAddress}")
            );
        }
    }
}

