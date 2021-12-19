#pragma once
#include<glm/glm.hpp>
#include<glm/gtx/rotate_vector.hpp>
class LightPoint
{
public:
	glm::vec3 position;
	glm::vec3 color;
	float attenuation;
	LightPoint(glm::vec3 _position,float _attenuation,glm::vec3 _color = glm::vec3(1.0f, 1.0f, 1.0f));

	float constant;
	float linear;
	float quadratic;
};

