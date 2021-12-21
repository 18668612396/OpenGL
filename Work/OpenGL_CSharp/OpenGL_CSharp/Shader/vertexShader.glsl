#version 330 core
layout (location = 0) in vec3 aPosition;
layout (location = 1) in vec4 aColor;
layout (location = 15) in vec2 aUV;
out vec4 vertexColor;
out vec2 uv;
void main()
{
    gl_Position = vec4(aPosition, 1.0);
    vertexColor = aColor;
    uv = aUV;
}