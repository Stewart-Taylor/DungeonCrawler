using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DungeonCrawl.Menu.Screen_Manager;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using DungeonCrawl.Engine.Characters;
using DungeonCrawl.Engine.LevelObjects;

namespace DungeonCrawl.Engine
{
    class InputManager
    {
        Director director;
        InputHelper input;

        public  InputManager(InputHelper inputT ,Director directorT)
        {
            director = directorT;
            input = inputT;
        }

        public void update()
        {
            playerInput();

            if (MainData.developerMode == true)
            {
                testInput();
            }

            updateCamera();
        }

        private void playerInput()
        {
            if(MainData.xboxMode == false)
            {
                pcInput();
            }
            else
            {
                xboxInput();
            }
        }

        private void xboxInput()
        {
            if (input.IsCurPress(Buttons.Y))
            {
                director.player.useAction();
            }

            if (input.IsCurPress(Buttons.A))
            {
                director.getPlayer().chargeAttack();
            }
            else if (input.IsOldPress(Buttons.A))
            {
                director.getPlayer().attack();
            }

            if (input.IsCurPress(Buttons.LeftTrigger))
            {
                director.player.defend();

                if (input.IsNewPress(Buttons.A))
                {
                    director.player.defendPush();
                }
            }

            if (input.IsNewPress(Buttons.RightTrigger))
            {
                director.getPlayer().getSpellManager().castFireBall(director.getPlayer());
            }

            float rotation = LeftThumbStickRotation();

            if (rotation != -9999)
            {
                director.player.setRotation(LeftThumbStickRotation());
                director.player.moveForward();

                if (input.IsCurPress(Buttons.LeftStick))
                {
                    director.getPlayer().sprint();
                }
            }
        }


        public float LeftThumbStickRotation() 
        {
            float rotation = -9999;

            Vector2 left = GamePad.GetState(PlayerIndex.One,GamePadDeadZone.Circular).ThumbSticks.Left;
            if ((left.X != 0.0f) & (left.Y != 0.0f)) rotation = (float)(double)Math.Atan2((double)left.Y * -1, (double)left.X);

            return rotation;
        } 


        private void pcInput()
        {
            if (input.IsCurPress(Keys.W))
            {
                director.player.moveForward();
            }

            if (input.IsCurPress(Keys.LeftAlt))
            {
                if (input.IsCurPress(Keys.W))
                {
                    director.getPlayer().sprint();
                }
            }

            if (input.IsCurPress(Keys.S))
            {
                director.player.moveBackward();
            }

            if (input.IsCurPress(Keys.Space))
            {
                director.player.useAction();
            }

            if (input.IsCurPress(MouseButtons.LeftButton))
            {
                director.getPlayer().chargeAttack();
            }
            else if (input.IsOldPress(MouseButtons.LeftButton))
            {
                director.getPlayer().attack();
            }

            if (input.IsCurPress(MouseButtons.RightButton))
            {
                director.player.defend();

                if (input.IsNewPress(MouseButtons.LeftButton))
                {
                    director.player.defendPush();
                }
            }

            if (input.IsNewPress(Keys.D))
            {
                director.getPlayer().getSpellManager().castFireBall(director.getPlayer());
            }

            director.player.pointTowardsPoint(new Vector2(input.MousePosition.X + director.camera.Pos.X - (500), input.MousePosition.Y + +director.camera.Pos.Y - (350)));
        }

        private void testInput()
        {
            if (input.IsNewPress(Keys.R))
            {
                director.generateLevel();
            }

            if (input.IsNewPress(Keys.A))
            {
                director.getPlayer().teleport(new Vector2(input.MousePosition.X + director.camera.Pos.X - (500), input.MousePosition.Y + +director.camera.Pos.Y - (350)));
            }

            if (input.IsNewPress(Keys.T))
            {
                director.test();
            }

            if (input.IsCurPress(Keys.Z))
            {
                director.getEffectManager().addTorchFlame(new Vector2(input.MousePosition.X + director.camera.Pos.X - (500), input.MousePosition.Y + +director.camera.Pos.Y - (350)), 1 );
            }

            if (input.IsNewPress(Keys.H))
            {
                Wraith skel = new Wraith(director, Vector2.Zero);
                skel.setPosition(new Vector2(input.MousePosition.X + director.camera.Pos.X - (500), input.MousePosition.Y + +director.camera.Pos.Y - (350)));
                director.addCharacter(skel);
            }

            if (input.IsNewPress(Keys.C))
            {
                Chest lo = new Chest(director, new Vector2(input.MousePosition.X + director.camera.Pos.X - (500), input.MousePosition.Y + +director.camera.Pos.Y - (350)));
                director.addLevelObject(lo);
            }
        }

        private void updateCamera()
        {
            if (MainData.developerMode)
            {

                if (input.IsCurPress(Keys.Up))
                {
                    CameraData.yPosition -= 30f;
                }
                if (input.IsCurPress(Keys.Down))
                {
                    CameraData.yPosition += 30f;
                }
                if (input.IsCurPress(Keys.Left))
                {
                    CameraData.xPosition -= 30f;
                }
                if (input.IsCurPress(Keys.Right))
                {
                    CameraData.xPosition += 30f;
                }
                if (input.IsCurPress(Keys.I))
                {
                    CameraData.rotation += 0.1f;
                }
                if (input.IsCurPress(Keys.K))
                {
                    CameraData.rotation -= 0.1f;
                }
                if (input.IsCurPress(Keys.Z))
                {
                    CameraData.zoom = 1;
                }
                if (input.IsCurPress(Keys.O))
                {
                    CameraData.zoom -= 0.01f;
                }
                if (input.IsCurPress(Keys.L))
                {
                    CameraData.zoom += 0.01f;
                }
                if (input.IsCurPress(Keys.X))
                {
                    CameraData.zoom = 0.3f;
                }

                if (input.IsNewPress(Keys.B))
                {
                    director.dynamicCamera = !director.dynamicCamera;
                }
            }


            if (director.dynamicCamera == false)
            {
                director.getCamera()._pos.X =  MathHelper.SmoothStep(director.getCamera().Pos.X, director.getPlayer().getPosition().X, 0.2f);
                director.getCamera()._pos.Y = MathHelper.SmoothStep(director.getCamera().Pos.Y, director.getPlayer().getPosition().Y, 0.2f);
                
                director.getCamera().Zoom = 1f;
                director.getCamera().Rotation = 0;

                CameraData.xPosition = director.getCamera()._pos.X;
                CameraData.yPosition = director.getCamera()._pos.Y;
            }
            else
            {
                director.getCamera().Pos = new Vector2(CameraData.xPosition, CameraData.yPosition);
                director.getCamera().Zoom = CameraData.zoom;
                director.getCamera().Rotation = CameraData.rotation;
            }
        }
    }
}
