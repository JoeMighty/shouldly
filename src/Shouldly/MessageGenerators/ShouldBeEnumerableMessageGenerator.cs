﻿using System.Linq.Expressions;
using Shouldly.DifferenceHighlighting;

namespace Shouldly.MessageGenerators
{
    internal class ShouldBeEnumerableMessageGenerator : ShouldlyMessageGenerator
    {
        public override bool CanProcess(IShouldlyAssertionContext context)
        {
            return context.ShouldMethod.Equals("ShouldBe") && !(context.Expected is Expression) && 
                   context.UnderlyingShouldMethod.ReflectedType == typeof (ShouldBeEnumerableTestExtensions);
        }

        public override string GenerateErrorMessage(IShouldlyAssertionContext context)
        {
            var codePart = context.CodePart;
            var caseSensitivity = context.CaseSensitivity == Case.Insensitive ? " (case insensitive comparison)" : string.Empty;
            string format = @"
    {0}
        {1}
    {2}{3}
        but was
    {4}";

            if (DifferenceHighlighter.CanHighlightDifferences(context))
            {
                format += string.Format(@"
        difference
    {0}",
                DifferenceHighlighter.HighlightDifferences(context));
            }

            return string.Format(format,
                    codePart,
                    context.ShouldMethod.PascalToSpaced(),
                    context.Expected.ToStringAwesomely(),
                    caseSensitivity,
                    context.Actual.ToStringAwesomely());
        }
    }


}