using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SSar.Contexts.Common.AbstractClasses;
using SSar.Contexts.Common.Helpers;
using SSar.Contexts.Common.Notifications;

namespace SSar.Contexts.Membership.Domain.Entities
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

            var constructionNotifications = new NotificationList();

            person.SetName(name).AddNotificationsTo(constructionNotifications);
            person.SetEmailAddress(emailAddress).AddNotificationsTo(constructionNotifications);

            return AggregateResult<ExamplePerson>.FromAggregate(person);
        }

        public AggregateResult<ExamplePerson> SetName(string name)
        {
            name = name?.Trim();

            var requirements = RequirementsList.Create()
                .AddRequirement( () => !String.IsNullOrEmpty(name), "Name", "Name is required.")
                .AddRequirement( () => name != "James Hoiby", "Name", "(Test) James Hoiby is not wanted here!"); // Temp test

            return ExecuteAction( () => _name = name, requirements);
        }

        public AggregateResult<ExamplePerson> SetEmailAddress(string emailAddress)
        {
            // Incomplete

            _emailAddress = emailAddress ?? throw new ArgumentNullException(nameof(emailAddress));

            return AggregateResult<ExamplePerson>.FromAggregate(this);
        }


        // TODO: BELOW THIS LINE IS STUFF TO REFACTOR TO OTHER CLASSES

        // Also consider better names and code cleanup

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

            return AggregateResult<ExamplePerson>.
                FromAggregateOrNotifications(this, notifications);
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
