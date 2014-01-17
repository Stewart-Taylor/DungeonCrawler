using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using DungeonCrawl.Engine.LevelObjects;

namespace DungeonCrawl.Engine.Tiles
{

    class WallTile : Tile
    {
        //Fields




        bool drawable = false;



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


        public bool getDrawable()
        {
            return drawable;
        }



        #endregion



        public WallTile(Director d)
            : base(d)
        {
            director = d;
            setWallData(d);
        }



        public WallTile(int x, int y, Director d)
            : base(x, y, d)
        {
            xTilePosition = x;
            yTilePositon = y;
            director = d;
            setWallData(d);


           

            walkable = false;
        }



     



        public void setDrawable()
        {

            checkTile(xTilePosition - 1, yTilePositon - 1);  // TOP LEFT
            checkTile(xTilePosition, yTilePositon - 1);     // TOP MID
            checkTile(xTilePosition + 1, yTilePositon - 1);  // TOP RIGHT
            checkTile(xTilePosition - 1, yTilePositon);     // MID LEFT
            checkTile(xTilePosition + 1, yTilePositon);     // MID RIGHT
            checkTile(xTilePosition - 1, yTilePositon + 1);  // BOT LEFT
            checkTile(xTilePosition, yTilePositon + 1);     // BOT MID
            checkTile(xTilePosition + 1, yTilePositon + 1);  // BOT RIGHT


        }



        private void checkTile(int x, int y)
        {
            if (isTileValid(x, y) == true)
            {
                drawable = true;
                director.getGrid().getWalls().Add(new Wall(xTilePosition, yTilePositon, director));
            }
        }



        private bool isTileValid(int x, int y)
        {


            if ((x < 0) || (x > director.grid.xTiles - 2 ))
            {
                return false;
            }

            if ((y < 0) || (y > director.grid.yTiles - 2))
            {
                return false;
            }


            if (director.grid.getTiles()[x,y].isWalkable())
            {
                            return true;

            }


            return false;
        }




        private void setWallData(Director d)
        {
            texturePath = "Map//Tiles//WallTile";
            tileString = "w";

           
        }






        public override void localDraw(SpriteBatch sb)
        {
            if (drawable == true)
            {

                sb.Draw(tileTexture, position, null, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 1);
            }
        }
  



    }
}