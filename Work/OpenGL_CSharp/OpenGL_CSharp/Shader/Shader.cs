
using System.IO;
using System.Text;
using OpenTK.Graphics.OpenGL;

namespace OpenGL_CSharp
{

    public class Shader
    {
        
        #region ShaderSource
        
        private OpenTK.Graphics.ProgramHandle shaderProgram;
        //定义vertexShader变量
        private OpenTK.Graphics.ShaderHandle vertexShader;
        private OpenTK.Graphics.ShaderHandle fragmentShader;


           
        #endregion
        

        public Shader(string vertexPath, string fragmentPath)
        {
     
            //读取顶点着色器完整内容
            var vertexSource = File.ReadAllText(vertexPath);
            //读取片元着色器完整内容
            var fragmentSource = File.ReadAllText(fragmentPath);
            
            //创建一个顶点着色器返还给我们定义好的Shader变量
            vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, vertexSource); //将shader代码给定到vertexShader变量内
            GL.CompileShader(vertexShader); //编译顶点着色器

            //创建一个顶点着色器返还给我们定义好的Shader变量
            fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, fragmentSource); //将shader代码给定到FragmentShader变量内
            GL.CompileShader(fragmentShader); //编译顶点着色器

            shaderProgram = GL.CreateProgram(); //创建Program //这个相当于渲染管线的片段或者说是插槽，里面装载了一些我们需要的shader，然后填充进渲染管线里
            GL.AttachShader(shaderProgram, vertexShader); //添加顶点着色器到shaderProgram
            GL.AttachShader(shaderProgram, fragmentShader); //添加片元着色器到shaderProgram
            GL.LinkProgram(shaderProgram); //将Program填充进渲染管线内
         }

        public void useShader()
        {
            GL.UseProgram(shaderProgram); //使用shaderProgram插入渲染管线内
        }
    }
 
}
