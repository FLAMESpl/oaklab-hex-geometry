namespace OakLab.HexGeometry;

public readonly record struct OffsetOddRowCoordinates(int X, int Y) : IHexGridCoordinates<OffsetOddRowCoordinates>
{
    public static implicit operator OffsetOddRowCoordinates(CubeCoordinates cube)
    {
        return new OffsetOddRowCoordinates(cube.X + (cube.Y - (cube.Y & 1)) / 2, cube.Y);
    }

    OffsetOddRowCoordinates IHexGridCoordinates<OffsetOddRowCoordinates>.ConvertFrom(CubeCoordinates cube) => cube;
    CubeCoordinates IHexGridCoordinates<OffsetOddRowCoordinates>.ConvertToCube() => this;
}