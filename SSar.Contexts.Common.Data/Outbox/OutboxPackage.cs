using System;
using SSar.Contexts.Common.Data.ServiceInterfaces;

namespace SSar.Contexts.Common.Data.Outbox
{
    public class OutboxPackage
    {
        public OutboxPackage(
            Guid packageId,
            string objectType,
            string serializedObject,
            DateTime validUntil)
        {
            PackageId = packageId;
            ObjectType = objectType ?? throw new ArgumentNullException(nameof(objectType));
            SerializedObject = serializedObject; // null allowed
            Created = DateTime.Now;
            ValidUntil = validUntil;
        }

        public Guid PackageId { get; private set; }
        public string ObjectType { get; private set; }
        public string SerializedObject { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime ValidUntil { get; private set; }
    }
}
