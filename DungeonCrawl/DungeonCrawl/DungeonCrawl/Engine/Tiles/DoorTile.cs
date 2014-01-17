using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace DungeonCrawl.Engine.Tiles
{
    class DoorTile : Tile
    {
        //Fields







        protected override void setUp(Tile t)
        {
            xTilePosition = t.getX();
            yTilePositon = t.getY();
            position = t.getPosition();
            hitbox = t.getHitBox();


        }



        #region SETS










        #endregion




        #region GETS




        #endregion



        public DoorTile(Director d)
            : base(d)
        {
            director = d;
            setWallData(d);
        }



        public DoorTile(int x, int y, Director d)
            : base(x, y, d)
        {
            xTilePosition = x;
            yTilePositon = y;
            director = d;
            setWallData(d);




            walkable = false;
        }










      




        private void setWallData(Director d)
        {
            texturePath = "Map//Tiles//Room";
            tileString = "d";
        }






        public override void localDraw(SpriteBatch sb)
        {
         
                sb.Draw(tileTexture, position, null, Color.Beige, 0, new Vector2(0, 0), 1, SpriteEffects.None, 1);
            
        }




    }
}