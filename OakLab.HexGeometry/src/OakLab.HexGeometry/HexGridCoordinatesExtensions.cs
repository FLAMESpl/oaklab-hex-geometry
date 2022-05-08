namespace OakLab.HexGeometry;

public static class HexGridCoordinatesExtensions
{
    public static int DistanceTo<TCoordinates>(this TCoordinates first, CubeCoordinates second)
        where TCoordinates : struct, IHexGridCoordinates<TCoordinates>
    {
        var (x, y, z) = first.ConvertToCube() - second;
        return (Math.Abs(x) + Math.Abs(y) + Math.Abs(z)) / 2;
    }

    public static bool IsDiagonallyNeighbouringWith<TCoordinates>(this TCoordinates first, CubeCoordinates second)
        where TCoordinates : struct, IHexGridCoordinates<TCoordinates>
    {
        ReadOnlySpan<CubeCoordinates> vectors = stackalloc CubeCoordinates[6]
        {
            new CubeCoordinates(2, -1, -1),
            new CubeCoordinates(-1, 2, -1),
            new CubeCoordinates(-1, -1, 2),
            new CubeCoordinates(-2, 1, 1),
            new CubeCoordinates(1, -2, 1),
            new CubeCoordinates(1, 1, -2)
        };

        var cubeFirst = first.ConvertToCube();

        foreach (var vector in vectors)
            if (cubeFirst + vector == second)
                return true;

        return false;
    }

    public static bool IsNeighbouringWith<TCoordinates>(this TCoordinates first, CubeCoordinates second)
        where TCoordinates : struct, IHexGridCoordinates<TCoordinates>
    {
        ReadOnlySpan<CubeCoordinates> vectors = stackalloc CubeCoordinates[6]
        {
            new CubeCoordinates(1, 0, -1),
            new CubeCoordinates(1, -1, 0),
            new CubeCoordinates(0, -1, 1),
            new CubeCoordinates(-1, 0, 1),
            new CubeCoordinates(-1, 1, 0),
            new CubeCoordinates(0, 1, -1)
        };

        var cubeFirst = first.ConvertToCube();

        foreach (var vector in vectors)
            if (cubeFirst + vector == second)
                return true;

        return false;
    }

    public static IEnumerable<TCoordinates> LineTo<TCoordinates>(this TCoordinates from, CubeCoordinates to)
        where TCoordinates : struct, IHexGridCoordinates<TCoordinates>
    {
        var cubeFrom = from.ConvertToCube();
        return Enumerable
            .Range(0, cubeFrom.DistanceTo(to))
            .Select((n, i) => default(TCoordinates).ConvertFrom(cubeFrom.Lerp(to, 1 / n * i).Round()));
    }

    public static TCoordinates RotateClockwise<TCoordinates>(this TCoordinates coordinates, CubeCoordinates center)
        where TCoordinates : struct, IHexGridCoordinates<TCoordinates>
    {
        var cubeCoordinates = coordinates.ConvertToCube();
        var rotated = (cubeCoordinates - center).RotateClockwise() + center;
        return default(TCoordinates).ConvertFrom(rotated);
    }
    
    public static TCoordinates RotateCounterclockwise<TCoordinates>(this TCoordinates coordinates, CubeCoordinates center)
        where TCoordinates : struct, IHexGridCoordinates<TCoordinates>
    {
        var cubeCoordinates = coordinates.ConvertToCube();
        var rotated = (cubeCoordinates - center).RotateCounterclockwise() + center;
        return default(TCoordinates).ConvertFrom(rotated);
    }

    public static TCoordinates ReflectXAgainst<TCoordinates>(this TCoordinates coordinates, CubeCoordinates reference)
        where TCoordinates : struct, IHexGridCoordinates<TCoordinates>
    {
        var cubeCoordinates = coordinates.ConvertToCube();
        var rotated = (cubeCoordinates - reference).ReflectX() + reference;
        return default(TCoordinates).ConvertFrom(rotated);
    }
    
    public static TCoordinates ReflectYAgainst<TCoordinates>(this TCoordinates coordinates, CubeCoordinates reference)
        where TCoordinates : struct, IHexGridCoordinates<TCoordinates>
    {
        var cubeCoordinates = coordinates.ConvertToCube();
        var rotated = (cubeCoordinates - reference).ReflectY() + reference;
        return default(TCoordinates).ConvertFrom(rotated);
    }
    
    public static TCoordinates ReflectZAgainst<TCoordinates>(this TCoordinates coordinates, CubeCoordinates reference)
        where TCoordinates : struct, IHexGridCoordinates<TCoordinates>
    {
        var cubeCoordinates = coordinates.ConvertToCube();
        var rotated = (cubeCoordinates - reference).ReflectZ() + reference;
        return default(TCoordinates).ConvertFrom(rotated);
    }
}
