using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using DungeonCrawl.Engine.LevelObjects;

namespace DungeonCrawl.Engine.Characters
{
    class AiManager
    {

        private float minDistance = 140;
        private float maxDistance = 800;

        Director director;
        Character character;


        Finder finder = new Finder();


        bool canSee = false;
        int timer = 0;
        int limit = 400;


        public AiManager(Director directorT , Character characterT)
        {
            director = directorT;
            character = characterT;

            finder.isIdle = true;

        }


        public void update()
        {
            timer--;
            if (canSee == true)
            {

                if (timer < 0)
                {
                    canSee = hasLineOfSight();
                    timer = 400;
                }
                else
                {
                    if (hasLineOfSight() == false)
                    {
                        timer -= 10;
                    }
                }
            }
            else
            {
                if (distanceToCharacter() < maxDistance)
                {
                    canSee = hasLineOfSight();
                }
            }




            if ( distanceToCharacter() < maxDistance)
            {
                if (canSee)
                {
                    //Close In
                    character.pointTowardsPoint(director.getPlayer().getPosition());
                    character.moveForward();

                    if (distanceToCharacter() < minDistance)
                    {
                        //Attack
                        character.attack();
                    }
                }
            }
            
        }



        private bool hasLineOfSight()
        {
            bool canSee = false;


            if (finder.isIdle)
            {
                finder.send(character.getPosition() , getAngleToPlayer());

            }
            else
            {
                finder.update();

                if (MainData.developerMode)
                {
                    director.getEffectManager().addFog(finder.finderPosition, 1, Color.DarkRed, true);
                }

                    
                if(finder.hitBox.Intersects(director.getPlayer().getHitBox()))
                {
                    finder.isIdle = true;
                    return true;
                }


                foreach(Wall w in director.getGrid().getWalls())
                {
                    if(finder.hitBox.Intersects(w.getHitBox()))
                    {
                        finder.isIdle = true;
                        return false;
                    }
                }

                

            }


            return canSee;
        }




        private float getAngleToPlayer()
        {
            float angle = 0;


            float XDistance = character.getPosition().X - director.getPlayer().getPosition().X;
            float YDistance = character.getPosition().Y - director.getPlayer().getPosition().Y;

                    //Calculate the required rotation by doing a two-variable arc-tan
                    angle = (float)Math.Atan2(YDistance, XDistance);
                    angle += MathHelper.ToRadians(90);
            


            return angle;
        }

        private float distanceToCharacter()
        {
            float distanceT;


            distanceT = Vector2.Distance(character.getPosition(), director.getPlayer().getPosition());

                return distanceT;
        }




    }


    #region Finder

    public struct Finder
    {
        public Vector2 finderPosition;
        public float rotation;
        public float speed;
        public Rectangle hitBox;
        public bool isIdle;


        public void send(Vector2 p, float angle)
        {
            isIdle = false;

            finderPosition = p;
            rotation = angle;
            speed = 50;

            hitBox.Width = 32;
            hitBox.Height = 32;
        }


        public void update()
        {
            if (this.isIdle == false)
            {
                move();
            }
        }

        private void move()
        {
            Vector2 direction;
            Vector2 up = new Vector2(0, -1);

            Matrix rotMatrix = Matrix.CreateRotationZ(rotation);

            direction = Vector2.Transform(up, rotMatrix);
            //   direction *= 400 / 50.0f;
            direction *= 50;

            finderPosition += -direction;

            hitBox.X = (int)finderPosition.X;
            hitBox.Y = (int)finderPosition.Y;

        }
    }


    #endregion



}
