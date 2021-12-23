#VERTEX
#version 330 core //Using version GLSL version 3.3
layout (location = 0) in vec4 vPos;
layout (location = 1) in vec4 vColor;
layout (location = 2) in vec2 texcoord;
out vec4 vertexColor;
out vec2 uv;

//uniform mat4 RotationMatrix;
uniform mat4 Matrix_ObjectToWorld;
uniform mat4 Matrix_WorldToView;
uniform mat4 Matrix_ViewToHClip;
void main()
{
    uv = texcoord;
    vertexColor = vColor;
    gl_Position = Matrix_ViewToHClip * Matrix_WorldToView * Matrix_ObjectToWorld * vec4(vPos.x, vPos.y, vPos.z, 1.0);
}
#VERTEND







#FRAGMENT
#version 330 core
out vec4 FragColor;
in vec4 vertexColor;
in vec2 uv;
uniform sampler2D diffuse;
uniform sampler2D specular;
uniform float test;
void main()
{
    vec4 var_diffuse = texture(diffuse, uv);
    vec4 var_specular = texture(specular, uv);
    vec3 finalColor = mix(var_diffuse.rgb,var_specular.rgb,0.2);
    FragColor = vec4(finalColor.rgb, uv);
    //    FragColor = vec4(test,test,test,1);
}
#FRAGEND
























