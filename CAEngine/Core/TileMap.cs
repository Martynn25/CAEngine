using OpenTK.Mathematics;
using CAEngine.Utils;
namespace CAEngine.Core;

public class TileMap
{
    public int Width { get; }
    public int Height { get; }
    public float TileSize { get; }

    private readonly List<Transform> _tiles = new List<Transform>();

    /// <summary>
    /// Creates a flat grid of tiles (cubes) on the XZ plane centered around the origin.
    /// </summary>
    public TileMap(int width, int height, float tileSize)
    {
        Width = width;
        Height = height;
        TileSize = tileSize;

        GenerateTiles();
    }

    private void GenerateTiles()
    {
        _tiles.Clear();

        float offsetX = (Width  - 1) * TileSize * 0.5f;
        float offsetZ = (Height - 1) * TileSize * 0.5f;

        for (int z = 0; z < Height; z++)
        {
            for (int x = 0; x < Width; x++)
            {
                var t = new Transform();

                float worldX = x * TileSize - offsetX;
                float worldZ = z * TileSize - offsetZ;
                float worldY = 0.0f; // all tiles on the ground

                t.Position = new Vector3(worldX, worldY, worldZ);
                t.Scale    = new Vector3(TileSize, TileSize, TileSize);
                t.Rotation = Vector3.Zero;

                _tiles.Add(t);
            }
        }
    }

    public IEnumerable<Transform> Tiles => _tiles;
}