namespace OakLab.HexGeometry;

public readonly record struct OffsetEvenRowCoordinates(int X, int Y) : IHexGridCoordinates<OffsetEvenRowCoordinates>
{
    public static implicit operator OffsetEvenRowCoordinates(CubeCoordinates cube)
    {
        return new OffsetEvenRowCoordinates(cube.X + (cube.Y + (cube.Y & 1)) / 2, cube.Y);
    }

    OffsetEvenRowCoordinates IHexGridCoordinates<OffsetEvenRowCoordinates>.ConvertFrom(CubeCoordinates cube) => cube;
    CubeCoordinates IHexGridCoordinates<OffsetEvenRowCoordinates>.ConvertToCube() => this;
}