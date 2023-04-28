#version 330 core
//in vec4 vertexColor;

uniform float vertexColor2;
//in float vertexColor2;
out vec4 fragmentColor;

void main(){
	fragmentColor = vec4(0.5, vertexColor2, 0, 1);
}
