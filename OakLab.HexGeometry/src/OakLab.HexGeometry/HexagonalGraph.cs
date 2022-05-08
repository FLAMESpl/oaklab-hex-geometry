namespace OakLab.HexGeometry;

public class HexagonalGraph
{
    private readonly ILookup<CubeCoordinates, HexagonalGraphEdge> edges;

    public IEnumerable<HexagonalGraphEdge> Edges => edges.SelectMany(x => x);

    public HexagonalGraph(IEnumerable<HexagonalGraphEdge> edges)
    {
        this.edges = edges.ToLookup(x => x.Start);
    }

    public HexagonalGridPath<TCoordinates> GetPath<TCoordinates>(
        TCoordinates start,
        TCoordinates end,
        params TCoordinates[] waypoints) where TCoordinates : struct, IHexGridCoordinates<TCoordinates>
    {
        var path = Enumerable.Empty<TCoordinates>();
        using var enumerator = waypoints.Prepend(start).Append(end).GetEnumerator();
        enumerator.MoveNext();
        var previous = enumerator.Current;

        while (enumerator.MoveNext())
        {
            var (partialPath, reachable) = GetPartialPath<TCoordinates>(
                previous.ConvertToCube(),
                enumerator.Current.ConvertToCube());

            if (!reachable)
                return HexagonalGridPath<TCoordinates>.Unreachable;

            path = path.Concat(partialPath);
        }

        return new HexagonalGridPath<TCoordinates>(path.ToList(), true);
    }

    private (IEnumerable<TCoordinates> Path, bool Rechable) GetPartialPath<TCoordinates>(
        CubeCoordinates start,
        CubeCoordinates end) where TCoordinates : struct, IHexGridCoordinates<TCoordinates>
    {
        var frontier = new PriorityQueue<CubeCoordinates, decimal>();
        frontier.Enqueue(start, 0);
        var cameFrom = new Dictionary<CubeCoordinates, CubeCoordinates>();
        var costSoFar = new Dictionary<CubeCoordinates, decimal>
        {
            [start] = 0
        };

        while (frontier.Count != 0)
        {
            var current = frontier.Dequeue();
            if (current == end)
                return (BuildPathFromTheEnd<TCoordinates>(end, cameFrom).Reverse(), true);

            foreach (var edge in edges[current])
            {
                var newCost = costSoFar[current] + edge.Weight;
                if (!costSoFar.TryGetValue(edge.End, out var nextCost) || newCost < nextCost)
                {
                    costSoFar[edge.End] = newCost;
                    frontier.Enqueue(edge.End, newCost + start.DistanceTo(end));
                    cameFrom[edge.End] = current;
                }
            }
        }

        return (Enumerable.Empty<TCoordinates>(), false);
    }

    private IEnumerable<TCoordinates> BuildPathFromTheEnd<TCoordinates>(
        CubeCoordinates end,
        IReadOnlyDictionary<CubeCoordinates, CubeCoordinates> cameFrom)
            where TCoordinates : struct, IHexGridCoordinates<TCoordinates>
    {
        var current = end;
        yield return default(TCoordinates).ConvertFrom(end);
        while (!cameFrom.TryGetValue(current, out var previous))
        {
            current = previous;
            yield return default(TCoordinates).ConvertFrom(previous);
        }
    }
}
