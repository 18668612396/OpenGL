
	#pragma region ͷ�ļ�
	#include <iostream>
	#define GLEW_STATIC
	#include <GL/glew.h>
	#include <GLFW/glfw3.h>

	#include "Shader.h"
	#include "Material.h"

	#define STB_IMAGE_IMPLEMENTATION
	#include "stb_image.h"

	#include <glm/glm.hpp>
	#include <glm/gtc/matrix_transform.hpp>
	#include <glm/gtc/type_ptr.hpp>

	#include "Camera.h"
	#include "Model.h"
	#include "Mesh.h"
	#include "LightDirectional.h"
	#include "LightPoint.h"
	#pragma endregion

	#pragma region �������
		Camera camera(glm::vec3(0, 0, 3.0f), glm::radians(15.0f), glm::radians(180.0f), glm::vec3(0, 1.0f, 0));
	#pragma endregion

	#pragma region ��Դ����

		LightDirectional directionalLight = LightDirectional
		(
			glm::vec3(1.2f, 1.0f, 2.0f),
			glm::vec3(glm::radians(-45.0f),glm::radians(150.0f), 0),
			glm::vec3(1.0f, 1.0f, 1.0f)
		);

		LightPoint pointLight = LightPoint(glm::vec3(1.0f, 1.0f, -1.0f), 1.0f, glm::vec3(1.0f, 1.0f, 1.0f));
	
	#pragma endregion

	#pragma region ���̺��������
	float lastX;
	float lastY;
	float runSpeed;

	void processInput(GLFWwindow* window)
	{
		if (glfwGetKey(window, GLFW_KEY_ESCAPE) == GLFW_PRESS)
		{
			glfwSetWindowShouldClose(window, true);
		}

		if (glfwGetKey(window, GLFW_KEY_LEFT_SHIFT) == GLFW_PRESS)
		{
			runSpeed = 5.0f;
		}
		else
		{
			runSpeed = 1.0f;
		}

		if (glfwGetKey(window, GLFW_KEY_W) == GLFW_PRESS)
		{
			camera.MoveZ = -runSpeed;
		}

		else if (glfwGetKey(window, GLFW_KEY_S) == GLFW_PRESS)
		{
			camera.MoveZ = runSpeed;
		}
		else
		{
			camera.MoveZ = 0;
		}

		if (glfwGetKey(window, GLFW_KEY_A) == GLFW_PRESS)
		{
			camera.MoveX = -runSpeed;
		}
		else if (glfwGetKey(window, GLFW_KEY_D) == GLFW_PRESS)
		{
			camera.MoveX = runSpeed;
		}
		else
		{
			camera.MoveX = 0;
		}
		if (glfwGetKey(window, GLFW_KEY_Q) == GLFW_PRESS)
		{
			camera.MoveY = -runSpeed;
		}
		else if (glfwGetKey(window, GLFW_KEY_E) == GLFW_PRESS)
		{
			camera.MoveY = runSpeed;
		}
		else
		{
			camera.MoveY = 0;
		}
	}

	//Instantiate Camera Class
	//Camera camera(glm::vec3(0, 0, 3.0f), glm::vec3(0, 0, 0), glm::vec3(0, 1.0f, 0));


	bool isRightButtonEnble;
	void mouse_callback(GLFWwindow* window, double xPos, double yPos)
	{
		float deltaX, deltaY;
		if (isRightButtonEnble)
		{
		deltaX = xPos - lastX;
		deltaY = yPos - lastY;

		camera.ProcessMouseMovement(deltaX, deltaY);
		}
		lastX = xPos;
		lastY = yPos;

	}
	void mouse_button_callback(GLFWwindow* window, int button, int action, int mods)
	{
		if (button == GLFW_MOUSE_BUTTON_RIGHT && action == GLFW_PRESS)
		{
			glfwSetInputMode(window, GLFW_CURSOR, GLFW_CURSOR_DISABLED);//�ر����
			isRightButtonEnble = true;
		}
		else
		{
			isRightButtonEnble = false;
			glfwSetInputMode(window, GLFW_CURSOR, GLFW_CURSOR_NORMAL);//�������
		}


	}
	#pragma endregion

	unsigned int LoadingTexture2D(const char* fileName,int textureSlot)
	{
		unsigned int texBuffer;
		glGenTextures(1, &texBuffer);
		glActiveTexture(GL_TEXTURE0 + textureSlot);
		glBindTexture(GL_TEXTURE_2D, texBuffer);
		int width, height, nrChannel;
		stbi_set_flip_vertically_on_load(true);//��ֱ��ת����
		unsigned char* data = stbi_load(fileName, &width, &height, &nrChannel, 0);
		if (data)
		{
			GLenum format;
			if (nrChannel == 1)
			{
				format = GL_RED;
			}
			if (nrChannel == 3)
			{
				format = GL_RGB;
			}
			if (nrChannel == 4)
			{
				format = GL_RGBA;
			}

			glTexImage2D(GL_TEXTURE_2D, 0, format, width, height, 0, format, GL_UNSIGNED_BYTE, data);
			glGenerateMipmap(GL_TEXTURE_2D);
		}

		else
		{
			printf("load image failed.");
		}
		stbi_image_free(data);
		return texBuffer;
	}
	
