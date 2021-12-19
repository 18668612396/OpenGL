#ifndef CAMERA_H
 #define CAMERA_H

#include <glm/glm.hpp>
#include <glm/gtc/matrix_transform.hpp>

class Camera
{
public:
	Camera(glm::vec3 position, glm::vec3 target, glm::vec3 worldup);
	Camera(glm::vec3 position, float pitch,float yaw, glm::vec3 worldup);
	glm::vec3 Position;
	glm::vec3 Forward;
	glm::vec3 Right;
	glm::vec3 Up;
	glm::vec3 WorldUp;
	float Pitch;
	float Yaw;
	float SenseX = 0.005f;//视角X旋转速率
	float SenseY = 0.005f;//视角Y旋转速率

	float MoveX = 0;//视角左右移动速率
	float MoveY = 0;//视角上下移动速率
	float MoveZ = 0;//视角前后移动速率
	
	
	glm::mat4 GetViewMatrix();

	void ProcessMouseMovement(float deltaX,float deltaY);
	void UpdataCameraPos();
private:
	void UpdataCameraVectors();
};
#endif // !CAMERA_H