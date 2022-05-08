namespace OakLab.HexGeometry;

public readonly record struct CubeFloatingCoordinates(float X, float Y, float Z)
{
    public CubeCoordinates Round()
    {
        var x = MathF.Round(X);
        var y = MathF.Round(Y);
        var z = MathF.Round(Z);

        var xDiff = MathF.Round(x - X);
        var yDiff = MathF.Round(y - Y);
        var zDiff = MathF.Round(z - Z);

        if (xDiff > yDiff && xDiff > zDiff)
            x = -y - z;
        else if (yDiff > zDiff)
            y = -x - z;
        else
            z = -x - y;

        return new CubeCoordinates((int) x, (int) y, (int) z);
    }
}