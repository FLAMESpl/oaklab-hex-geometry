namespace OakLab.HexGeometry;

public readonly record struct OffsetOddColumnCoordinates(int X, int Y) : IHexGridCoordinates<OffsetOddColumnCoordinates>
{
    public static implicit operator OffsetOddColumnCoordinates(CubeCoordinates cube)
    {
        return new OffsetOddColumnCoordinates(cube.X, cube.Y + (cube.X - (cube.X & 1)) / 2);
    }

    OffsetOddColumnCoordinates IHexGridCoordinates<OffsetOddColumnCoordinates>.ConvertFrom(CubeCoordinates cube) => cube;
    CubeCoordinates IHexGridCoordinates<OffsetOddColumnCoordinates>.ConvertToCube() => this;
}