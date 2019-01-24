using System;
using System.Linq;
using Newtonsoft.Json;
using SSar.Contexts.Common.Data.ServiceInterfaces;

namespace SSar.Contexts.Common.Data.Outbox
{
    public class OutboxService : IOutboxService
    {
        private AppDbContext _dbContext;

        public void AddProvider(AppDbContext provider)
        {
            if (_dbContext != null)
            {
                throw new InvalidOperationException("OutboxService provider is already set.");
            }

            _dbContext = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        public bool ProviderIsSet => _dbContext != null;

        public void Delete(Guid id)
        {
            _dbContext.Remove(_dbContext.OutboxPackages.Single(p => p.PackageId == id));
        }

        public void DeleteRange(Guid[] idArray)
        {
            _dbContext.RemoveRange(
                _dbContext.OutboxPackages.Where(p => idArray.Contains(p.PackageId)));
        }

        public void AddObject(Guid objectId, string objectType, object @object, DateTime validUntil)
        {
            var serializedObject = JsonConvert.SerializeObject(@object);

            // TODO: Try/catch with custom Outbox exception

            _dbContext.Add<OutboxPackage>(
                new OutboxPackage(objectId, objectType, serializedObject, validUntil));

            // Will be written to DB later when handler calls AppDbContext.SaveAsync
        }
    }
}
