using System;
using Shouldly.DifferenceHighlighting;

namespace Shouldly.MessageGenerators
{
    internal class ShouldBeWithinRangeTimespanMessageGenerator : ShouldlyMessageGenerator
    {
        public override bool CanProcess(IShouldlyAssertionContext context)
        {
            bool matches = (context.ShouldMethod.StartsWith("ShouldBe") || context.ShouldMethod.StartsWith("ShouldNotBe")) 
                && !context.ShouldMethod.Contains("Contain") && context.Tolerance != null;
            return matches && context.Actual is TimeSpan;
        }

        public override string GenerateErrorMessage(IShouldlyAssertionContext context)
        {
            var tolerance = context.Tolerance.ToStringAwesomely();

            var expectedValue = context.Expected.ToStringAwesomely();
            var actualValue = context.Actual.ToStringAwesomely();

            TimeSpan expectedTimeSpan = TimeSpan.Parse(expectedValue);
            TimeSpan actualTimeSpan = TimeSpan.Parse(actualValue);

            string final = expectedTimeSpan.Subtract(actualTimeSpan).ToString();
            // (codePart == actualValue) = No source
            string codePart = context.CodePart;
            string actual = codePart == actualValue ? $"{actualValue}" : $"{codePart} ({actualValue})";
            

            string message = $@"{actual}
    should be within
{tolerance}
    of
{expectedValue}
    but had difference of
{final}";

            if (DifferenceHighlighter.CanHighlightDifferences(context))
            {
                message += $@"
    difference
{DifferenceHighlighter.HighlightDifferences(context)}";
            }

            return message;
        }
    }
}
