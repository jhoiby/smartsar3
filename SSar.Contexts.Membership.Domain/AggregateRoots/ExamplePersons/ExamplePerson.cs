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

            var requirements = RequirementList.Create()
                .AddRequirement( 
                    () => !string.IsNullOrWhiteSpace(name), "Name", "Name is required.")
                .AddRequirement( 
                    () => name != "Jar Jar Binks", "Name", "Jar Jar Binks is not wanted here!"); // TODO: Remove. Here for fun test

            return AggregateExecution
                .CheckRequirements(requirements)
                .ExecuteAction(action)
                .ReturnAggregateResult(this);
        }

        public AggregateResult<ExamplePerson> SetEmailAddress(string emailAddress)
        {
            Action action = () => _emailAddress = emailAddress.Trim();

            var requirements = RequirementList.Create()
                .AddException(
                    () => emailAddress == null,
                    nameof(EmailAddress), "A program error occurred (null email address)."
                    , new ArgumentNullException(
                        nameof(EmailAddress), "Email address must not be null."))
                .AddRequirement(
                    () => emailAddress.Trim().Length==0,
                    nameof(EmailAddress), "Email address is required.")
                .AddRequirement(
                    () => !RegexUtilities.IsValidEmail(emailAddress),
                    nameof(EmailAddress), "Please supply a valid email address.");

            return AggregateExecution
                .CheckRequirements(requirements)
                .ExecuteAction(action)
                .ReturnAggregateResult(this);
        }
        

        // This is here to compare the above pattern to a more traditional,
        // less-abstracted pattern.
        //
        // Note: Not tested, made private to discourage use.
        //
        private AggregateResult<ExamplePerson> SetEmailAddress2(string emailAddress)
        {
            AggregateResult<ExamplePerson> result;
            var notifications = new NotificationList();

            if (emailAddress == null)
            {
                throw new ArgumentNullException(nameof(emailAddress));
            }

            if (emailAddress.Trim().Length == 0)
            {
                notifications.AddNotification(nameof(EmailAddress), "Email address is required.");
            }

            if (!RegexUtilities.IsValidEmail(emailAddress))
            {
                notifications.AddNotification(nameof(EmailAddress), "Please supply a valid email address.");
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
