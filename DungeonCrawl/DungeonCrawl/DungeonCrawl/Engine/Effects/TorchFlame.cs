using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace DungeonCrawl.Engine.Effects
{
    class TorchFlame
    {


        Vector2 position;
        float rotation;
        float angle;
        Texture2D texture;
        Texture2D glowTexture;

        Color color = Color.White;
        Color glowColor = Color.Orange;

        float speed;
        float scale = 0;


        Vector2 origin;

        int age;
        int limit = 100;

        Vector2 source;

  

        #region SETS





        #endregion

 
        public bool isDead()
        {
            if (age >= limit)
            {
                return true;
            }
            return false;
        }




        public TorchFlame(Vector2 positionT, ContentManager content )
        {
            position = positionT;
            source = position;

   

            angle = (float)Director.random.Next(300);
            speed = (float)Director.random.NextDouble()  ;
            scale =  (float)Director.random.NextDouble() + 1f;

            color = Color.White;

            load(content);
        }



      



        public void load(ContentManager content)
        {
            texture = content.Load<Texture2D>("Effects//Flame");
            glowTexture = content.Load<Texture2D>("Effects//Fog");

            origin.X = texture.Width / 2;
            origin.Y = texture.Height / 2;
        }




        public void update()
        {


            

            if (age <= limit / 2)
            {
                scale += 0.001f;
            }
            if (age <= limit)
            {
                age++;



                //scale += 0.001f;

                Vector2 velocity;
                velocity.X = (float)Math.Cos(angle) * speed;
                velocity.Y = (float)Math.Sin(angle) * speed;


                rotation += (float)Director.random.NextDouble() / 10;

                position += velocity;



                float percent = (float)age / (float)limit;
                color.A = (byte)((250 - (percent * 250)) + 10);
            }


            glowColor.A = color.A;

            speed = (float)Director.random.NextDouble();
        }

        public void draw(SpriteBatch sb)
        {


           
                sb.Draw(texture, position, null, color, rotation, origin, scale, SpriteEffects.None, 1);

            
        }

    }
}