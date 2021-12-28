#VERTEX
#version 330 core //Using version GLSL version 3.3
layout (location = 0) in vec3 vPos;
layout (location = 1) in vec3 vNormal;
layout (location = 2) in vec2 texcoord;

out vec3 worldPos;
out vec3 worldNormal;
out vec2 uv;

uniform mat4 Matrix_ObjectToWorld;
uniform mat4 Matrix_WorldToView;
uniform mat4 Matrix_ViewToHClip;
uniform vec4 _WorldSpaceCameraPos;
void main()
{
    worldPos = (Matrix_ObjectToWorld * vec4(vPos.xyz, 1.0)).xyz;
    gl_Position = Matrix_ViewToHClip * Matrix_WorldToView * Matrix_ObjectToWorld * vec4(vPos.xyz, 1.0);

    uv = texcoord;
    worldNormal =mat3(transpose(inverse(Matrix_ObjectToWorld))) * vNormal;
}
    #VERTEND
    //======================================================================================================================
#FRAGMENT
#version 330 core
out vec4 FragColor;
in vec3 worldNormal;
in vec4 wordView;
in vec3 worldPos;
in vec2 uv;
uniform sampler2D diffuse;
uniform sampler2D specular;
uniform vec3 LightPos;
uniform vec3 _WorldSpaceCameraPos;
void main()
{
    //Value
    vec3 normalDir = normalize(worldNormal).xyz;
    vec3 lightDir  = normalize(LightPos.xyz);
    vec3 viewDir   = normalize(_WorldSpaceCameraPos - worldPos).xyz;
    vec3 reflectDir= reflect(-lightDir, normalDir);
    //SampleTexture
    vec4 var_diffuse = texture(diffuse, uv);
    vec4 var_specular = texture(specular, uv);
    //Dot
    float NdotL = max(0.0f, dot(normalDir, lightDir));
    float VdotR = max(0.0f, dot(viewDir, reflectDir));

    vec3 ambientColor = vec3(0.3f, 0.3f, 0.3f) * var_diffuse.rgb;
    vec3 diffuseColor = var_diffuse.rgb * NdotL;
    vec3 specularColor = diffuseColor * pow(VdotR, 64);



    FragColor = vec4(1,0,0,1);

}
    #FRAGEND
























