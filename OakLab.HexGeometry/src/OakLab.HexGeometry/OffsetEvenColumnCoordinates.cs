namespace OakLab.HexGeometry;

public readonly record struct OffsetEvenColumnCoordinates(int X, int Y) : IHexGridCoordinates<OffsetEvenColumnCoordinates>
{
    public static implicit operator OffsetEvenColumnCoordinates(CubeCoordinates cube)
    {
        return new OffsetEvenColumnCoordinates(cube.X, cube.Y + (cube.X + (cube.X & 1)) / 2);
    }

    OffsetEvenColumnCoordinates IHexGridCoordinates<OffsetEvenColumnCoordinates>.ConvertFrom(CubeCoordinates cube) => cube;
    CubeCoordinates IHexGridCoordinates<OffsetEvenColumnCoordinates>.ConvertToCube() => this;
}