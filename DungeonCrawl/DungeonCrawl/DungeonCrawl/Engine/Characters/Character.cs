using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using DungeonCrawl.Engine.LevelObjects;

namespace DungeonCrawl.Engine.Characters
{
    class Character
    {


        #region FIELDS

        protected Vector2 position;
        protected Vector2 origin = new Vector2(64, 64);
        protected Rectangle hitBox;
        protected Texture2D texture;
        protected Director director;
        protected float rotation;


  
        protected bool isDead = false;

        protected bool moving = false;

        //MOVEMENT
        


        //STATS
        protected int strength = 5;
        protected int health = 10;
        protected float speed = 3.5f;


        protected int damageCounter;
        protected int damageLimit = 10;



        int stuckTimer = 0;


        #endregion







        #region SETS



        public void setPosition(Vector2 posT)
        {
            position = posT;
         

        }


        public void setRotation(float rot)
        {
            rotation = rot;

        }


        #endregion






        #region GETS




        public Vector2 getPosition()
        {
            return position;
        }

        public float getRotation()
        {
            return rotation;
        }

        public float getSpeed()
        {
            return speed;
        }

        public Rectangle getHitBox()
        {
            return hitBox;
        }


        public int getHealth()
        {
            return health;
        }

        public bool isAlive()
        {
            return !isDead;
        }



        #endregion





        #region commands


        public virtual void teleport(Vector2 positionT)
        {
            director.getEffectManager().addFog(position, 40, Color.White, true);
            position = positionT;

            director.getEffectManager().addFog(positionT, 40, Color.White, true);


        }

        public virtual void damage(int damageAmount)
        {
            MainData.soundBank.PlayCue("bodyHit");
            if (damageCounter >= damageLimit)
            {
                damageCounter = 0;

                health -= damageAmount;

                if (health < 0)
                {
                    MainData.soundBank.PlayCue("Death");
                    isDead = true;
                    health = 0;
                }


                knockBack();
            }
        }



        protected virtual void knockBack()
        {



        }


        public void moveForward()
        {

            moving = true;

            Vector2 velocity;
            velocity.X = (float)Math.Cos(rotation) * speed;
            velocity.Y = (float)Math.Sin(rotation) * speed;


           

            if (isMoveValid(position + velocity) == false)
            {
                


                if (velocity.X > 0) // object came from the left
                    velocity.X = -0.3f;
                   
                else if (velocity.X < 0) // object came from the right
                {
                    velocity.X = 0.3f;
          
                }
                if (velocity.Y > 0) // object came from the top
                    velocity.Y = -0.3f;
                 
                else if (velocity.Y < 0) // object came from the bottom
                    velocity.Y = 0.3f;
                  


                position += velocity;

                MainData.soundBank.PlayCue("Walk");

            
                
            }
            else
            {
                MainData.soundBank.PlayCue("Walk");
                    position += velocity;
               
            }
        }



        public virtual void defend()
        {


        }

        public virtual void attack()
        {



        }



        protected Wall getCollideWall(Vector2 tPosition)
        {

            foreach (Wall w in director.grid.getWalls())
            {
                if (w.IsColliding(hitBox))
                {
                    return w;
                }

            }
            return null;
        }




        protected bool isMoveValid(Vector2 tPosition)
        {
            Rectangle tempHitBox = new Rectangle((int)tPosition.X -50, (int)tPosition.Y -50, hitBox.Width, hitBox.Height);

            foreach (Wall w in director.grid.getWalls())
            {
                if (w.IsColliding(tempHitBox))
                {
                    return false;
                }
            }



            foreach (LevelObject lObject in director.getLevelObjects())
            {
                if (lObject.canWalkThrough() == false)
                {
                    if (lObject.getHitBox().Intersects(tempHitBox))
                    {
                        return false;
                    }
                }
            }

            foreach (Character c in director.getCharacters())
            {
                if( !(c.Equals(this)))
                {
                    if (c.isAlive() == true)
                    {
                        if (c.getHitBox().Intersects(tempHitBox))
                        {
                            return false;
                        }
                    }
                }
            }


            if( !(this is Player))
            {
                if (director.getPlayer().getHitBox().Intersects(tempHitBox))
                {
                    return false;
                }
            }


            return true;
        }



