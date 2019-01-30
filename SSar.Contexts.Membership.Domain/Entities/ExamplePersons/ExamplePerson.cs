using System;
using System.Collections.Generic;
using SSar.Contexts.Common.Domain.Entities;
using SSar.Contexts.Common.Domain.Notifications;

namespace SSar.Contexts.Membership.Domain.Entities.ExamplePersons
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

            var result = person.SetName(name);
            result.AddNotificationsTo(notifications);


            person.SetEmailAddress(emailAddress).AddNotificationsTo(notifications);

            person.AddEvent(new ExamplePersonCreated(person.Id, person.Name, person.EmailAddress));
            person.AddEvent(new TestEvent1());
            person.AddEvent(new TestEvent2());

            return person.OrNotifications(notifications)
                .AsResult<ExamplePerson>();
        }

        // TODO: Make these private property accessors then create public change methods

        public AggregateResult<ExamplePerson> SetName(string name)
        {
            name = name?.Trim();

            var requirements = RequirementsList.Create()
                .AddRequirement( 
                    () => !string.IsNullOrEmpty(name), "Name", "Name is required.")
                .AddRequirement( 
                    () => name != "James Hoiby", "Name", "James Hoiby is not wanted here!"); // TODO: Remove. Here for fun test

            // TODO: Event publish

            return ExecuteAction( 
                () => _name = name, 
                requirements);
        }

        public AggregateResult<ExamplePerson> SetEmailAddress(string emailAddress)
        {
            emailAddress = emailAddress?.Trim();

            var requirements = RequirementsList.Create()
                .AddRequirement(
                    () => !string.IsNullOrEmpty(emailAddress), "EmailAddress", "Email address is required.");

            // TODO: Event publish

            return ExecuteAction(
                () => _emailAddress = emailAddress, 
                requirements);
        }





        // TODO: BELOW THIS LINE IS STUFF TO REFACTOR AND MOVE TO OTHER CLASSES
        //
        // This was quick and dirty, consider better names and code cleanup
        //
        private NotificationList ExecuteRequirementSet(List<RequirementSet> requirements)
        {
            var notifications = new NotificationList();

            foreach (var requirementSet in requirements)
            {
                bool success = requirementSet.Test.Invoke();

                if (!success)
                {
                    notifications.AddNotification(requirementSet.ParamName, requirementSet.FailureMessage);
                }
            }

            return notifications;
        }

        private class RequirementSet
        {
            public RequirementSet(Func<bool> test, string paramName, string failureMessage)
            {
                Test = test;
                ParamName = paramName;
                FailureMessage = failureMessage;
            }

            public Func<bool> Test { get;  }
            public string ParamName { get; }
            public string FailureMessage { get; }
        }

        private AggregateResult<ExamplePerson> ExecuteAction(Action action, List<RequirementSet> requirements)
        {
            NotificationList notifications = ExecuteRequirementSet(requirements);

            if (!notifications.HasNotifications)
            {
                action.Invoke();
            }

            return this.OrNotifications(notifications)
                .AsResult<ExamplePerson>();
        }

        private class RequirementsList : List<RequirementSet>
        {
            private RequirementsList()
            {
            }

            public static RequirementsList Create()
            {
                return new RequirementsList();
            }

            public RequirementsList AddRequirement(Func<bool> test, string paramName, string failureMessage)
            {
                var requirementSet = new RequirementSet(test, paramName, failureMessage);

                this.Add(requirementSet);

                return this;
            }
        }
    }
}
