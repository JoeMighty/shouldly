using System;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldNotThrow
{
    public class FuncDelegateScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            Should.NotThrow(new Func<int>(() => { throw new InvalidOperationException(); }), "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return "Should should not throw but threw System.InvalidOperationException with message \"Operation is not valid due to the current state of the object.\"" +
                        "Additional Info:" +
                        "Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            Should.NotThrow(() => 1).ShouldBe(1);
        }
    }
}