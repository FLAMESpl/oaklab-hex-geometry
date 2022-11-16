namespace OakLab.HexGeometry;

public interface IHexGridCoordinates<out T> where T : struct, IHexGridCoordinates<T>
{
    T ConvertFrom(CubeCoordinates cube);
    CubeCoordinates ConvertToCube();
}
