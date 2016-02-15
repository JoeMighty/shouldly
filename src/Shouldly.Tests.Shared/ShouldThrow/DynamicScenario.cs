using System;
using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldThrow
{
    public class DynamicScenario
    {
        public void Thrower()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void DynamicDelegateScenarioShouldFail()
        {
            Action action = () => { };
            Verify.ShouldFail(() =>
                action.ShouldThrow<NotImplementedException>("Some additional context"),
                errorWithSource:
                    @"`action()`
    should throw
System.NotImplementedException
    but did not

Additional Info:
    Some additional context",
                errorWithoutSource:
                    @"delegate
    should throw
System.NotImplementedException
    but did not

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            Should.Throw<NotImplementedException>(() => ((dynamic)this).Thrower());
        }
    }
}