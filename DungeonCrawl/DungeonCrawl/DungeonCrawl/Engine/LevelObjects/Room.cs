using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using DungeonCrawl.Engine.Tiles;

namespace DungeonCrawl.Engine.LevelObjects
{
    class Room
    {


        public List<Tile> roomTiles = new List<Tile>();


        bool isEntryRoom;
        bool isExitRoom;


        Stair stair;

        Director director;

        #region SETS


        public void setEntryRoom()
        {
            isEntryRoom = true;
            isExitRoom = false;
        }


        public void setExitRoom()
        {
            isEntryRoom = false;
            isExitRoom = true;
        }





        #endregion




        public bool isRoomStart()
        {
            return isEntryRoom;
        }

        public bool isRoomExit()
        {
            return isExitRoom;
        }


        public List<Tile> getTiles()
        {
            return roomTiles;
        }



        public Stair getStair()
        {
            return stair;
        }

        public Room( Director d)
        {


            director = d;



        }






        public void load(ContentManager content)
        {


            foreach (Tile t in roomTiles)
            {
                t.load(content);
            }



            if((isEntryRoom == true) || (isExitRoom == true))
            {
                Tile temp;

                Random random = new Random();

                bool positionValid = false;
                do
                {
                    temp = roomTiles[random.Next(1,roomTiles.Count -1)];

                    if (temp is RoomTile)
                    {
                        positionValid = true;
                    }

                } while (positionValid == false);


                if (isEntryRoom == true)
                {
                    stair = new Stair(director, temp.getX(), temp.getY() , true);
                }
                else
                {
                    stair = new Stair(director, temp.getX(), temp.getY() , false);
                }

                stair.load(content);
            }
          

        }







        public void update()
        {




        }







        public virtual void draw(SpriteBatch sb)
        {

           // sb.Draw(tileTexture, position, null, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 1);

            foreach (Tile t in roomTiles)
            {
                if (t is DoorTile)
                {
                    t.draw(sb);
                }
                else
                {
                      t.draw(sb);
                }
            }


            if (stair != null)
            {
                stair.draw(sb);
            }
   
        }




    }
}
