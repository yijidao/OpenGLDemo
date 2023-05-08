#version 330 core
// location 0 position
layout (location = 0) in vec3 aPos; 
// location 1 color
layout (location = 1) in vec3 aColor; 

out vec4 vertexColor;
uniform float xOffset;
uniform float yOffset;

void main(){
	gl_Position = vec4(aPos.x + xOffset, aPos.y + yOffset, aPos.z, 1.0);
//	vertexColor = vec4(aColor, 1.0);
	vertexColor = vec4(gl_Position);
}