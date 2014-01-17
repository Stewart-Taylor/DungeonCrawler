using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace DungeonCrawl.Engine
{
    class Treasure
    {

        #region Fields


        Texture2D texture;

        Rectangle hitBox;

        Vector2 position;

        int value = 1;
        Director director;

        bool pickedUp = false;

        #endregion




        public Rectangle getHitBox()
        {
            return hitBox;
        }



        public Treasure(Director directorT , Vector2 positionT)
        {
            position = positionT;
            director = directorT;
        }


       

        public void load(ContentManager content)
        {

            texture = content.Load<Texture2D>("Treasure//Coin");

            hitBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);


            director.getEffectManager().addFog(position, 1, Color.Gold, true);
        }



        public void update()
        {

            if (hitBox.Intersects(director.getPlayer().getHitBox()))
            {
                pickUp();
            }

        }

        private void pickUp()
        {
            if (pickedUp == false)
            {
                MainData.soundBank.PlayCue("menuMove");
                director.getPlayer().giveGold(value);
            }  
            pickedUp = true;
        }



        public void draw(SpriteBatch sb)
        {
            if (pickedUp == false)
            {
                if(Camera.onScreen(position))
                {

                sb.Draw(texture, position, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
            }
            }
        }



    }
}
