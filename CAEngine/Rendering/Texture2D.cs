using OpenTK.Graphics.OpenGL4;
using StbImageSharp;
namespace CAEngine.Rendering;

public class Texture2D : IDisposable
    {
        public int Handle { get; private set; }

        public Texture2D(string path)
        {
            path = Path.Combine(AppContext.BaseDirectory, path);
            if (!File.Exists(path))
                throw new FileNotFoundException($"Texture file not found: {path}");

            // Load image with StbImageSharp
            ImageResult image;
            using (var stream = File.OpenRead(path))
            {
                // Flip vertically so OpenGL's (0,0) matches bottom-left
                StbImage.stbi_set_flip_vertically_on_load(1);
                image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
            }

            Handle = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, Handle);

            GL.TexImage2D(
                TextureTarget.Texture2D,
                level: 0,
                internalformat: PixelInternalFormat.Rgba,
                width: image.Width,
                height: image.Height,
                border: 0,
                format: PixelFormat.Rgba,
                type: PixelType.UnsignedByte,
                pixels: image.Data);

            // Texture parameters
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        public void Bind(TextureUnit unit = TextureUnit.Texture0)
        {
            GL.ActiveTexture(unit);
            GL.BindTexture(TextureTarget.Texture2D, Handle);
        }

        public void Dispose()
        {
            GL.DeleteTexture(Handle);
        }
    }