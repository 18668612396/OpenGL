#include "LightPoint.h"

LightPoint::LightPoint(glm::vec3 _position, float _attenuation, glm::vec3 _color):
	position(_position),attenuation(_attenuation),color(_color)
{
	 constant = 1.0f;
	 linear = 0.09f;
	 quadratic = 0.032f;
}
