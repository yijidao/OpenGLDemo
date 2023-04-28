// See https://aka.ms/new-console-template for more information

using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using System.Drawing;
using System.Reflection.Metadata;


Console.WriteLine("Hello, World!");

var option = WindowOptions.Default with
{
    Title = "ShaderDemo",
    Size = new Vector2D<int>(1200, 900)
};
var window = Window.Create(option);

GL _gl = null;
uint vbo, vao = 0;
Shader.Shader _shader = null;

unsafe void OnWindowOnLoad()
{
    float[] vertices =
    {
        // 位置                // 颜色
        0f, 0.5f, 0.0f,       1.0f, 0.0f, 0.0f,
        0.5f, -0.5f, 0.0f,    0.0f, 1.0f, 0.0f,
        -0.5f, -0.5f, 0.0f,   0.0f, 0.0f, 1.0f
    };

    _gl = window.CreateOpenGL();
    _gl.ClearColor(Color.DarkOliveGreen);


    //_shader = new Shader.Shader(_gl, $"{Path.Combine(Directory.GetCurrentDirectory(), "shader.vert")}",
    //    $"{Path.Combine(Directory.GetCurrentDirectory(), "shader_uniform.frag")}");
    _shader = new Shader.Shader(_gl, $"{Path.Combine(Directory.GetCurrentDirectory(), "shader.vert")}",
        $"{Path.Combine(Directory.GetCurrentDirectory(), "shader.frag")}");


    vao = _gl.GenVertexArray();
    vbo = _gl.GenBuffer();

    _gl.BindVertexArray(vao);
    _gl.BindBuffer(BufferTargetARB.ArrayBuffer, vbo);
    fixed (float* buf = vertices)
    {
        _gl.BufferData(BufferTargetARB.ArrayBuffer, (uint)(vertices.Length * sizeof(float)), buf, BufferUsageARB.StaticDraw);
    }

    //_gl.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), (void*)0);
    //_gl.EnableVertexAttribArray(0);

    _gl.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), (void*)0);
    _gl.EnableVertexAttribArray(0);

    _gl.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), (void*)(3 * sizeof(float)));
    _gl.EnableVertexAttribArray(1);

}

unsafe void OnWindowOnRender(double d)
{
    _gl.Clear(ClearBufferMask.ColorBufferBit);

    _gl.BindVertexArray(vao);
    _shader.Use();

    //var f = DateTime.Now.Second / 59f;
    //_shader.SetUniform("vertexColor2", f);

    _gl.DrawArrays(PrimitiveType.Triangles, 0, 3);


}

window.Load += OnWindowOnLoad;
window.Render += OnWindowOnRender;
window.Run();