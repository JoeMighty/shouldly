using System;
using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldNotBe.WithTolerance
{
    public class TimeSpanScenario
    {

        [Fact]
        public void TimeSpanScenarioShouldFail()
        {
            var timeSpan = TimeSpan.FromHours(1);
            var res = timeSpan.ToString(); //1:00:00
            var tolerance = TimeSpan.FromHours(1.5d).ToString(); // 1:30:00
            var expected = timeSpan.Add(TimeSpan.FromHours(1.1d)).ToString(); // 2:06:00

            Verify.ShouldFail(() =>
timeSpan.ShouldNotBe(timeSpan.Add(TimeSpan.FromHours(1.1d)), TimeSpan.FromHours(1.5d), "Some additional context"),

errorWithSource:
@"timeSpan (01:00:00)
    should not be within
01:30:00
    of
02:06:00
    but was
01:00:00

Additional Info:
    Some additional context",

errorWithoutSource:
@"01:00:00
    should not be within
01:30:00
    of
02:06:00
    but was not

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            var timeSpan = TimeSpan.FromHours(1);
            timeSpan.ShouldNotBe(timeSpan.Add(TimeSpan.FromHours(1.1d)), TimeSpan.FromHours(1));
        }
    }
}