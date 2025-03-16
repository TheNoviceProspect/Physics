﻿using Box2DSharp.Collision.Shapes;
using Box2DSharp.Common;
using Box2DSharp.Dynamics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;

namespace Physics.App
{
    internal class App : GameWindow
    {
        private World world;

        public struct CircleBody
        {
            public Body body;
            public float radius;

            public CircleBody(Body body, float radius)
            {
                this.body = body;
                this.radius = radius;
            }
        }

        public App(int width, int height, string title) : base(GameWindowSettings.Default, new NativeWindowSettings() { ClientSize = (width, height), Title = title })
        {
            //world = new World(new System.Numerics.Vector2(0, -10));
            // NO GRAVITY
            world = new World(new System.Numerics.Vector2(0, 0));

            Random random = new Random();

            // Create 5 circles
            for (int i = 0; i < 5; i++)
            {
                float x = (float)(random.NextDouble() * width) - width / 2 + 100;
                float y = (float)(random.NextDouble() * height) - height / 2 + 100;

                BodyDef bodyDef = new BodyDef();
                bodyDef.Position.Set(x / 64f, y / 64f); // Convert to meters

                CircleShape circleShape = new CircleShape() { Radius = 10f / 64f };
                FixtureDef fixtureDef = new FixtureDef();
                fixtureDef.Shape = circleShape;

                Body body = world.CreateBody(bodyDef);
                body.CreateFixture(fixtureDef);
                body.BodyType = BodyType.DynamicBody;

                // Store the bodies
                CircleBody circle = new CircleBody(body, 10f);
                Program._log.Debug($"[CREATE] New CircleBody[{i + 1}] at X:{circle.body.GetPosition().X} Y:{circle.body.GetPosition().Y} with r{circle.radius}");
            }
        }

        // ...

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.ClearColor(0.2f, 0.2f, 0.2f, 1.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            world.Step(1f / 60, 8, 3); // Update physics

            foreach (var body in world.BodyList)
            {
                if (!body.IsEnabled) continue;
                Vector2 pos = new Vector2(body.GetPosition().X * 64, body.GetPosition().Y * 64); // Convert back to pixels
                GL.Begin(PrimitiveType.Points);
                GL.Color3(1, 1, 1);
                GL.Vertex2(Vector2.Add(pos, new Vector2(10, 0))); // Draw circles using Points mode
                GL.End();
            }
            SwapBuffers(); // Swap the buffers to display the rendered frame
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }
        }

        protected override void OnKeyUp(KeyboardKeyEventArgs e)
        {
            base.OnKeyUp(e);
            Program._log.Info($"Key pressed: {e.Key}");
        }
    }
}