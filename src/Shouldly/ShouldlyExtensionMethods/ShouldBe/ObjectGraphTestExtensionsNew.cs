using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Shouldly.EquivalencyChecks;

namespace Shouldly
{
    [ShouldlyMethods]
    public static class ObjectGraphTestExtensionsNew
    {
        public static void ShouldBeEquivalentToNew(this object actual, object expected, string customMessage)
        {
            ShouldBeEquivalentToNew(actual, expected, () => customMessage);
        }
        
        public static void ShouldBeEquivalentToNew(this object actual, object expected)
        {
            ShouldBeEquivalentToNew(actual, expected, () => null);
        }

        public static void ShouldBeEquivalentToNew(this object actual, object expected,
            [InstantHandle] Func<string> customMessage)
        {
            var path = new List<string>();
            var previousComparisons = new Dictionary<object, IList<object>>();
            
            if (new NullRule().Compare(actual, expected).AreEquivalent)
                return;
                
            var results = CompareObjectsRevisited(actual, expected, path, previousComparisons);

            foreach (var result in results)
            {
                if (!result.AreEquivalent)
                    AssertResults(actual, expected, new List<string>(), customMessage);
            }
        }
        
        private static void AssertResults(object actual, object expected, IList<string> path,
            [InstantHandle] Func<string> customMessage, [CallerMemberName] string shouldlyMethod = null)
        {
            ThrowException(actual, expected, path, customMessage, shouldlyMethod);
        }

        private static List<EquivalencyCheckResult> CompareObjectsRevisited(object actual, object expected,
            IList<string> path, IDictionary<object, IList<object>> previousComparisons,
            [CallerMemberName] string shouldlyMethod = null)
        {
/*            if (BothValuesAreNull(actual, expected, path, customMessage, shouldlyMethod))
                return;*/

            /*Type type = GetTypeToCompare(actual, expected, path, customMessage, shouldlyMethod);*/

            var checks = new List<IEquivalencyCheck>
            {
/*                new NullRule(),*/
                new StringRule()
            };

            var resultChecks = new List<EquivalencyCheckResult>();
            foreach (var check in checks)
            {
                var result = check.Compare(actual, expected);
                resultChecks.Add(result);
            }

            return resultChecks;
        }

        private static bool BothValuesAreNull(object actual, object expected, IEnumerable<string> path,
            [InstantHandle] Func<string> customMessage, [CallerMemberName] string shouldlyMethod = null)
        {
            if (expected == null)
            {
                if (actual == null)
                    return true;

                ThrowException(actual, expected, path, customMessage, shouldlyMethod);
            }
            else if (actual == null)
            {
                ThrowException(actual, expected, path, customMessage, shouldlyMethod);
            }

            return false;
        }

        private static Type GetTypeToCompare(object actual, object expected, IList<string> path,
            [InstantHandle] Func<string> customMessage, [CallerMemberName] string shouldlyMethod = null)
        {
            var expectedType = expected.GetType();
            var actualType = actual.GetType();

            if (actualType != expectedType)
                ThrowException(actualType, expectedType, path, customMessage, shouldlyMethod);

            var typeName = $" [{actualType.FullName}]";
            if (path.Count == 0)
                path.Add(typeName);
            else
                path[path.Count - 1] += typeName;

            return actualType;
        }

        private static void ThrowException(object actual, object expected, IEnumerable<string> path,
            [InstantHandle] Func<string> customMessage, [CallerMemberName] string shouldlyMethod = null)
        {
            throw new ShouldAssertException(
                new ExpectedEquvalenceShouldlyMessage(expected, actual, path, customMessage, shouldlyMethod)
                    .ToString());
        }
    }
}