#include <iostream>
#define GLEW_STATIC
#include <GL/glew.h>
#include <GLFW/glfw3.h>

int main()
{
	glfwInit();
	glfwWindowHint(GLFW_CONTEXT_VERSION_MAJOR, 3);
	glfwWindowHint(GLFW_CONTEXT_VERSION_MAJOR, 3);
	glfwWindowHint(GLFW_OPENGL_PROFILE, GLFW_OPENGL_PROFILE);

	//Open GLFW Window
	GLFWwindow* window = glfwCreateWindow(800, 600, "My OpenGL Game", NULL, NULL);
	if (window == NULL)
	{
		printf("Open Window Failed");
		glfwTerminate();
		return -1;
	}
	glfwMakeContextCurrent(window);

	// Init GLEW
	glewExperimental = true;
	if (glfwInit() != GLEW_OK)
	{
		printf("Inti GLEW failed.");
		glfwTerminate();
		return -1;
	}
	glViewport(0,0,800,600);
	
	while (!glfwWindowShouldClose(window))
	{
		glfwSwapBuffers(window);
		glfwWaitEvents();
	}
	glfwTerminate();
	return 0;
}