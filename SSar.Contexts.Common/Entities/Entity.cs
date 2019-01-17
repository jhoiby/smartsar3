using System;

namespace SSar.Contexts.Common.Entities
{
    // For an excellent article on DDD base classes see
    // https://github.com/dotnet/docs/blob/master/docs/standard/microservices-architecture/microservice-ddd-cqrs-patterns/seedwork-domain-model-base-classes-interfaces.md
    // Much of this class is based on the examples there.
    // (See that article for implementing equality operators, hashes

    public abstract class Entity : IEntity
    {
        private Guid _id;

        // TODO: Unit test the id property parameter

        public Entity(Guid id = default(Guid))
        {
            if (id == default(Guid))
            {
                _id = Guid.NewGuid();
            }
        }

        public Guid Id => _id;
    }
}
