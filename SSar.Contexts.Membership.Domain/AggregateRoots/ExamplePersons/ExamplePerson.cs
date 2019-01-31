using System;
using SSar.Contexts.Common.Domain.AggregateRoots;
using SSar.Contexts.Common.Domain.Entities;
using SSar.Contexts.Common.Domain.Notifications;

namespace SSar.Contexts.Membership.Domain.AggregateRoots.ExamplePersons
{
    public class ExamplePerson : AggregateRoot
    {
        public string _name;
        public string _emailAddress;

        private ExamplePerson()
        {
        }
        
        public string Name => _name;
        public string EmailAddress => _emailAddress;

        public static AggregateResult<ExamplePerson> Create(string name, string emailAddress)
        {
            var person = new ExamplePerson();
            var notifications = new NotificationList();

            person.SetName(name).AddNotificationsTo(notifications);
            person.SetEmailAddress(emailAddress).AddNotificationsTo(notifications);

            if (!notifications.HasNotifications)
            {
                person.AddEvent(new ExamplePersonCreated(person.Id, person.Name, person.EmailAddress));
                person.AddEvent(new TestEvent1());
                person.AddEvent(new TestEvent2());
            }
            
            return person.OrNotifications(notifications)
                .AsResult<ExamplePerson>();
        }

        // TODO: Make these private property accessors then create public change methods

        public AggregateResult<ExamplePerson> SetName(string name)
        {
            Action action = () => _name = name.Trim();

            var actionRequirements = RequirementList.Create()
                .AddRequirement( 
                    () => !string.IsNullOrWhiteSpace(name), "Name", "Name is required.")
                .AddRequirement( 
                    () => name != "Jar Jar Binks", "Name", "Jar Jar Binks is not wanted here!"); // TODO: Remove. Here for fun test

            return AggregateExecution
                .CheckRequirements(actionRequirements)
                .ExecuteAction(action)
                .ReturnAggregateResult(this);
        }

        public AggregateResult<ExamplePerson> SetEmailAddress(string emailAddress)
        {
            Action action = () => _emailAddress = emailAddress.Trim();

            var actionRequirements = RequirementList.Create()
                .AddRequirement(
                    () => emailAddress == null,
                    nameof(EmailAddress), "Email address must not be null.")
                .AddRequirement(
                    () => emailAddress.Trim().Length==0,
                    nameof(EmailAddress), "Email address is required.");

            return AggregateExecution
                .CheckRequirements(actionRequirements)
                .ExecuteAction(action)
                .ReturnAggregateResult(this);
        }

        // This is here to compare the above pattern to a more traditional,
        // less-abstracted pattern. I believe both are fairly clear.
        // One thing I like about the above pattern is it's readily
        // refactorable to a specification store.
        //
        // Note: Not tested, don't use.
        //
        public AggregateResult<ExamplePerson> SetEmailAddress2(string emailAddress)
        {
            AggregateResult<ExamplePerson> result;
            var notifications = new NotificationList();

            if (string.IsNullOrWhiteSpace(emailAddress))
            {
                notifications.AddNotification(nameof(EmailAddress), "Email address is required.");
            }

            if (notifications.HasNotifications)
            {
                result = AggregateResult<ExamplePerson>.Fail(notifications);
            }
            else
            {
                _emailAddress = emailAddress.Trim();
                result = AggregateResult<ExamplePerson>.Success(this);
            }

            return result;
        }


    }
}
