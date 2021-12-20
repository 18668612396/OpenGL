#version 330 core
out vec4 FragColor;
in vec4 vertexColor;
in vec2 uv;

uniform sampler2D _MainTex;

void main()
{
//    FragColor = vec4(1.0f, 0.5f, 0.2f, 1.0f);
//    var_MainTex = texture(_MainTex,uv);
    FragColor = vec4(uv,1,1);
}