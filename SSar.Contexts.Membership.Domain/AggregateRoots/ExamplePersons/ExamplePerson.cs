using System;
using System.Text.RegularExpressions;
using SSar.Contexts.Common.Domain.AggregateRoots;
using SSar.Contexts.Common.Domain.Entities;
using SSar.Contexts.Common.Domain.Notifications;
using SSar.Contexts.Common.Helpers;

namespace SSar.Contexts.Membership.Domain.AggregateRoots.ExamplePersons
{
    public class ExamplePerson : AggregateRoot
    {
        private string _name;
        private string _emailAddress;

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

        private AggregateResult<ExamplePerson> SetName(string name)
        {
            Action action = () => _name = name.Trim();

            var requirements = RequirementList.Create()
                .AddNotificationRequirement( 
                    () => !string.IsNullOrWhiteSpace(name), "Name", "A name is required.")
                .AddNotificationRequirement( 
                    () => name != "Jar Jar Binks", "Name", "Jar Jar Binks is not wanted here!"); // TODO: Remove. Here for fun test

            return AggregateExecution
                .CheckRequirements(requirements)
                .ExecuteAction(action)
                .ReturnAggregateResult(this);
        }

        private AggregateResult<ExamplePerson> SetEmailAddress(string emailAddress)
        {
            Action action = () => _emailAddress = emailAddress.Trim();

            var requirements = RequirementList.Create()
                .AddExceptionRequirement(
                    () => emailAddress != null,
                    nameof(EmailAddress), "A program error occurred while setting the email address." // TODO: Remove these user params
                    , new ArgumentNullException(
                        nameof(EmailAddress), "Email address must not be null."))
                .AddNotificationRequirement(
                    () => RegexUtilities.IsValidEmail(emailAddress),
                    nameof(EmailAddress), "A valid email address is required.");

            return AggregateExecution
                .CheckRequirements(requirements)
                .ExecuteAction(action)
                .ReturnAggregateResult(this);
        }
        

        // This is here to compare the above pattern to a more traditional,
        // less-abstracted pattern. Food for thought...
        //
        // Note: Not tested, don't use.
        //
        private AggregateResult<ExamplePerson> SetEmailAddress2(string emailAddress)
        {
            AggregateResult<ExamplePerson> result;
            var notifications = new NotificationList();

            if (emailAddress == null)
            {
                throw new ArgumentNullException(nameof(emailAddress));
            }

            if (!RegexUtilities.IsValidEmail(emailAddress))
            {
                notifications.AddNotification(nameof(EmailAddress), "A valid email address is required.");
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
