using OpenTK.Mathematics;
using CAEngine.Core;
using CAEngine.Utils;
using OpenTK.Graphics.OpenGL4;

namespace CAEngine.Rendering;

public class Renderer : IDisposable
    {
        private Shader _shader;
        private Mesh _cubeMesh;
        private Texture2D _tileTexture;

        private TileMap _tileMap;

        private Matrix4 _projection;
        private Matrix4 _view;

        public Renderer(int width, int height)
        {
            _shader = new Shader(BasicShaders.Vertex, BasicShaders.Fragment);

            // Textured cube
            _cubeMesh = Shapes.CreateRegionedCube(1.0f);

            // Load a texture (adjust path!)
            _tileTexture = new Texture2D("Assets/Textures/tile.png");

            // Tilemap
            _tileMap = new TileMap(10, 10, 1.0f);

            // Camera (isometric-style)
            var cameraPos = new Vector3(10f, 10f, 10f);
            var target    = Vector3.Zero;
            var up        = Vector3.UnitY;

            _view = Matrix4.LookAt(cameraPos, target, up);

            UpdateProjection(width, height);
        }

        public void Update(float deltaTime)
        {
            // Tilemap is static for now
        }

        public void Render()
        {
            _shader.Use();

            // Bind texture to unit 0
            _tileTexture.Bind(TextureUnit.Texture0);
            _shader.SetInt("uTexture", 0);

            _shader.SetMatrix4("uView", _view);
            _shader.SetMatrix4("uProjection", _projection);

            foreach (var tile in _tileMap.Tiles)
            {
                Matrix4 model = tile.WorldMatrix;
                _shader.SetMatrix4("uModel", model);

                _cubeMesh.Draw();
            }
        }

        public void Resize(int width, int height)
        {
            UpdateProjection(width, height);
        }

        private void UpdateProjection(int width, int height)
        {
            float aspect = width / (float)height;
            float orthoSize = 12.0f;

            _projection = Matrix4.CreateOrthographic(
                orthoSize * aspect,
                orthoSize,
                0.1f,
                100f
            );
        }

        public void Dispose()
        {
            _cubeMesh?.Dispose();
            _shader?.Dispose();
            _tileTexture?.Dispose();
        }
    }