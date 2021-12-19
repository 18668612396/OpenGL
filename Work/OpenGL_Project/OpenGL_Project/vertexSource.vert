#version 330 core

layout(location = 0) in vec3 aPos;
//layout(location = 14) in vec4 aColor;
layout(location = 2)  in vec2 atexCoord;
layout(location = 1)  in vec3  anormal;



out vec4 vertexColor;
out vec2 uv;
out vec3 worldNormal;
out vec4 worldPos;
uniform mat4 modelMat;
uniform mat4 viewMat;
uniform mat4 projMat;
void main()
{
gl_Position = projMat * viewMat * modelMat * vec4(aPos, 1.0f);
worldPos = modelMat * vec4(aPos, 1.0f);
//vertexColor = aColor;
uv = atexCoord.xy;
worldNormal = mat3(modelMat) * anormal;

}