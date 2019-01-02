using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SSar.Contexts.Common.Notifications;

namespace SSar.Contexts.Common.Validation
{
    public class DomainRequirementValidator
    {
        private struct RequirementWithFailureMessage
        {
            public RequirementWithFailureMessage(Func<bool> requirement, string failureMessage = "")
            {
                Requirement = requirement ?? throw new ArgumentNullException(nameof(requirement));
                FailureMessage = failureMessage;
            }

            public Func<bool> Requirement;
            public string FailureMessage;
        }

        private string _forFieldName = "";
        private Func<bool> _requirementBeingConstructed;
        private string _messageForRequirementBeingConstructed;
        private List<RequirementWithFailureMessage> _requirements;

        private DomainRequirementValidator()
        {
            _requirements = new List<RequirementWithFailureMessage>();
        }

        public static DomainRequirementValidator CreateValidator()
        {
            return new DomainRequirementValidator();
        }

        public DomainRequirementValidator ForFieldName(string fieldName = "")
        {
            _forFieldName = fieldName ?? "";

            return this;
        }

        public DomainRequirementValidator AddRequirement(Func<bool> newRequirement)
        {
            if (newRequirement == null)
            {
                throw new ArgumentNullException(nameof(newRequirement));
            }

            if (_requirementBeingConstructed != null)
            {
                SaveAndClearRulesUnderConstruction();
            }

            _requirementBeingConstructed = newRequirement ?? throw new ArgumentNullException(nameof(newRequirement));

            return this;
        }

        public DomainRequirementValidator WithMessage(string failureMessage)
        {
            if (_requirementBeingConstructed == null)
            {
                throw new DomainRequirementValidatorException("A rule must have been already added before attaching its message.");
            }

            _messageForRequirementBeingConstructed = failureMessage;
            return this;
        }

        public NotificationList Validate()
        {
            SaveAndClearRulesUnderConstruction();

            var notifications = new NotificationList();

            if (_requirements.Any())
            {
                foreach (var requirementWithMessage in _requirements)
                {
                    if (requirementWithMessage.Requirement.Invoke() == false)
                    {
                        // Requirement not met
                        notifications
                            .AddNotificationForField(requirementWithMessage.FailureMessage, _forFieldName);
                    }
                }
            }

            return notifications;
        }

        private void SaveAndClearRulesUnderConstruction()
        {
            if (_requirementBeingConstructed != null)
            {
                _messageForRequirementBeingConstructed = _messageForRequirementBeingConstructed ?? "";

                _requirements.Add(
                    new RequirementWithFailureMessage(
                        _requirementBeingConstructed, _messageForRequirementBeingConstructed));

                ClearRulesUnderConstructions();
            }
        }

        private void ClearRulesUnderConstructions()
        {
            _requirementBeingConstructed = null;
            _messageForRequirementBeingConstructed = null;
        }

    }
}
