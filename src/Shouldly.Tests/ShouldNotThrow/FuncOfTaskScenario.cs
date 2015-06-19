﻿#if net40
using System;
using System.Threading;
using System.Threading.Tasks;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldNotThrow
{
    public class FuncOfTaskScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            /*Should.NotThrow(() =>
            {
                var task = Task.Factory.StartNew(() => { throw new RankException(); },
                    CancellationToken.None, TaskCreationOptions.None,
                    TaskScheduler.Default);
                return task;
             * 
            }, "Some additional context")*/;

            var task = Task.Factory.StartNew(() => { throw new RankException(); },
                CancellationToken.None, TaskCreationOptions.None,
                TaskScheduler.Default);

            task.ShouldNotThrow("Some additional context");

        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return @"task should not throw System.RankException but does
Additional Info:
Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            var task = Task.Factory.StartNew(() => { },
                CancellationToken.None, TaskCreationOptions.None,
                TaskScheduler.Default);

            task.ShouldNotThrow();
        }
    }
}
#endif