
#include "Material.h"


Material::Material(Shader* _Shader, unsigned int _MainTex, unsigned int _SpecTex, glm::vec3 _Albedo)

{
	shader = _Shader;
	diffuseTexture = _MainTex;
	specularTexture = _SpecTex;
	albedo = _Albedo;

}