int main(int argc,char* argv[])
{
	std::string exePath = argv[0];
	//std::cout << exePath.substr(0,exePath.find_last_of("\\")) + "\\Model\\mesh_JadeToad.obj" << std::endl;

	#pragma region ��������
		glfwInit();
		glfwWindowHint(GLFW_CONTEXT_VERSION_MAJOR, 3);
		glfwWindowHint(GLFW_CONTEXT_VERSION_MINOR, 3);
		glfwWindowHint(GLFW_OPENGL_PROFILE, GLFW_OPENGL_CORE_PROFILE);

		//Open GLFW Window
		GLFWwindow* window = glfwCreateWindow(800, 600, "My OpenGL Game", NULL, NULL);
		if (window == NULL)
		{
			printf("Open Window Failed");
			glfwTerminate();
			return -1;
		}
		glfwMakeContextCurrent(window);

		glfwSetCursorPosCallback(window, mouse_callback);//������Ļص�
		glfwSetMouseButtonCallback(window, mouse_button_callback);
		// Init GLEW
		glewExperimental = true;
		if (glewInit() != GLEW_OK)
		{
			printf("Inti GLEW failed.");
			glfwTerminate();
			return -1;
		}
		glViewport(0,0,800,600);
		//glEnable(GL_CULL_FACE);//�����޳���
		//glDisable(GL_CULL_FACE);//�ر��޳���
		//glCullFace(GL_BACK);//�����޳�
		glEnable(GL_DEPTH_TEST);//������Ȳ��� --------------------------------
	#pragma endregion

	#pragma region ����ģ��

	Model model(exePath.substr(0, exePath.find_last_of("\\")) + "\\model\\nanosuit.obj");
	
	#pragma endregion

	#pragma region ������ɫ��

		Shader* shader = new Shader("vertexSource.vert", "fragmentSource.frag");//д��Shader

	#pragma endregion

	#pragma region ���ز���
				Material* material = new Material(shader,
					LoadingTexture2D("container2.png", shader->DIFFUSE),
					LoadingTexture2D("container2_specular.png", shader->SPECULAR)
					, glm::vec3(0.3f, 0.3f, 0.3f));
		#pragma endregion

	#pragma region ����MVP����Model,View,Projection��
					glm::mat4 modelMat = glm::mat4(1.0f);
					glm::mat4 viewMat = glm::mat4(1.0f);
					glm::mat4 projMat = glm::mat4(1.0f);

					projMat = glm::perspective(glm::radians(45.0f), 800.0f / 600.0f, 0.1f, 100.0f);
	#pragma endregion
	
	//��Ⱦѭ��
	while (!glfwWindowShouldClose(window))
	{
		processInput(window);//������������

		 //�����Ļ
		glClearColor(0.0f, 0.3f,0.2f, 1.0f);//���������Ļ�����ɫ
		glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);//�����ɫ����������Ȼ�������������Ļ���ÿһ֡����һ�� ����һ֡�Ļ���

		//����View����
		viewMat = camera.GetViewMatrix();
		//forѭ�����cube
		for (int i = 0; i < 1; i++)
		{
			//����ģ�;���
			modelMat = glm::mat4(1.0f);
			//�����ӽǾ���
			//-------------
			//����ͶӰ����
			//--------------
			//���ò����� -> ��ɫ��
			shader->ShaderProgram();//ʹ��Shader Program
			//���ò���   -> ����

			//���ò����� -> Texture
			//glActiveTexture(GL_TEXTURE0);
			//glBindTexture(GL_TEXTURE_2D, material->diffuseTexture);
			//glActiveTexture(GL_TEXTURE0 + 1 );
			//glBindTexture(GL_TEXTURE_2D, material->specularTexture);

			//���ò����� -> Uniform
			glUniformMatrix4fv(glGetUniformLocation(shader->ID, "modelMat"), 1, GL_FALSE, glm::value_ptr(modelMat));//�����󴫽�shader�� �����Ϊ��transform
			glUniformMatrix4fv(glGetUniformLocation(shader->ID, "viewMat"), 1, GL_FALSE, glm::value_ptr(viewMat));//�����󴫽�shader�� �����Ϊ��transform
			glUniformMatrix4fv(glGetUniformLocation(shader->ID, "projMat"), 1, GL_FALSE, glm::value_ptr(projMat));//�����󴫽�shader�� �����Ϊ��transform

			glUniform3f(glGetUniformLocation(shader->ID, "lightColor"), directionalLight.color.r, directionalLight.color.g, directionalLight.color.b);
			glUniform3f(glGetUniformLocation(shader->ID, "lightPos"), pointLight.position.x, pointLight.position.y, pointLight.position.z);
			glUniform3f(glGetUniformLocation(shader->ID, "lightDir"), directionalLight.direction.x, directionalLight.direction.y, directionalLight.direction.z);
			glUniform3f(glGetUniformLocation(shader->ID, "cameraPos"), camera.Position.x, camera.Position.y, camera.Position.z);
			glUniform4f(glGetUniformLocation(shader->ID, "_Time"), glfwGetTime() * 0.02, glfwGetTime(), glfwGetTime() * 2, glfwGetTime() * 20);

			material->shader->setUniform1i("material.diffuseTexture", material->shader->DIFFUSE);
			material->shader->setUniform1i("material.specularTexture",material->shader->SPECULAR);
			material->shader->setUniform3f("material.ambientColor", material->albedo);
			
			////����ģ��
			//glBindVertexArray(VAO);
			//
			////Drawcall
			//glDrawArrays(GL_TRIANGLES, 0, 36);

			//Drawcall
			model.Draw(material->shader);
		}
		

	//glPolygonMode(GL_FRONT_AND_BACK, GL_LINE);//�����߿�
	//	glPolygonMode(GL_FRONT_AND_BACK, GL_FILL);//������ģʽ

		//���� ׼����һ����Ⱦ
		glfwSwapBuffers(window);
		glfwPollEvents();
		camera.UpdataCameraPos();
	}
	//�˳�Pragam
	glfwTerminate();
	return 0;
}

