﻿using System;
using Silk.NET.OpenGL;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime;
using Silk.NET.Input;
using Silk.NET.Windowing;


namespace Silk_OpenGL
{
    public static class Camera
    {
        private static Vector3 Camera_Position = Vector3.Zero;
        private static Vector3 Camera_Direction;
        private static Vector3 Camera_Right;
        private static Vector3 Camera_Up;
        private static Vector2 EulerAngles;
        private static Vector3 Camera_Move;
        public static float FieldOfView = 45.0f;
        public static Vector2 ClippingPlanes = new Vector2(0.1f, 100.0f);
        public static Matrix4x4 GetViewMatrix;
        public static void UpdataCamera(GL Gl, IWindow window)
        {
            // Camera_Position = new Vector3(0.0f, 0.0f, -3.0f);
            Camera_Direction.X = MathF.Cos(Radians(EulerAngles.X)) * MathF.Cos(Radians(EulerAngles.Y));
            Camera_Direction.Y = MathF.Sin(Radians(EulerAngles.X));
            Camera_Direction.Z = MathF.Cos(Radians(EulerAngles.X)) * MathF.Sin(Radians(EulerAngles.Y));
            Camera_Direction = Vector3.Normalize(Camera_Direction);//
            Camera_Right = Vector3.Normalize(Vector3.Cross(Camera_Direction, new Vector3(0.0f, 1.0f, 0.0f)));
            Camera_Up = Vector3.Normalize(Vector3.Cross(Camera_Right,Camera_Direction));

            GetViewMatrix = Matrix4x4.CreateLookAt(Camera_Position, Camera_Position + Camera_Direction, new Vector3(0.0f, 1.0f, 0.0f));
            Gl.Uniform3(Gl.GetUniformLocation(Shader.program,"_WorldSpaceCameraPos"),Camera_Position);
            
            UpdataCameraTransform(window);
        }

        private static void UpdataCameraTransform(IWindow window)
        {
            IInputContext input = window.CreateInput();
            IKeyboard keyboard = input.Keyboards.FirstOrDefault();

            for (int i = 0; i < input.Mice.Count; i++)
            {
                input.Mice[0].Cursor.CursorMode = CursorMode.Raw;
                input.Mice[0].MouseMove += OnMouseMove;
            }

            OnKeyboardDown(keyboard);

            Camera_Position += Camera_Direction * Camera_Move.Z;
            Camera_Position += Camera_Right * Camera_Move.X;
            Camera_Position += Camera_Up * Camera_Move.Y;
        }

        private static Vector2 LastMousePosition;
        private static unsafe void OnMouseMove(IMouse mouse, Vector2 position)
        {
            var lookSensitivity = 0.1f;
            if (LastMousePosition == default) { LastMousePosition = position; }
            else
            {
                var xOffset = (position.X - LastMousePosition.X) * lookSensitivity;
                var yOffset = (position.Y - LastMousePosition.Y) * lookSensitivity;
                LastMousePosition = position;

                EulerAngles.Y += xOffset;
                EulerAngles.X -= yOffset;
                //We don't want to be able to look behind us by going over our head or under our feet so make sure it stays within these bounds
                EulerAngles.X = Math.Clamp(EulerAngles.X, -89.0f, 89.0f);
            }
        }
        private static float CameraSpeed = 0.05f;
        private static void OnKeyboardDown(IKeyboard keyboard)
        {

            if (keyboard.IsKeyPressed(Key.W))
            {
                Camera_Move.Z = CameraSpeed;
            }
            else if (keyboard.IsKeyPressed(Key.S))
            {
                Camera_Move.Z = -CameraSpeed;
            }
            else
            {
                Camera_Move.Z = 0;
            }
            
            if (keyboard.IsKeyPressed(Key.D))
            {
                Camera_Move.X = CameraSpeed;
            }
            else if (keyboard.IsKeyPressed(Key.A))
            {
                Camera_Move.X = -CameraSpeed;
            }
            else
            {
                Camera_Move.X = 0;
            }
            
            if (keyboard.IsKeyPressed(Key.E))
            {
                Camera_Move.Y = CameraSpeed;
            }
            else if (keyboard.IsKeyPressed(Key.Q))
            {
                Camera_Move.Y = -CameraSpeed;
            }
            else
            {
                Camera_Move.Y = 0;
            }
            
        }
        private static float Radians(float value)
        {
            return MathF.PI / 180 * value;
        }
    }
}