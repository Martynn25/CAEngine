using OpenTK.Mathematics;
namespace CAEngine.Rendering;

public struct Vertex
{
    public Vector3 Position;
    public Vector2 TexCoord;

    public Vertex(Vector3 position, Vector2 texCoord)
    {
        Position = position;
        TexCoord = texCoord;
    }

    public const int SizeInBytes = (3 + 2) * sizeof(float); // pos(3) + uv(2)
}