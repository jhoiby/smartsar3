using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shouldly;
using SSar.Contexts.Common.Notifications;
using SSar.Contexts.Common.Validation;
using Xunit;

namespace SSar.Contexts.Common.UnitTests.Validation
{
    public class DomainRequirementValidatorTests
    {
        [Fact]
        public void CreateValidator_returns_proper_type()
        {
            var validator = DomainRequirementValidator.CreateValidator();

            validator.ShouldBeOfType<DomainRequirementValidator>();
        }

        // Skipped test of return types for most "builder" methods as 
        // error would be obvious during coding.

        [Fact]
        public void RunValidation_returns_NotificationList()
        {
            var validator = DomainRequirementValidator.CreateValidator();

            var notifications = validator.Validate();

            notifications.ShouldSatisfyAllConditions(
                () => notifications.ShouldBeOfType<NotificationList>(),
                () => notifications.ShouldNotBeNull()
                );
        }

        [Fact]
        public void RunValidation_returns_empty_notifications_if_all_requirements_met()
        {
            var testName = "Bob Tabor";

            var validator = DomainRequirementValidator.CreateValidator();

            validator
                .AddRequirement(() => testName.Length > 0)
                .AddRequirement(() => testName == "Bob Tabor")
                .AddRequirement(() => testName.Length == 9);

            var result = validator.Validate();

            result.Notifications.ShouldBeEmpty();
        }

        [Fact]
        public void RunValidation_returns_notifications_for_failing_requirements()
        {
            var testName = "Bob Tabor";

            var validator = DomainRequirementValidator.CreateValidator();

            validator
                .AddRequirement(() => testName.Length > 0) // Should succeed
                .WithMessage("Name must not be empty.")
                .AddRequirement(() => testName == "Bob Neighbor") // Should fail
                .WithMessage("You're not Bob Neighbor.")
                .AddRequirement(() => testName.Length == 3) // Should fail
                .WithMessage("We only accept people whose name is 9 characters long.");

            var result = validator.Validate();

            result.ShouldSatisfyAllConditions(
                () => result.Notifications.Count.ShouldBe(2),
                () => result.Notifications
                    .Where(n => n.Message == "You're not Bob Neighbor.")
                    .ShouldNotBeEmpty(),
                () => result.Notifications
                    .Where(n => n.Message == "We only accept people whose name is 9 characters long.")
                    .ShouldNotBeEmpty());
        }

        [Fact]
        public void ForFieldName_sets_empty_string_if_given_null()
        {
            var testName = "Bob Tabor";

            var validator = DomainRequirementValidator.CreateValidator();

            validator
                .ForFieldName(null)
                .AddRequirement(() => testName.Length > 99)
                .WithMessage("Name must at least 99 characters!");

            var result = validator.Validate();

            result.Notifications.First().ForField.ShouldBe("");
        }

        [Fact]
        public void ForFieldName_sets_empty_string_if_param_not_provided()
        {
            var testName = "Bob Tabor";

            var validator = DomainRequirementValidator.CreateValidator();

            validator
                .AddRequirement(() => testName.Length > 99)
                .WithMessage("Name must at least 99 characters!");

            var result = validator.Validate();

            result.Notifications.First().ForField.ShouldBe("");
        }

        [Fact]
        public void AddRequirement_throws_ArgNullException_if_null()
        {
            var ex = Should.Throw<ArgumentNullException>(
                () => DomainRequirementValidator.CreateValidator()
                    .AddRequirement(null));

            ex.ParamName.ShouldBe("newRequirement");
        }

        [Fact]
        public void Validate_returns_no_notifications_if_no_rules_provided()
        {
            var result = DomainRequirementValidator.CreateValidator()
                .Validate();

            result.Notifications.ShouldBeEmpty();
        }

        [Fact]
        public void Notifications_field_name_empty_if_ForField_not_called()
        {
            var testName = "Bob Tabor";

            var result = DomainRequirementValidator.CreateValidator()
                .AddRequirement(() => testName.Length > 99)
                .WithMessage("Name must at least 99 characters!")
                .Validate();

            result.Notifications.First().ForField.ShouldBe("");
        }

        [Fact]
        public void WithMessage_throws_exception_if_no_rules_added()
        {
            var ex = Should.Throw<DomainRequirementValidatorException>(
                () => DomainRequirementValidator.CreateValidator()
                    .WithMessage("Hello"));

            ex.Message.ShouldBe("A rule must have been already added before attaching its message.");
        }
    }
}
