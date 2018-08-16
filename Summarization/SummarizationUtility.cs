using System;
using System.Collections.Generic;
using System.Text;

using Messages;

namespace Summarization
{
    public static class SummarizationUtility
    {
        public static IntervalType? GetNextInterval(IntervalType interval)
        {
            if (interval == IntervalType.Hourly)
                return IntervalType.Daily;
            if (interval == IntervalType.Daily)
                return IntervalType.Monthly;

            return null;
        }
    }
}
