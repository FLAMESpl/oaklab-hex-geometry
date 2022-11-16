using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace OakLab.HexGeometry.Tests.HexagonalGraphs.Loading;

public static class EnumerableExtensions
{
    [Pure]
    public static IEnumerable<T> EverySecond<T>(this IEnumerable<T> source, int offset)
    {
        using var enumerator = source.Skip(offset).GetEnumerator();

        while (enumerator.MoveNext())
        {
            yield return enumerator.Current;
            if (!enumerator.MoveNext())
                yield break;
        }
    }
}
