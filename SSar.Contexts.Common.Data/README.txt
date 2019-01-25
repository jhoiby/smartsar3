README - About SSar.Contexts.Common.Data

The Domin Driven Design tenets specify that each bounded context should have its own data store.
However, since we aren't implementing microservices and developer resources is a concern, a 
single data store has been implemented for the application.

This also saves using integration messages between the bounded contexts and the overhead that
implementing a guaranteed messaging system within each context would likely entail.

Becuase this solution has been carefully structured into bounded contexts with domain events
used to propogate changes between aggregates it is well-prepared for refactoring into
separate datastores and microservices if needed in the future.