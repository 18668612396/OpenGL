#version 330 core //Using version GLSL version 3.3
layout (location = 0) in vec4 vPos;
layout (location = 1) in vec4 vColor;
layout (location = 2) in vec2 texcoord;
out vec4 vertexColor;
out vec2 uv;

uniform mat4 RotationMatrix;
uniform mat4 ModelMatrix;
void main()
{
    uv = texcoord;
    vertexColor = vColor;
    gl_Position =ModelMatrix * RotationMatrix * vec4(vPos.x, vPos.y, vPos.z, 1.0);
}