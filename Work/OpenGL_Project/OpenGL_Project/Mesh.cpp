#include "Mesh.h"

Mesh::Mesh(float vertices[])
{
	this->vertices.resize(36);
	memcpy(&(this->vertices[0]),vertices,36*8*sizeof(float));

	setupMesh();
}

Mesh::Mesh(std::vector<Vertex> vertices, std::vector<unsigned int> indices, std::vector<Texture> textures)
{
	this->vertices = vertices;
	this->indices = indices;
	this->textures = textures;

	setupMesh();

}

void Mesh::Draw(Shader* shader)
{
	for (unsigned int i = 0; i < textures.size(); i++)
	{
		if (textures[i].type =="texture_diffuse")
		{
			glActiveTexture(GL_TEXTURE0);
			glBindTexture(GL_TEXTURE_2D, textures[i].id);
			shader->setUniform1i("material.diffuseTexture", 0);
		}
		else if (textures[i].type == "texture_specular")
		{
			glActiveTexture(GL_TEXTURE0);
			glBindTexture(GL_TEXTURE_2D,textures[i].id);
			shader->setUniform1i("material.specularTexture", 1);
		}
	}
	glBindVertexArray(VAO);
	//glDrawArrays(GL_TRIANGLES, 0, 36);
	glDrawElements(GL_TRIANGLES, indices.size(), GL_UNSIGNED_INT, 0);
	glBindVertexArray(0);
	glActiveTexture(GL_TEXTURE0);
} 

void Mesh::setupMesh()
{
	glGenVertexArrays(1, &VAO);
	glBindVertexArray(VAO);

	glGenBuffers(1, &VBO);
	glBindBuffer(GL_ARRAY_BUFFER, VBO);
	glBufferData(GL_ARRAY_BUFFER, sizeof(Vertex) * vertices.size(), &vertices[0], GL_STATIC_DRAW);

	glGenBuffers(1, &EBO);
	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, EBO);
	glBufferData(GL_ELEMENT_ARRAY_BUFFER, sizeof(unsigned int) *indices.size(), &indices[0], GL_STATIC_DRAW);
	
	//顶点
	glEnableVertexAttribArray(0);
	glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, sizeof(Vertex), (void*)0);
	//法线
	glEnableVertexAttribArray(1);
	glVertexAttribPointer(1, 3, GL_FLOAT, GL_FALSE, sizeof(Vertex), (void*)(3 * sizeof(float)));
	//UV
	glEnableVertexAttribArray(2);
	glVertexAttribPointer(2, 2, GL_FLOAT, GL_FALSE, sizeof(Vertex), (void*)(6 * sizeof(float)));
	//解绑
	glBindVertexArray(0);
}
