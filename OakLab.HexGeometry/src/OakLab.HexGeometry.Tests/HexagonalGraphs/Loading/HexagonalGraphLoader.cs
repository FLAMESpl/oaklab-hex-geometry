using System;
using System.Collections.Generic;
using System.Linq;

namespace OakLab.HexGeometry.Tests.HexagonalGraphs.Loading;

public static class HexagonalGraphLoader
{
    private record struct Node(OffsetEvenRowCoordinates Coordinates, decimal Weight);

    public static HexagonalGraph Load(string map)
    {
        var lines = map.Split(Environment.NewLine);

        if (lines.Length == 0)
            return HexagonalGraph.CreateEmpty();

        var nodes = lines
            .Select(ParseRow)
            .SelectMany((x, row) => x.Select(y => new Node(new OffsetEvenRowCoordinates(y.Column, row), y.Weight)))
            .ToDictionary(x => x.Coordinates, x => x.Weight);

        var edges = nodes
            .SelectMany(x => x.Key
                .GetNeighbours()
                .Select(y => (
                    start: x.Key,
                    end: y,
                    weight: nodes.TryGetValue(x.Key, out var weight) ? (decimal?) weight : null))
                .Where(y => y.weight is not null)
                .Select(y => new HexagonalGraphEdge(y.start, y.end, y.weight!.Value)))
            .ToHashSet();

        return new HexagonalGraph(edges);
    }

    private static IEnumerable<(decimal Weight, int Column)> ParseRow(string row, int rowIndex)
    {
        return EverySecondCharacter(row, rowIndex % 2)
            .Select((ch, index) => (weigth: TryGetWeightFromCharacter(ch), index))
            .Where(x => x.weigth.HasValue)
            .Select(x => (x.weigth!.Value, x.index));
    }

    private static IEnumerable<char> EverySecondCharacter(string source, int offset)
    {
        using var enumerator = source.Skip(offset).GetEnumerator();

        while (enumerator.MoveNext())
        {
            yield return enumerator.Current;
            if (!enumerator.MoveNext())
                yield break;
        }
    }

    private static decimal? TryGetWeightFromCharacter(char @char) => char.IsDigit(@char)
        ? (decimal)char.GetNumericValue(@char)
        : null;
}
