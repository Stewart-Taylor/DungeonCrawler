using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace DungeonCrawl.Engine.Effects
{
    class Blood
    {

 
        Vector2 position;
        float rotation;
        Texture2D texture;


        float scale;



        #region SETS





        #endregion








        public Blood(Vector2 positionT , ContentManager content)
        {
            position = positionT;

            scale = (float)Director.random.NextDouble();

            rotation = (float)Director.random.Next(300);

            load(content);
        }



        public void load(ContentManager content)
        {
                texture = content.Load<Texture2D>("Effects//Blood");
        }


        public void draw(SpriteBatch sb)
        {

            if (Camera.onScreen(position))
            {
                sb.Draw(texture, position, null, Color.White, rotation, Vector2.Zero, scale, SpriteEffects.None, 1);

            }
        }

    }
}

