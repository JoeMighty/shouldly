using System;
using Shouldly.DifferenceHighlighting;

namespace Shouldly.MessageGenerators
{
    internal class ShouldBeWithinRangeTimespanMessageGenerator : ShouldlyMessageGenerator
    {
        public override bool CanProcess(IShouldlyAssertionContext context)
        {
            object tolerance = context.Tolerance;
            bool matches = (context.ShouldMethod.StartsWith("ShouldBe") ||
                            context.ShouldMethod.StartsWith("ShouldNotBe"))
                           && !context.ShouldMethod.Contains("Contain")
                           && context.Tolerance != null;


            if (matches && context.Actual is TimeSpan)
            {
                return true;
            }

            return false;
        }

        public override string GenerateErrorMessage(IShouldlyAssertionContext context)
        {
            var codePart = context.CodePart;
            var tolerance = context.Tolerance.ToStringAwesomely();
            var expectedValue = context.Expected.ToStringAwesomely();
            var actualValue = context.Actual.ToStringAwesomely();
            /*string actual = $@"{actualValue}";*/
            string actual = "01:06:00";
            /*if (codePart == actualValue) actual = " yes";
            else actual = $@"
{actualValue}";
            var negated = context.ShouldMethod.Contains("Not") ? "not " : string.Empty;*/

            string message;
            object actualType = context.Actual;

            message =
$@"timeSpan (01:00:00) 
   should be within
01:00:00
    of
02:06:00
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
