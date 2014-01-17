using System;

namespace DungeonCrawl.Engine.Tiles
{
    class TileFactory
    {




        public static Tile getTile(String s , Director director,int x , int y)
        {


            Tile t = new Tile(director);

            if (s == "w")
            {
                t = new WallTile(x, y, director);
            }
            else if (s == "t")
            {
                t = new Tile(x, y, director);
            }
            else if (s == "d")
            {
                t = new DoorTile(x, y, director);
            }
            else if (s == "r")
            {
                t = new RoomTile(x, y, director);
            }
            else
            {
                t = new Tile(x, y, director);
            }

            return t;

        }


    }
}
