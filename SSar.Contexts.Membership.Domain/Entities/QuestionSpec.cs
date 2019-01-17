using System;
using System.Collections.Generic;
using System.Text;
using SSar.Contexts.Common.Helpers;
using SSar.Contexts.Common.Helpers.Enums;

namespace SSar.Contexts.Membership.Domain.Entities
{
    public class QuestionSpec
    {
        public QuestionSpec(string question, FieldType responseFieldType, bool required)
        {
            Question = question.Require(nameof(question)); //.Trim();
            ResponseFieldType = responseFieldType;
            Required = required;
        }

        public string Question { get; }
        public bool Required { get; }
        public FieldType ResponseFieldType { get; }

    }
}
