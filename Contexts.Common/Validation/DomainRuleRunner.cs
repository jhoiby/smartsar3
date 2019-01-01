using System;
using System.Collections.Generic;
using System.Text;
using SSar.Contexts.Common.Results;

namespace SSar.Contexts.Common.Validation
{
    public class DomainRuleRunner
    {
        struct RuleWithErrorMessage
        {
            public RuleWithErrorMessage(Func<bool> rule, string errorMessage)
            {
                Rule = rule;
                ErrorMessage = errorMessage;
            }

            public Func<bool> Rule;
            public string ErrorMessage;
        }

        private string _propertyName;
        private Func<bool> _ruleUnderConstruction;
        private string _errorForRuleUnderConstruction;
        private List<RuleWithErrorMessage> _ruleList;
        private ErrorDictionary _errorsFound;

        private DomainRuleRunner()
        {
            _ruleList = new List<RuleWithErrorMessage>();
        }

        public static DomainRuleRunner CreateRunner()
        {
            return new DomainRuleRunner();
        }

        public DomainRuleRunner ForPropertyNamed(string name)
        {
            _propertyName = name;

            return this;
        }

        public DomainRuleRunner AddRule(Func<bool> newRule)
        {
            FinalizeAndClearPreviousRuleUnderConstruction();
            _ruleUnderConstruction = newRule ?? throw new ArgumentNullException(nameof(newRule));

            return this;
        }

        public DomainRuleRunner WithErrorMessage(string errorMessage)
        {
            if (_ruleUnderConstruction == null)
            {
                throw new DomainRuleRunnerException("A rule must be provided before an error message can be added.");
            }

            _errorForRuleUnderConstruction = errorMessage ?? "";

            return this;
        }

        public ErrorDictionary RunValidation()
        {
            FinalizeAndClearPreviousRuleUnderConstruction();
            _errorsFound = new ErrorDictionary();

            if (_ruleList.Count > 0)
            {
                foreach (var ruleAndError in _ruleList)
                {
                    if (ruleAndError.Rule.Invoke() == false)
                    {
                        _errorsFound.AddOrAppend(_propertyName, ruleAndError.ErrorMessage);
                    }
                }
            }

            return _errorsFound;
        }

        private void FinalizeAndClearPreviousRuleUnderConstruction()
        {
            if (_ruleUnderConstruction != null)
            {
                _errorForRuleUnderConstruction = _errorForRuleUnderConstruction ?? "";

                _ruleList.Add(
                    new RuleWithErrorMessage(_ruleUnderConstruction, _errorForRuleUnderConstruction));
            }

            ClearRuleUnderConstruction();
        }

        private void ClearRuleUnderConstruction()
        {
            _ruleUnderConstruction = null;
            _errorForRuleUnderConstruction = null;
        }

    }
}
