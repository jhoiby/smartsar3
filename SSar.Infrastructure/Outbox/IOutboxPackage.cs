using System;
using System.Collections.Generic;
using System.Text;

namespace SSar.Infrastructure.Outbox
{
    public interface IOutboxPackage
    {
        Guid PackageId { get; }
        string ObjectType { get; }
        string SerializedObject { get; }
        DateTime Created { get; }
        DateTime ValidUntil { get; }
    }
}
