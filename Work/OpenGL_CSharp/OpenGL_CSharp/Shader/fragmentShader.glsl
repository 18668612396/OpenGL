#version 330 core
out vec4 FragColor;
in vec4 vertexColor;
in vec2 uv;
uniform sampler2D diffuseTex;
uniform sampler2D specularTex;
void main()
{
 vec3 var_diffuseTex = texture(diffuseTex,uv).rgb;
 vec3 var_specularTex = texture(specularTex,uv).rgb;
 FragColor = vec4(var_specularTex.rgb,1);
}