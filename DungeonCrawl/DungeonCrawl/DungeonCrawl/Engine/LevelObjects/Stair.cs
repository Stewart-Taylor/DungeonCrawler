using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace DungeonCrawl.Engine.LevelObjects
{
    class Stair
    {



        bool isEntryStair;
        Vector2 position;
        Rectangle hitBox;
        Texture2D texture;
        int xTile;
        int yTile;
        Director director;





        #region SETS





        #endregion




        #region GETS



        public bool isEntry()
        {
            return isEntryStair;
        }

        public Vector2 getPositon()
        {
            return position;
        }

        public Rectangle getHitBox()
        {
            return hitBox;
        }



        #endregion





        public Stair(Director d , int x , int y ,bool entryStair)
        {
            director = d;

            xTile = x;
            yTile = y;


            isEntryStair = entryStair;

            //set position 

            position = director.getGrid().getTiles()[xTile, yTile].getPosition();


            hitBox = new Rectangle((int)position.X, (int)position.Y, 128, 128);
        }


        public void load(ContentManager content)
        {


            if (isEntryStair == true)
            {
                texture = content.Load<Texture2D>("Map//Objects//StairUp");
            }
            else
            {
                texture = content.Load<Texture2D>("Map//Objects//StairDown");
            }




        }






        public void draw(SpriteBatch sb)
        {

            sb.Draw(texture, position, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);

        }

    }
}
