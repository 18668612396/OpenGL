#version 330 core
layout (location = 0) in vec3 aPosition;
layout (location = 1) in vec4 aColor;
layout (location = 15) in vec2 aUV;
out vec4 vertexColor;
out vec2 uv;


//uniform mat4 trans;
void main()
{
//    mat4 trans = mat4(vec4(1,0,0,0),vec4(0,1,0,0),vec4(0,0,1,0),vec4(0,0,0,1));
    gl_Position =  vec4(aPosition, 1.0);
    vertexColor = aColor;
    uv = aUV;
}