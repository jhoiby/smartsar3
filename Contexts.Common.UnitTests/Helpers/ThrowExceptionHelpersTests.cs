using System;
using System.Collections.Generic;
using System.Text;
using Shouldly;
using SSar.Contexts.Common.Helpers;
using Xunit;

namespace SSar.Contexts.Common.UnitTests.Helpers
{
    public class ThrowExceptionHelpersTests
    {
        [Fact]
        public void If_object_is_null_throw_ArgNullException()
        {
            string name = null;

            Should.Throw<ArgumentNullException>(
                () => name.ThrowIfArgumentNull(nameof(name))
            );
        }

        [Fact]
        public void If_paramName_null_then_ex_paramName_should_be_null()
        {
            string name = null;

            var ex = Should.Throw<ArgumentNullException>( () => name.ThrowIfArgumentNull() );
            ex.ParamName.ShouldBeNull();
        }

        [Fact]
        public void If_paraName_set_it_should_be_passed_to_exception()
        {
            string name = null;

            var ex = Should.Throw<ArgumentNullException>(() => name.ThrowIfArgumentNull(nameof(name)));
            ex.ParamName.ShouldBe("name");
        }
    }
}
