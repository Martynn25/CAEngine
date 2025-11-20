using CAEngine.Core;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace CAEngine.Platform;

public class CAGame : GameWindow
{
    private readonly Engine _engine = new Engine();
    public CAGame()
        : base(
            GameWindowSettings.Default,
            new NativeWindowSettings()
            {
                Size  = new Vector2i(1280, 720),
                Title = "Shinanigans Engine"
            })
    {
    }

    protected override void OnLoad()
    {
        base.OnLoad();

        // OpenGL setup
        GL.ClearColor(0.1f, 0.1f, 0.3f, 1.0f);
        GL.Enable(EnableCap.DepthTest);
        _engine.Initialize(Size.X, Size.Y);
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        base.OnUpdateFrame(args);

        // Press Escape to quit
        if (KeyboardState.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.Escape))
        {
            Close();
        }

        _engine.Update((float)args.Time);
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);

        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        _engine.Render();

        SwapBuffers();
    }

    protected override void OnResize(ResizeEventArgs e)
    {
        base.OnResize(e);
        GL.Viewport(0, 0, Size.X, Size.Y);
        _engine.Resize(Size.X, Size.Y);
    }
    protected override void OnUnload()
    {
        _engine.Dispose();
        base.OnUnload();
    }
}