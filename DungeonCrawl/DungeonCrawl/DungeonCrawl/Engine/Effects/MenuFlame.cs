using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace DungeonCrawl.Engine.Effects
{
    class MenuFlame
    {

 
        Vector2 position;
        float rotation;
        float angle;
        Texture2D texture;



        Color color = Color.White;

        float speed;
        float scale = 0;


        Vector2 origin;

        int age;
        int limit = 20;

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




        public MenuFlame(Vector2 positionT , ContentManager content)
        {
            position = positionT;
            source = position;

            angle = MathHelper.ToRadians(-90);

            angle += (float)Director.random.NextDouble() /2 - (float)Director.random.NextDouble() /2;

            speed = (float)Director.random.NextDouble() * 5;
            scale = 3f + (float)Director.random.NextDouble() + (float)Director.random.NextDouble();

            limit += Director.random.Next(40);

            load(content);
        }



 



        public void load(ContentManager content)
        {
                texture = content.Load<Texture2D>("Effects//Flame");
                

                origin.X =  texture.Width / 2;
                origin.Y =  texture.Height / 2;
        }




        public void update()
        {


            if (age <= limit/2 )
            {
                scale += 0.005f;
            }
            if (age <= limit)
            {
                age++;



                scale -= 0.06f;

                Vector2 velocity;
                velocity.X = (float)Math.Cos(angle) * speed;
                velocity.Y = (float)Math.Sin(angle) * speed;


                rotation += (float)Director.random.NextDouble() / 10;

                position += velocity;



                float percent = (float)age / (float)limit;
                color.A = (byte)((250 - (percent * 250)) + 10);
            }



        }

        public void draw(SpriteBatch sb)
        {
            
      
                sb.Draw(texture, position, null, color, rotation, origin, scale, SpriteEffects.None, 1);

            
        }

    }
}