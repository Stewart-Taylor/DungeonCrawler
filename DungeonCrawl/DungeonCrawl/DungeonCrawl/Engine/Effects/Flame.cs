using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace DungeonCrawl.Engine.Effects
{
    class Flame
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
        int limit = 40;

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




        public Flame(Vector2 positionT , ContentManager content)
        {
            position = positionT;
            source = position;

            angle = (float)Director.random.Next(300);
            speed = (float)Director.random.NextDouble();
            scale = 1f;

            rotation = (float)Director.random.Next(300);

            load(content);
        }



        public Flame(Vector2 positionT, ContentManager content , float angleT , float speedT)
        {
            position = positionT;
            source = position;

            angle = angleT;
            speed = (float)Director.random.NextDouble() * 10;
            scale = 1f;

            angle += (float)Director.random.NextDouble() /12 - (float)Director.random.NextDouble() /12;

            rotation = (float)Director.random.Next(300);

            speed += speedT * 2f;

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
            age++;

            if (age <= limit/2 )
            {
                scale += 0.005f;
            }
            if (age <= limit)
            {
             



                scale -= 0.02f;

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

            if (Vector2.Distance(source, position) > 70)
            {

                sb.Draw(texture, position, null, color, rotation, origin, scale, SpriteEffects.None, 1);

            } 
        }

    }
}