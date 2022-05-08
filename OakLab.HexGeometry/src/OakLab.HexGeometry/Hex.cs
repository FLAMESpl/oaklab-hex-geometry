using System.Numerics;

namespace OakLab.HexGeometry;

public readonly record struct Hex(Vector2 Center, HexModel Model)
{
    public float Height => Model.Height;
    public float Width => Model.Width;

    public IEnumerable<Vector2> GetVertices() => Model.GetVerticesFromCenter(Center);
    public Vector2 GetVertex(int i) => Model.GetVertexFromCenter(Center, i);
}