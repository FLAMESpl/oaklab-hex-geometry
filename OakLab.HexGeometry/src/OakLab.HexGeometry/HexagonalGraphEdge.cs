namespace OakLab.HexGeometry;

public class HexagonalGraphEdge
{
    public HexagonalGraphEdge(CubeCoordinates start, CubeCoordinates end, decimal weight)
    {
        Start = start;
        End = end;
        Weight = weight;
    }

    public CubeCoordinates Start { get; }
    public CubeCoordinates End { get; }
    public decimal Weight { get; set; }
}
