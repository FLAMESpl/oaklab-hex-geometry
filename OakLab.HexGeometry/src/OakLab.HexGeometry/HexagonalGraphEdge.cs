namespace OakLab.HexGeometry;

public record HexagonalGraphEdge(CubeCoordinates Start, CubeCoordinates End, decimal Weight)
{
    public decimal Weight { get; set; } = Weight;
}
