namespace OakLab.HexGeometry;

public readonly record struct CubeCoordinates(int X, int Y, int Z) : IHexGridCoordinates<CubeCoordinates>
{
    public CubeCoordinates Minus(CubeCoordinates other)
    {
        return new CubeCoordinates(X - other.X, Y - other.Y, Z - other.Z);
    }

    public CubeFloatingCoordinates Lerp(CubeCoordinates other, float t)
    {
        return new CubeFloatingCoordinates(
            Lerp(X, other.X, t),
            Lerp(Y, other.Y, t),
            Lerp(Z, other.Z, t));
    }

    public CubeCoordinates ReflectX() => new(X, Z, Y);
    public CubeCoordinates ReflectY() => new(Z, Y, X);
    public CubeCoordinates ReflectZ() => new(Y, X, Z);
    public CubeCoordinates RotateClockwise() => new(-Y, -Z, -X);
    public CubeCoordinates RotateCounterclockwise() => new(-Z, -X, -Y);

    public CubeCoordinates Plus(CubeCoordinates other)
    {
        return new CubeCoordinates(X + other.X, Y + other.Y, Z + other.Z);
    }

    public static CubeCoordinates operator -(CubeCoordinates first, CubeCoordinates second) => first.Minus(second);
    public static CubeCoordinates operator +(CubeCoordinates first, CubeCoordinates second) => first.Plus(second);

    public static implicit operator CubeCoordinates(AxialCoordinates axial)
    {
        return new CubeCoordinates(axial.X, axial.Y, -axial.X - axial.Y);
    }

    public static implicit operator CubeCoordinates(OffsetEvenColumnCoordinates offset)
    {
        var y = offset.Y - (offset.X + (offset.X & 1)) / 2;
        return new CubeCoordinates(offset.X, y, -offset.X - y);
    }

    public static implicit operator CubeCoordinates(OffsetEvenRowCoordinates offset)
    {
        var x = offset.X - (offset.Y + (offset.Y & 1)) / 2;
        return new CubeCoordinates(x, offset.Y, -x - offset.Y);
    }

    public static implicit operator CubeCoordinates(OffsetOddColumnCoordinates offset)
    {
        var y = offset.Y - (offset.X - (offset.X & 1)) / 2;
        return new CubeCoordinates(offset.X, y, -offset.X - y);
    }

    public static implicit operator CubeCoordinates(OffsetOddRowCoordinates offset)
    {
        var x = offset.X - (offset.Y - (offset.Y & 1)) / 2;
        return new CubeCoordinates(x, offset.Y, -x - offset.Y);
    }

    private static float Lerp(float a, float b, float t) => a + (b - a) * t;

    CubeCoordinates IHexGridCoordinates<CubeCoordinates>.ConvertFrom(CubeCoordinates cube) => cube;
    CubeCoordinates IHexGridCoordinates<CubeCoordinates>.ConvertToCube() => this;
}