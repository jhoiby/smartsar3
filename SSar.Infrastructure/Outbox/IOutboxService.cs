using System;

namespace SSar.Infrastructure.Outbox
{
    public interface IOutboxService
    {
        /// <summary>
        /// Save serializable object to the outbox.
        /// </summary>
        /// <param name="objectId">Unique id to provide later operations on specific outbox item.</param>
        /// <param name="objectType">Type for filtering during object retrieval. Example: IIntegrationEvent.</param>
        /// <param name="object">The object being persisted in the outbox. Must be serializable.</param>
        /// <param name="validUntil">Date after which outbox item may no longer be used and may be automatically deleted.</param>
        void AddObject(Guid objectId, string objectType, object @object, DateTime validUntil);

        /// <summary>
        /// Delete outbox item matching the provided id.
        /// </summary>
        /// <param name="id">Id of OutboxPackage to delete.</param>
        /// <returns>True if item deleted. False if no item found.</returns>
        void Delete(Guid id);

        /// <summary>
        /// Delete outbox items matching the provided ids.
        /// </summary>
        /// <param name="idArray">Array containing ids of OutboxPackages to delete.</param>
        /// <returns>True if item deleted. False if no item found.</returns>
        void DeleteRange(Guid[] idArray);

        // TODO: Below classes need to be implemented as part of scheduled outbox cleaning

        ///// <summary>
        ///// Fetches next item from the outbox without deleting it.
        ///// </summary>
        ///// <returns>Dynamic type containing the deserialized original object retrieved from the outbox.</returns>
        //IOutboxPackage GetNext();

        ///// <summary>
        ///// Deletes all outbox items with a passed expiration date.
        ///// </summary>
        ///// <returns></returns>
        //int DeleteExpired();



    }
}
