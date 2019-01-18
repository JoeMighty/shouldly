using System.Collections.Generic;

namespace Shouldly.EquivalencyChecks
{
    public class StringRule : IEquivalencyCheck
    {
        public EquivalencyCheckResult Compare(object actual, object expected, IDictionary<object, IList<object>> comparisonTracker)
        {
            
            return new EquivalencyCheckResult();
        }
    }

    public interface IEquivalencyCheck
    {
        EquivalencyCheckResult Compare(object actual, object expected, IDictionary<object, IList<object>> comparisonTracker);
    }

    public class EquivalencyCheckResult
    {
    }
}