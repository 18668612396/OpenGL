#version 330 core
out vec4 FragColor;
in vec4 vertexColor;
in vec2 uv;

uniform vec3 test;
void main()
{

 FragColor = vec4(test,1);
}