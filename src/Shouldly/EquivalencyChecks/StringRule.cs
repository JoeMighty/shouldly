using System;
using System.Collections.Generic;

namespace Shouldly.EquivalencyChecks
{
/*    public abstract class CheckBase
    {
        public bool TypeMatches(object actual, object expected)
        {
            return true;
        }
    }*/
    
    public class StringRule : IEquivalencyCheck
    {
        public bool TypeMatches(object actual, object expected)
        {
            var type = actual;

            return false;
        }

        public EquivalencyCheckResult Compare(object actual, object expected)
        {
            var a = actual as string;
            var e = expected as string;
            var result = a != null && a.Equals(e, StringComparison.Ordinal);
            
            return new EquivalencyCheckResult
            {
                AreEquivalent = result
            };
        }
    }

    public interface IEquivalencyCheck
    {
        bool TypeMatches(object actual, object expected);
        
        EquivalencyCheckResult Compare(object actual, object expected);
    }

    public class EquivalencyCheckResult
    {
        public bool AreEquivalent { get; set; }
    }
}