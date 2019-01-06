using System;
using System.Collections.Generic;
using System.Text;
using Shouldly;
using SSar.Contexts.Common.Enums;
using SSar.Contexts.Membership.Domain.Entities;
using Xunit;

namespace SSar.Contexts.Membership.UnitTests.Domain
{
    public class QuestionSpecTests
    {
        [Fact]
        public void Constructor_sets_properties()
        {
            var spec = new QuestionSpec("The question", FieldType.Text, true);

            spec.ShouldSatisfyAllConditions(
                () => spec.Question.ShouldBe("The question"),
                () => spec.ResponseFieldType.ShouldBe(FieldType.Text),
                () => spec.Required.ShouldBe(true));
        }

        [Fact]
        public void Throws_ArguementNullException_if_question_is_null()
        {
            var ex = Should.Throw<ArgumentNullException>(() => new QuestionSpec(null, FieldType.Text, false));
            ex.ParamName.ShouldBe("question");
        }

        [Fact]
        public void Throws_ArgumentOutOfRangeException_if_question_is_empty_string()
        {
            var ex = Should.Throw<ArgumentOutOfRangeException>(() => new QuestionSpec("", FieldType.Text, false));
            ex.ParamName.ShouldBe("question");
        }
    }
}
