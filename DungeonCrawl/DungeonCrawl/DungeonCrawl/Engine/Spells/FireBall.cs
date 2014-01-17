using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using DungeonCrawl.Engine.Characters;
using DungeonCrawl.Engine.LevelObjects;

namespace DungeonCrawl.Engine.Spells
{
    class FireBall 
    {


        Color color = Color.White;

        float rotation;
        Vector2 trailPoint;

        Texture2D texture;

        Vector2 position;
        Director director;
        Rectangle hitBox;

        float speed = 20f;

        float angle;

        bool isDead = false;

        public const int   cost =60;




        public FireBall(Director d, Vector2 p , float rot)
        {
            rotation = rot;


            position = p;
            

            director = d;



            director.getEffectManager().addFog(position, 10, Color.CornflowerBlue , true);
            

            

        }



        public  void load(ContentManager content)
        {
            texture = content.Load<Texture2D>("Spells//FireBall");

           // position.X -= texture.Width / 4;
            //position.Y -= texture.Height / 4;

            hitBox.X = (int)position.X - 32;
            hitBox.Y = (int)position.Y - 32;
            hitBox.Width = texture.Width;
            hitBox.Height = texture.Height;
        }


        public  void update()
        {
            if (isDead == false)
            {
                moveUpdate();
                checkCollision();


                director.getEffectManager().addFog(position, 1, Color.Red, true);

                angle += 0.4f;


                director.getEffectManager().addFlame(position , 10);

            
            }
        }



 


        private void moveUpdate()
        {
            


            Vector2 velocity;
            velocity.X = (float)Math.Cos(rotation) * speed;
            velocity.Y = (float)Math.Sin(rotation) * speed;

            position += velocity;

            hitBox.X = (int)position.X - 32;
            hitBox.Y = (int)position.Y - 32;



        }



  

        private void explode(Vector2 positionT)
        {
            director.getEffectManager().addFlame(positionT, 40, rotation , -speed / 5);
            director.getEffectManager().addFlame(positionT, 40, (rotation + (float)Director.random.NextDouble()), -speed / 4);
            director.getEffectManager().addFlame(positionT, 40, (rotation + (float)Director.random.NextDouble()), -speed / 4);
            director.getEffectManager().addFlame(positionT, 40, (rotation - (float)Director.random.NextDouble()), -speed / 4);
            director.getEffectManager().addFlame(positionT, 40, (rotation - (float)Director.random.NextDouble()), -speed / 4);

            director.getEffectManager().addFog(position, 30, Color.Orange, true);
            MainData.soundBank.PlayCue("Explode");
            director.getRumbleManager().addVibrate(25, 0.4f);

         splashDamage();
        }


        private void splashDamage()
        {
            foreach (Character c in director.getCharacters())
            {
                if(Vector2.Distance(c.getPosition() , position) < 150)
                {
                    if (c.isAlive())
                    {
                        director.getEffectManager().addFlame(c.getPosition(), 40, rotation, -speed / 5);
                        c.damage(1);
                    }
                }
            }


            if (Vector2.Distance(director.getPlayer().getPosition(), position) < 30)
            {
                director.getEffectManager().addFlame(director.getPlayer().getPosition(), 40, rotation, -speed / 5);
                director.getPlayer().damage(1);
            }

        }

        private void checkCollision()
        {

            foreach (Wall w in director.getGrid().getWalls())
            {
                if( hitBox.Intersects(w.getHitBox()))
                {
                    hit();
                    break;
                }
            }

            if (isDead == false)
            {

                foreach (Character c in director.getCharacters())
                {
                    if (c.isAlive())
                    {
                        if (hitBox.Intersects(c.getHitBox()))
                        {
                            c.damage(5);
                            hit();
                            break;
                        }
                    }
                }
            }


        }





        private void hit()
        {
            isDead = true;

            Vector2 velocity;
            velocity.X = (float)Math.Cos(rotation) * speed;
            velocity.Y = (float)Math.Sin(rotation) * speed;

            Vector2 positionT = position + (velocity * 2);

            explode(positionT);
        }


        public  void draw(SpriteBatch spriteBatch)
        {
            if (isDead == false)
            {

                spriteBatch.Draw(texture, position, null, new Color(200,0,0,150), angle, new Vector2(32, 32), 1.6f, SpriteEffects.None, 1);

                spriteBatch.Draw(texture, position, null, Color.White, angle, new Vector2(32, 32), 0.6f, SpriteEffects.None, 1);


              //  drawHitBox(spriteBatch);
            }
        }


        protected void drawHitBox(SpriteBatch sb)
        {


            sb.Draw(texture, new Vector2(hitBox.X, hitBox.Y), null, Color.Gold, 0, new Vector2(0, 0), 0.1f, SpriteEffects.None, 1);
            sb.Draw(texture, new Vector2(hitBox.X + hitBox.Width, hitBox.Y), null, Color.Gold, 0, new Vector2(0, 0), 0.1f, SpriteEffects.None, 1);
            sb.Draw(texture, new Vector2(hitBox.X, hitBox.Y + hitBox.Height), null, Color.Gold, 0, new Vector2(0, 0), 0.1f, SpriteEffects.None, 1);
            sb.Draw(texture, new Vector2(hitBox.X + hitBox.Width, hitBox.Y + hitBox.Height), null, Color.Gold, 0, new Vector2(0, 0), 0.1f, SpriteEffects.None, 1);


        }

    }
}
