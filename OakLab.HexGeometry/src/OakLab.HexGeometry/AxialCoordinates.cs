namespace OakLab.HexGeometry;

public readonly record struct AxialCoordinates(int X, int Y) : IHexGridCoordinates<AxialCoordinates>
{
    public static implicit operator AxialCoordinates(CubeCoordinates cube)
    {
        return new AxialCoordinates(cube.X, cube.Y);
    }

    AxialCoordinates IHexGridCoordinates<AxialCoordinates>.ConvertFrom(CubeCoordinates cube) => cube;
    CubeCoordinates IHexGridCoordinates<AxialCoordinates>.ConvertToCube() => this;
}
