#pragma once
#include <string>
#include <glm/glm.hpp>
#include <glm/gtc/matrix_transform.hpp>
#include <glm/gtc/type_ptr.hpp>
class Shader
{ 
public: 
	Shader(const char* vertexPath, const char* fragmentPath);
	std::string vertexString;
	std::string fragmentString; 
	const char* vertexSource;
	const char* fragmentSource;
	unsigned int ID;//Shader Program ID

	enum Slot
	{
		DIFFUSE,SPECULAR
	};


	void ShaderProgram();//Shader主输出入口
	void setUniform3f(const char* paramNameString,glm::vec3 param);
	void setUniform1f(const char* paramNameString,float param);
	void setUniform1i(const char* paramNameString,int param);

private:
	void checkCompileError(unsigned int ID, std::string type);
};

