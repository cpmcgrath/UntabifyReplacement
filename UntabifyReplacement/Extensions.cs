using System;
using System.Collections.Generic;

namespace CMcG.UntabifyReplacement
{
    public static class Extensions
    {
        public static IEnumerable<int> UpTo(this int start, int end)
        {
            for (int i = start; i <= end; i++)
                yield return i;
        }
    }
}