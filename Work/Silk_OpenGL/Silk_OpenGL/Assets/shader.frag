#version 330 core
out vec4 FragColor;
in vec4 vertexColor;
in vec2 uv;
uniform sampler2D diffuse;
uniform sampler2D specular;
uniform float test;
void main()
{
    vec4 var_diffuse = texture(diffuse, uv);
    vec4 var_specular = texture(specular, uv);
    vec3 finalColor = mix(var_diffuse.rgb,var_specular.rgb,0.2);
    FragColor = vec4(finalColor.rgb, uv);
//    FragColor = vec4(test,test,test,1);
}