using System;
using System.Collections.Generic;

namespace Shouldly.EquivalencyChecks
{
    public class NullRule : IEquivalencyCheck
    {
        public bool TypeMatches(object actual, object expected)
        {
            throw new NotImplementedException();
        }

        public EquivalencyCheckResult Compare(object actual, object expected)
        {
            return new EquivalencyCheckResult
            {
                AreEquivalent = actual == null && expected == null
            };
        }
    }
}