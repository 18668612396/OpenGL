#include "Model.h"


Model::Model(std::string path)
{
	loadModel(path);
}

void Model::loadModel(std::string path)
{
	Assimp::Importer importer;
	const aiScene* scene = importer.ReadFile(path,aiProcess_Triangulate | aiProcess_FlipUVs | aiProcess_CalcTangentSpace);

	if (!scene || scene->mFlags & AI_SCENE_FLAGS_INCOMPLETE || !scene->mRootNode)
	{
		std::cout << "Assimp error" << std::endl;
		return;
	}
	directory = path.substr(0, path.find_last_of("\\"));
		//std::cout << "Success!" << directory << std::endl;
	processNode(scene->mRootNode,scene);
}

void Model::processNode(aiNode * node, const aiScene * scene)
{


	for (unsigned int i = 0; i < node->mNumMeshes; i++)
	{
		aiMesh* mesh = scene->mMeshes[node->mMeshes[i]];

		meshes.push_back(processMesh(mesh, scene));;//push Mesh
		std::cout << mesh->mName.data << std::endl;
	}
	//std::cout << node->mName.data << std::endl;
	for (unsigned int i = 0; i < node->mNumChildren; i++)
	{

		processNode(node->mChildren[i], scene);
	}
	
}

Mesh Model::processMesh(aiMesh* mesh, const aiScene* scene)
{
	std::vector<Vertex> vertices;
	std::vector<unsigned int> indices;
	std::vector<Texture> textures;

	for (unsigned int i = 0; i < mesh->mNumVertices; i++)
	{

		Vertex vertex;
		vertex.Position.x = mesh->mVertices[i].x;
		vertex.Position.y = mesh->mVertices[i].y;
		vertex.Position.z = mesh->mVertices[i].z;


		vertex.Normal.x = mesh->mNormals[i].x;
		vertex.Normal.y = mesh->mNormals[i].y;
		vertex.Normal.z = mesh->mNormals[i].z;


		if (mesh->mTextureCoords[0])
		{
			vertex.TexCoords.x = mesh->mTextureCoords[0][i].x;
			vertex.TexCoords.y = mesh->mTextureCoords[0][i].y;
		}
		else
			vertex.TexCoords = glm::vec2(0.0f, 0.0f);

		vertices.push_back(vertex);
	}
	for (unsigned int i = 0; i < mesh->mNumFaces; i++)
		{

			for (unsigned int j = 0; j < mesh->mFaces[i].mNumIndices; j++)
			{
				indices.push_back(mesh->mFaces[i].mIndices[j]);
			}
	
	}

	

	return Mesh(vertices, indices, {});
}

void Model::Draw(Shader* shader)
{
	for (unsigned int i = 0; i < meshes.size(); i++)
	{
		meshes[i].Draw(shader);
	}
}
