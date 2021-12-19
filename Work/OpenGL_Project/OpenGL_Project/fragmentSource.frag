#version 330 core
out vec4 FragColor;
in vec2 uv;
in vec3 worldNormal;
in vec4 worldPos;
uniform vec3 lightPos;
uniform vec3 lightColor;
uniform vec3 cameraPos;
uniform vec4 _Time;
uniform vec3 lightDir;
struct Material{
	sampler2D diffuseTexture;
	sampler2D specularTexture;
	vec3 ambientColor;
};
uniform Material material;


void main()
{



//向量准备
vec3 lightDir = normalize(lightDir);
vec3 viewDir  = normalize(cameraPos - worldPos.xyz);
vec3 normalDir = normalize(worldNormal);
vec3 reflectDir = reflect(-lightDir,normalDir);
//dot
float NdotL = max(0.0f,dot(normalDir,lightDir));
float VdotR = max(0.0f,dot(viewDir,reflectDir));

//纹理采样
vec4 var_diffuseTexture = texture(material.diffuseTexture,uv);
vec4 var_specularTexture = texture(material.specularTexture,uv);
//光照模型
vec3  lambert      = NdotL * var_diffuseTexture.rgb;
vec3  Phong		   = pow(VdotR,64) * lightColor;

vec3 specularColor = Phong * var_specularTexture.rgb;
vec3 diffuseColor  = lambert;
vec3 ambientColor  = material.ambientColor * var_diffuseTexture.rgb;
FragColor = vec4(NdotL,NdotL,NdotL,1.0);

}