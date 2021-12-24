using System.Collections.Generic;
using System.Xml.Serialization;
using Assimp;
using Assimp.Unmanaged;


namespace Silk_OpenGL
{
    public class Model
    {
        public Model(string path)
        {
            List<Mesh> meshes;

            loadModel(path);
        }

        private void loadModel(string path)
        {
    
            
        }
    }
}