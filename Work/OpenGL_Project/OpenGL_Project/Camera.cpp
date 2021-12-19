#include "Camera.h"



Camera::Camera(glm::vec3 position, glm::vec3 target, glm::vec3 worldup)
{
	Position = position;
	Forward = glm::normalize(target - position);
	WorldUp = worldup;
	Right = glm::normalize(glm::cross(Forward, WorldUp));
	Up = glm::cross(Forward, Right);
}

Camera::Camera(glm::vec3 position, float pitch, float yaw, glm::vec3 worldup)
{
	Position = position;
	WorldUp = worldup;
	Pitch = pitch;
	Yaw = yaw;

	Forward.x = glm::cos(Pitch) * glm::sin(Yaw);
	Forward.y = glm::sin(Pitch);
	Forward.z = glm::cos(Pitch) * glm::cos(Yaw);
	Right = glm::normalize(glm::cross(Forward, WorldUp));
	Up = glm::cross(Forward, Right);
}

glm::mat4 Camera::GetViewMatrix()
{
	return glm::lookAt(Position, Position + Forward, WorldUp);
}

void Camera::UpdataCameraVectors()
{
	Forward.x = glm::cos(Pitch) * glm::sin(Yaw);
	Forward.y = glm::sin(Pitch);		  
	Forward.z = glm::cos(Pitch) * glm::cos(Yaw);
	Right = glm::normalize(glm::cross(Forward, WorldUp));
	Up = glm::cross(Forward, Right);
}

void Camera::ProcessMouseMovement(float deltaX, float deltaY)
{
	Pitch -= deltaY * SenseX;
	Yaw -= deltaX * SenseY;
	UpdataCameraVectors();
}
void Camera::UpdataCameraPos()
{
	glm::vec3 Move = Forward * glm::vec3(MoveX, MoveY, -MoveZ);
	Position -= Forward * MoveZ * 0.1f;
	Position += Right * MoveX * 0.1f;
	Position -= Up * MoveY * 0.1f;
}