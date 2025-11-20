using CAEngine.Rendering;
namespace CAEngine.Core;

public class Engine : IDisposable
{
    private Renderer? _renderer;

    public void Initialize(int width, int height)
    {
        _renderer = new Renderer(width, height);
    }

    public void Update(float deltaTime)
    {
        _renderer?.Update(deltaTime);
    }

    public void Render()
    {
        _renderer?.Render();
    }

    public void Resize(int width, int height)
    {
        _renderer?.Resize(width, height);
    }

    public void Dispose()
    {
        _renderer?.Dispose();
    }
}