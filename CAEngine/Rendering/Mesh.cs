using OpenTK.Graphics.OpenGL4;
namespace CAEngine.Rendering;

public class Mesh : IDisposable
    {
        public int Vao { get; private set; }
        private int _vbo;
        private int _ebo;
        public int IndexCount { get; private set; }

        public Mesh(Vertex[] vertices, uint[] indices)
        {
            IndexCount = indices.Length;

            Vao = GL.GenVertexArray();
            _vbo = GL.GenBuffer();
            _ebo = GL.GenBuffer();

            GL.BindVertexArray(Vao);

            // Vertex buffer
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
            GL.BufferData(
                BufferTarget.ArrayBuffer,
                vertices.Length * Vertex.SizeInBytes,
                vertices,
                BufferUsageHint.StaticDraw);

            // Index buffer
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _ebo);
            GL.BufferData(
                BufferTarget.ElementArrayBuffer,
                indices.Length * sizeof(uint),
                indices,
                BufferUsageHint.StaticDraw);

            // Layout: location 0 = position (vec3), location 1 = color (vec3)
            int stride = Vertex.SizeInBytes;

            // Position
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(
                index: 0,
                size: 3,
                type: VertexAttribPointerType.Float,
                normalized: false,
                stride: stride,
                offset: 0);

            // TexCoord
            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(
                index: 1,
                size: 2,
                type: VertexAttribPointerType.Float,
                normalized: false,
                stride: stride,
                offset: 3 * sizeof(float));

            GL.BindVertexArray(0);
        }

        public void Draw()
        {
            GL.BindVertexArray(Vao);
            GL.DrawElements(
                PrimitiveType.Triangles,
                IndexCount,
                DrawElementsType.UnsignedInt,
                0);
        }

        public void Dispose()
        {
            GL.DeleteBuffer(_vbo);
            GL.DeleteBuffer(_ebo);
            GL.DeleteVertexArray(Vao);
        }
    }