using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonCrawl.Engine.Effects
{
    class Fog
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
        int limit = 100;

        Vector2 source;

        bool additive;

        #region SETS





        #endregion

        public bool isAdditve()
        {
            return additive;
        }

        public bool isDead()
        {
            if (age >= limit)
            {
                return true;
            }
            return false;
        }




        public Fog(Vector2 positionT, ContentManager content ,Color colorT , bool additiveT)
        {
            position = positionT;
            source = position;

            additive = additiveT;

            angle = (float)Director.random.Next(300);
            speed = (float)Director.random.NextDouble()  ;
            scale =  (float)Director.random.NextDouble();

            color = colorT;

            load(content);
        }



      



        public void load(ContentManager content)
        {
            texture = content.Load<Texture2D>("Effects//Fog");


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


            speed = (float)Director.random.NextDouble();
        }

        public void draw(SpriteBatch sb)
        {

            

                sb.Draw(texture, position, null, color, rotation, origin, scale, SpriteEffects.None, 1);

            
        }

    }
}