cmake_minimum_required(VERSION 3.21)
project(OpenGL_Clion)

set(CMAKE_CXX_STANDARD 14)





#头文件
link_directories("E:/GitHub/OpenGL/Header/glew-2.1.0/include")
link_directories("E:/GitHub/OpenGL/Header/glfw-3.3.6.bin.WIN32/include")






add_executable(OpenGL_Clion main.cpp)


#target_link_libraries(OpenGL_Clion -lglfw -lGLEW)
#target_link_libraries(libfreeglut.a glew32.lib)






