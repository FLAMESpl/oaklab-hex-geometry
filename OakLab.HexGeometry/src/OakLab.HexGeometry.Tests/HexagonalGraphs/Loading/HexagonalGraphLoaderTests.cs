using System.Linq;
using FluentAssertions;
using Xunit;

namespace OakLab.HexGeometry.Tests.HexagonalGraphs.Loading;

public class HexagonalGraphLoaderTests
{
    [Fact]
    public void CanGetEverySecondElementWithoutOffset()
    {
        Enumerable.Range(0, 10).EverySecond(0).Should().BeEquivalentTo(new[] { 0, 2, 4, 6, 8 });
    }

    [Fact]
    public void CanGetEverySecondElementWithOffset()
    {
        Enumerable.Range(0, 10).EverySecond(1).Should().BeEquivalentTo(new[] { 1, 3, 5, 7, 9 });
    }

    [Fact]
    public void CanLoadSingleEdge()
    {
        HexagonalGraphLoader
            .Load("1 1")
            .Edges
            .Should()
            .BeEquivalentTo(new[]
            {
                new HexagonalGraphEdge(new OffsetOddRowCoordinates(0, 0), new OffsetOddRowCoordinates(1, 0), 1),
                new HexagonalGraphEdge(new OffsetOddRowCoordinates(1, 0), new OffsetOddRowCoordinates(0, 0), 1)
            });
    }

    [Fact]
    public void CanLoadTriangle()
    {
        HexagonalGraphLoader
            .Load(@"1 2
 3")
            .Edges
            .Should()
            .BeEquivalentTo(new[]
            {
                new HexagonalGraphEdge(new OffsetOddRowCoordinates(0, 0), new OffsetOddRowCoordinates(1, 0), 2),
                new HexagonalGraphEdge(new OffsetOddRowCoordinates(1, 0), new OffsetOddRowCoordinates(0, 0), 1),
                new HexagonalGraphEdge(new OffsetOddRowCoordinates(0, 0), new OffsetOddRowCoordinates(0, 1), 3),
                new HexagonalGraphEdge(new OffsetOddRowCoordinates(0, 1), new OffsetOddRowCoordinates(0, 0), 1),
                new HexagonalGraphEdge(new OffsetOddRowCoordinates(1, 0), new OffsetOddRowCoordinates(0, 1), 3),
                new HexagonalGraphEdge(new OffsetOddRowCoordinates(0, 1), new OffsetOddRowCoordinates(1, 0), 2)
            });
    }
}
