using System;
using System.Collections.Generic;
using System.Text;

namespace Contexts.Common.AbstractClasses
{
    // For an excellent article on DDD base classes see
    // https://github.com/dotnet/docs/blob/master/docs/standard/microservices-architecture/microservice-ddd-cqrs-patterns/seedwork-domain-model-base-classes-interfaces.md
    // Much of this class is based on the examples there.
    // (See that article for implementing equality operators, hashes

    public abstract class Entity
    {
        private Guid _id;

        public Entity()
        {
            _id = Guid.NewGuid();
        }

        public Guid Id => _id;
    }
}
