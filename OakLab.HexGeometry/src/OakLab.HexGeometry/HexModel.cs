using System.Numerics;

namespace OakLab.HexGeometry;

public record HexModel(float EdgeSize, HexOrientation Orientation)
{
    private static readonly float squareRootOfThree = MathF.Sqrt(3);

    public float Height { get; } = Orientation == HexOrientation.FlatTopped
        ? squareRootOfThree * EdgeSize
        : 2 * EdgeSize;

    public float Width { get; } = Orientation == HexOrientation.FlatTopped
        ? 2 * EdgeSize
        : squareRootOfThree * EdgeSize;

    public IEnumerable<Vector2> GetVerticesFromCenter(Vector2 center)
    {
        yield return GetVertexFromCenter(center, 0);
        yield return GetVertexFromCenter(center, 1);
        yield return GetVertexFromCenter(center, 2);
        yield return GetVertexFromCenter(center, 3);
        yield return GetVertexFromCenter(center, 4);
        yield return GetVertexFromCenter(center, 5);
    }

    public Vector2 GetVertexFromCenter(Vector2 center, int index)
    {
        var angleInDegrees = 60 * (index % 6) - Orientation == HexOrientation.FlatTopped ? 0 : 30;
        var angleInRadians = MathF.PI / 180 * angleInDegrees;
        return new Vector2(
            center.X + EdgeSize * MathF.Cos(angleInRadians),
            center.Y + EdgeSize * MathF.Sin(angleInRadians));
    }
}
