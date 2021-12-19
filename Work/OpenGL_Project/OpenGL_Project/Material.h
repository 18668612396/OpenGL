#pragma once
#include "Shader.h"

#include <glm/glm.hpp>
#include <glm/gtc/matrix_transform.hpp>
#include <glm/gtc/type_ptr.hpp>

class Material
{
public:
	unsigned int diffuseTexture;
	unsigned int specularTexture;
	glm::vec3 albedo;
	Shader* shader;
	Material(Shader* _Shader, unsigned int _MainTex, unsigned int _SpecTex,glm::vec3 _Albedo);
};

