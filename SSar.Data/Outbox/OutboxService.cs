using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace SSar.Data.Outbox
{
    public class OutboxService : IOutboxService
    {
        private readonly AppDbContext _dbContext;

        public OutboxService(AppDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public void Delete(Guid id)
        {
            _dbContext.Remove(_dbContext.OutboxPackages.Single(p => p.PackageId == id));
        }

        public void Delete(Guid[] idArray)
        {
            _dbContext.RemoveRange(
                _dbContext.OutboxPackages.Where(p => idArray.Contains(p.PackageId)));
        }

        public int FlushExpired()
        {
            throw new NotImplementedException();
        }

        public dynamic GetNext()
        {
            throw new NotImplementedException();
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
