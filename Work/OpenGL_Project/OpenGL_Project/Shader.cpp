#include "Shader.h"
#include <iostream>
#include <fstream>
#include <sstream>

#define GLEW_STATIC
#include <GL/glew.h>
#include <GLFW/glfw3.h>

using namespace std;
Shader::Shader( const char* vertexPath,const char* fragmentPath)
{
	ifstream vertexFile;
	ifstream fragmentFile;
	stringstream vertexSStream;
	stringstream fragmentSStreaml;

	vertexFile.open(vertexPath);
	fragmentFile.open(fragmentPath);
	vertexFile.exceptions(ifstream::failbit || ifstream::badbit);
	fragmentFile.exceptions(ifstream::failbit || ifstream::badbit);
	try
	{
		if (!vertexFile.is_open() || !fragmentFile.is_open())
		{
			throw exception("Open File Error");
		}
		//读取顶点片元着色器文件到SStream
		vertexSStream << vertexFile.rdbuf();
		fragmentSStreaml << fragmentFile.rdbuf();
		//读取SStream到String
		vertexString = vertexSStream.str();
		fragmentString = fragmentSStreaml.str();
		//读取String到我们可认的ShaderSource里
		vertexSource = vertexString.c_str();
		fragmentSource = fragmentString.c_str();

		//定义顶点片元着色器
		unsigned int vertex, fragment;
		//创建顶点着色器
		vertex = glCreateShader(GL_VERTEX_SHADER);
		glShaderSource(vertex, 1, &vertexSource, NULL);
		glCompileShader(vertex);
		checkCompileError(vertex, "VERTEX");
		//创建片元着色器
		fragment = glCreateShader(GL_FRAGMENT_SHADER);
		glShaderSource(fragment, 1, &fragmentSource, NULL);
		glCompileShader(fragment);
		checkCompileError(fragment, "FRAGMENT");
		//创建Program
		ID = glCreateProgram();
		glAttachShader(ID, vertex);
		glAttachShader(ID, fragment);
		glLinkProgram(ID);//引入
		glDeleteShader(vertex);//删除原顶点着色器
		glDeleteShader(fragment);//删除原片元着色器
		checkCompileError(ID, "PROGRAM");
	}

	catch (const std::exception& ex)
	{
		printf(ex.what());
	}
}
void Shader::ShaderProgram()
{
	glUseProgram(ID);
}

void Shader::setUniform3f(const char* paramNameString, glm::vec3 param)
{
	glUniform3f(glGetUniformLocation(ID, paramNameString), param.x, param.y, param.z);
}

void Shader::setUniform1f(const char* paramNameString, float param)
{
	glUniform1f(glGetUniformLocation(ID, paramNameString), param);
}

void Shader::setUniform1i(const char* paramNameString, int param)
{
	glUniform1i(glGetUniformLocation(ID, paramNameString), param);
}

void Shader::checkCompileError(unsigned int ID, std::string type)
{
	int success;
	char infoLog[512];

	if (type != "PROGRAM")
	{
		glGetShaderiv(ID, GL_COMPILE_STATUS, &success);
		if (!success)
		{
			glGetShaderInfoLog(ID, 512, NULL, infoLog);
			cout << "shader compile error:" << infoLog << endl;
		}
		else
		{
			glGetProgramiv(ID, GL_LINK_STATUS, &success);
			if (!success)
			{
				glGetProgramInfoLog(ID, 512, NULL, infoLog);
				cout << "program linking error:" << infoLog << endl;
			}
		}
	}

}