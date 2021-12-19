#include "ShaderInput.h"
#include "Shader.h"
#include "Camera.h"


#include <GL/glew.h>
#include <GLFW/glfw3.h>



void ShaderInput::ShaderInputParam(Shader* shader)
{
	shader->ShaderProgram();//Ê¹ÓÃShader Program

	glUniform3f(glGetUniformLocation(shader->ID, "ambientColor"), 0.0f, 0.5f, 0.0f);
	glUniform3f(glGetUniformLocation(shader->ID, "lightColor"), 1.0f, 1.0f, 1.0f);
	glUniform3f(glGetUniformLocation(shader->ID, "lightPos"), 10.0f, 10.0f, -5.0f);
	glUniform3f(glGetUniformLocation(shader->ID, "cameraPos"),camer, camera.Position.y, camera.Position.z);
	glUniform4f(glGetUniformLocation(shader->ID, "_Time"), glfwGetTime() * 0.02, glfwGetTime(), glfwGetTime() * 2, glfwGetTime() * 20);

}
