namespace OakLab.HexGeometry;

public class HexagonalGridPath<TCoordinates>
{
    internal static readonly HexagonalGridPath<TCoordinates> Unreachable = new(Array.Empty<TCoordinates>(), false);

    public IReadOnlyList<TCoordinates> Coordinates { get; }
    public bool Reachable { get; }

    public HexagonalGridPath(IReadOnlyList<TCoordinates> coordinates, bool reachable)
    {
        Coordinates = coordinates;
        Reachable = reachable;
    }
}
