using OpenTK.Mathematics;
namespace CAEngine.Rendering;

public struct UvRegion
{
    public Vector2 Min; // (u0, v0)
    public Vector2 Max; // (u1, v1)

    public UvRegion(Vector2 min, Vector2 max)
    {
        Min = min;
        Max = max;
    }

    public static UvRegion FromCell(int col, int row, int cols, int rows)
    {
        float u0 = col       / (float)cols;
        float u1 = (col + 1) / (float)cols;
        float v0 = row       / (float)rows;
        float v1 = (row + 1) / (float)rows;

        return new UvRegion(new Vector2(u0, v0), new Vector2(u1, v1));
    }
}