        public void moveBackward()
        {

            moving = true;

            Vector2 velocity;
            velocity.X = -(float)Math.Cos(rotation) * speed;
            velocity.Y = -(float)Math.Sin(rotation) * speed;




            if (isMoveValid(position + velocity) == false)
            {



                if (velocity.X > 0) // object came from the left
                    velocity.X = -0.3f;

                else if (velocity.X < 0) // object came from the right
                {
                    velocity.X = 0.3f;

                }
                if (velocity.Y > 0) // object came from the top
                    velocity.Y = -0.3f;

                else if (velocity.Y < 0) // object came from the bottom
                    velocity.Y = 0.3f;



                position += velocity;



            }
            else
            {
                position += velocity;

            }


        }



        public void pointTowardsPoint(Vector2 point)
        {


            float XDistance = position.X - point.X;
            float YDistance = position.Y - point.Y;


            if (Math.Abs(XDistance) > 1f)
            {
                if (Math.Abs(YDistance) > 1f)
                {

                    //Calculate the required rotation by doing a two-variable arc-tan
                     rotation = (float)Math.Atan2(YDistance, XDistance);

                    rotation += MathHelper.ToRadians(180);

                    

                }
            }
        }






        #endregion




 


        public Character(Director d, Vector2 pos)
        {
            director = d;


            setUpPosition(pos);

        }

        protected void setUpPosition(Vector2 pos)
        {
            position = pos;
            position.X += 64;
            position.Y += 64;

            rotation = (float)Director.random.Next(300);

        }



        public virtual void load(ContentManager content)
        {

            texture = content.Load<Texture2D>("Characters//CharTest");


            

            setUpHitBox();
            stuckCheck();
        }


        protected void setUpHitBox()
        {
            //set up hitbox
            hitBox = new Rectangle((int)position.X + 64, (int)position.Y + 64, 96, 96);
        }


        public virtual void update()
        {



            mainUpdate();


            moving = true;
        }


        protected void mainUpdate()
        {
            updateHitBox();
            stuckCheck();

            if (damageCounter < damageLimit)
            {
                damageCounter++;
            }

        }



        private void stuckCheck()
        {
           

            stuckTimer--;
            if (stuckTimer < 0)
            {
                stuckTimer = 200;
                //stuck in wall
                foreach (Wall w in director.getGrid().getWalls())
                {
       
                        if (w.getHitBox().Intersects(hitBox))
                        {
                          
                            teleport(getClearPosition());
                            break;
                        }
                    
                }





            }

        }


        private Vector2 getClearPosition()
        {
            Vector2 clearPosition = new Vector2(0, 0);


            Tile closeTile = null;
            float distance = 99999;

            foreach (Tile t in director.getGrid().getTiles())
            {
                if (t.isWalkable())
                {
                    if(Camera.onScreen(t.getPosition()))
                    {

                        if( Vector2.Distance(position , t.getPosition()) < distance)
                        {
                            distance = Vector2.Distance(position , t.getPosition());
                            closeTile = t;
                        }
                    }

                }

            }

            clearPosition = closeTile.getPosition();
            clearPosition.X += 64;
            clearPosition.Y += 64;

            return clearPosition;
        }

        private void updateHitBox()
        {
            hitBox.X = (int)position.X - 50;
            hitBox.Y = (int)position.Y - 50;
        }




        protected void deathCheck()
        {
            if (health <= 0)
            {

                isDead = true;
            }

        }


        public virtual void draw(SpriteBatch sb)
        {


            sb.Draw(texture, position, null, Color.White, rotation, origin, 1, SpriteEffects.None, 1);



           // TextManager.drawTextBorder(director.font, sb, "Current Speed: " + currentSpeed, Color.Black, Color.White, 1, 0, new Vector2(position.X -100, position.Y - 100));

            drawHitBox(sb);
        }



        protected  void drawHitBox(SpriteBatch sb)
        {


            sb.Draw(texture, new Vector2(hitBox.X, hitBox.Y), null, Color.Gold, 0, new Vector2(0, 0), 0.1f, SpriteEffects.None, 1);
            sb.Draw(texture, new Vector2(hitBox.X + hitBox.Width, hitBox.Y), null, Color.Gold, 0, new Vector2(0, 0), 0.1f, SpriteEffects.None, 1);
            sb.Draw(texture, new Vector2(hitBox.X, hitBox.Y + hitBox.Height), null, Color.Gold, 0, new Vector2(0, 0), 0.1f, SpriteEffects.None, 1);
            sb.Draw(texture, new Vector2(hitBox.X + hitBox.Width, hitBox.Y + hitBox.Height), null, Color.Gold, 0, new Vector2(0, 0), 0.1f, SpriteEffects.None, 1);


        }


    }
}