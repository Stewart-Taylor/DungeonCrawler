using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DungeonCrawl.Engine.Tiles;
using DungeonCrawl.Engine.Characters;
using Microsoft.Xna.Framework;
using DungeonCrawl.Engine.LevelObjects;

namespace DungeonCrawl.Engine.Level
{
    class LevelObjectGenerator
    {


        Director director;

        public LevelObjectGenerator(Director directorT)
        {
            director = directorT;

        }





        private void spawnMobsInRoom(Room room)
        {

            int r = Director.random.Next(100);

            int count = 0;

            if (r > 90)
            {
                //spawn wraith
                Tile t = null;
                do
                {
                    r = Director.random.Next(1, room.getTiles().Count - 1);
                     t = room.getTiles()[r];

                } while (t is Wall);
                spawnBoss(t.getPosition());

            }
            else if (r < 30)
            {

                foreach (Tile t in room.getTiles())
                {
                    if (count < 10)
                    {
                        if (Director.random.Next(100) < Director.random.Next(5, 50))
                        {
                            count++;
                            spawnCorridorMob(t.getPosition());
                        }
                    }
                }

            }
            else
            {
                foreach (Tile t in room.getTiles())
                {
                    if (count < 20)
                    {
                        if (Director.random.Next(100) < Director.random.Next(5, 20))
                        {
                            count++;
                            spawnRoomMob(t.getPosition());
                        }
                    }
                }

            }
        }





        private void spawnBoss(Vector2 position)
        {
          

            Character mob = null;

            if (Director.random.Next(100) < 50)
            {
                //wraith
                mob = new Wraith(director, position);
            }
            else
            {
                //wizard when done
                mob = new Wraith(director, position);
            }


            director.addCharacter(mob);


        }


        private void spawnMobsInHalls()
        {


            int gap = 300;

            foreach (Tile tile in director.getGrid().getTiles())
            {
                if (isValidHallTile(tile) == true)
                {
                    if (Director.random.Next(1000) < Director.random.Next(5, 20))
                    {
                        spawnCorridorMob(tile.getPosition());
                    }

                }
            }


        }


        private bool isValidHallTile(Tile tile)
        {

            if (tile is WallTile)
            {
                return false;
            }

            if (tile is DoorTile)
            {
                return false;
            }

            foreach (Room room in director.getGrid().getRooms())
            {
                foreach (Tile roomTile in room.getTiles())
                {
                    if( tile.Equals(roomTile))
                    {
                        return false;
                    }
                }
            }



            return true;
        }


        public void spawnMobs()
        {

            //Spawn Mobs In rooms

            foreach (Room room in director.getGrid().getRooms())
            {
                if (room.isRoomStart() == false) 
                {
                    spawnMobsInRoom(room);
                }
            }


            spawnMobsInHalls();


            /*

            foreach (Tile t in director.getGrid().getTiles())
            {
                if (t is RoomTile)
                {
                    int s = Director.random.Next(100);

                    if (s  < 5)
                    {
                        spawnRoomMob(t.getPosition());
                    }

                }
                else if (t is Tile)
                {
                    if( !(t is  WallTile))
                    {

                    int s = Director.random.Next(1000);

                    if (s  <= 50)
                    {
                        spawnCorridorMob(t.getPosition());
                    }
                }
                }
            }
             * 
             * 
             */ 
        }



        private Character spawnRoomMob(Vector2 position)
        {
            Character mob = new Skeleton(director, position);
            director.addCharacter(mob);

            return mob;
        }




        private Character spawnCorridorMob(Vector2 position)
        {
            Character mob = null;

            if (Director.random.Next(100) < 50)
            {
                 mob = new Skeleton(director, position);
            }
            else
            {
                 mob = new Zombie(director, position);
            }

            
            director.addCharacter(mob);

            return mob;


        }




        public void placeTreasure()
        {

            foreach(Room room in director.getGrid().getRooms())
            {
                if ( (room.isRoomStart() == false) && (room.isRoomExit()) == false)
                {
                    foreach (Tile t in room.getTiles())
                    {
                        if (Director.random.Next(100) == 45)
                        {
                            Chest chest = new Chest(director, t.getPosition());

                            director.addLevelObject(chest);
                            break;
                        }

                    }
                }
            }


        }



        public void placeTorches()
        {

            List<WallTorch> torches = new List<WallTorch>();


            //Fake torch REQUIRED
                        WallTorch temp = new WallTorch(director, new Vector2(1,1));
                        torches.Add(temp);

           

            foreach(Wall w in director.getGrid().getWalls() )
            {

                bool valid = false;
              
                    foreach (WallTorch t in torches)
                    {



                        if (Vector2.Distance(t.getPosition(), w.getPosition()) < 1500)
                        {
                            //Torches too close 
                            valid = false;
                            break;
                        }
                        else
                        {
                            valid = true;
                        }



                    }

                    if (valid == true)
                    {

                        WallTorch lo = new WallTorch(director, new Vector2(w.getPosition().X - 64, w.getPosition().Y - 64));
                        director.addLevelObject(lo);
                        torches.Add(lo);
                    }
                }
 

                    
                

            torches.Clear();


            }
        

        public void placeTraps()
        {

            foreach (Tile tile in director.getGrid().getTiles())
            {
                if (isValidHallTile(tile) == true)
                {
                    if (Director.random.Next(100) == 67 )
                    {
                        Spike lo = new Spike(director, tile.getPosition());
                        director.addLevelObject(lo);
                    }

                }
            }


        }



        public void placeCobWeb()
        {


            foreach(Tile t in  director.getGrid().getTiles())
            {
                if (Director.random.Next(100) == 45)
                {

                    Cobweb web = new Cobweb(director, t.getPosition());
                    director.addLevelObject(web);
                    



                }
            }



        }


    }
}
