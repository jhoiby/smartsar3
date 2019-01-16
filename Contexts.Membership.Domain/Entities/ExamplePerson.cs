using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SSar.Contexts.Common.Entities;
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
            var notifications = new NotificationList();

            person.SetName(name).AddNotificationsTo(notifications);
            person.SetEmailAddress(emailAddress).AddNotificationsTo(notifications);

            return AggregateResult<ExamplePerson>
                .FromAggregateOrNotifications(person, notifications);
        }

        public AggregateResult<ExamplePerson> SetName(string name)
        {
            name = name?.Trim();

            var requirements = RequirementsList.Create()
                .AddRequirement( 
                    () => !string.IsNullOrEmpty(name), "Name", "Name is required.")
                .AddRequirement( 
                    () => name != "James Hoiby", "Name", "James Hoiby is not wanted here!"); // Temp fun test

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
