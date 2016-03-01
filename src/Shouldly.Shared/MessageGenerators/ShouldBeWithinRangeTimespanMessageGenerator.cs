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
            var codePart = context.CodePart;
            var tolerance = context.Tolerance.ToStringAwesomely();
            var expectedValue = context.Expected.ToStringAwesomely();
            var actualValue = context.Actual.ToStringAwesomely();

            // (codePart == actualValue) = No source
            string actual = codePart == actualValue ? $"{actualValue}" : $"{codePart} ({actualValue})";

            /*var negated = context.ShouldMethod.Contains("Not") ? "not " : string.Empty;*/

            object actualType = context.Actual;

            string message = $@"{actual}
    should be within
{tolerance}
    of
{expectedValue}
    but had difference of
01:06:00";

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
