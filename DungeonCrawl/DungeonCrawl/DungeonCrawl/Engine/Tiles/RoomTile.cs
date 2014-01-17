using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace DungeonCrawl.Engine.Tiles
{
    class RoomTile : Tile
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



        public RoomTile(Director d)
            : base(d)
        {
            director = d;
            setWallData(d);
        }



        public RoomTile(int x, int y, Director d)
            : base(x, y, d)
        {
            xTilePosition = x;
            yTilePositon = y;
            director = d;

            setWallData(d);




            walkable = true;
        }















        private void setWallData(Director d)
        {
            texturePath = "Map//Tiles//Room";
            tileString = "r";
        }






        public override void localDraw(SpriteBatch sb)
        {

            sb.Draw(tileTexture, position, null, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 1);

        }




    }
}