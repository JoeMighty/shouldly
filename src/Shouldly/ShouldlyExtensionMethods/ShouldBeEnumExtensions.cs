﻿using System;
using JetBrains.Annotations;

namespace Shouldly.ShouldlyExtensionMethods
{
    public static class ShouldHaveEnumExtensions
    {
        public static void ShouldHaveFlag(this Enum actual, Enum expectedFlag)
            => ShouldHaveFlag(actual, expectedFlag, () => null);

        public static void ShouldHaveFlag(this Enum actual, Enum expectedFlag, string customMessage)
            => ShouldHaveFlag(actual, expectedFlag, () => customMessage);

        public static void ShouldHaveFlag(this Enum actual, Enum expectedFlag, [InstantHandle] Func<string> customMessage)
        {
            CheckEnumHasFlagAttribute(actual);
            if (!actual.HasFlag(expectedFlag))
            {
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(expectedFlag, actual, customMessage).ToString());
            }
        }

        public static void ShouldNotHaveFlag(this Enum actual, Enum expectedFlag)
            => ShouldNotHaveFlag(actual, expectedFlag, () => null);

        public static void ShouldNotHaveFlag(this Enum actual, Enum expectedFlag, string customMessage)
            => ShouldNotHaveFlag(actual, expectedFlag, () => customMessage);

        public static void ShouldNotHaveFlag(this Enum actual, Enum expectedFlag,
            [InstantHandle] Func<string> customMessage)
        {
            CheckEnumHasFlagAttribute(actual);
            if (actual.HasFlag(expectedFlag))
            {
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(expectedFlag, actual, customMessage).ToString());
            }
        }

        static void CheckEnumHasFlagAttribute(Enum actual)
        {
            if (!actual.GetType().IsDefined(typeof(FlagsAttribute), false))
            {
                throw new ArgumentException("Enum doesn't have Flags attribute", nameof(actual));
            }
        }
    }
}
