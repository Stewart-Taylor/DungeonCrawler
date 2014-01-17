using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace DungeonCrawl.Engine
{
    class LevelGenerator
    {


        bool validMap;


        int width;
        int height;

        string[,] map;


        Room currentRoom;


        int firstRoom;
        int lastRoom;

        public List<Room> rooms = new List<Room>();


        public List<RoomTile> path = new List<RoomTile>();



        public struct Room
        {
            public List<RoomTile> tiles;
            public bool isConnected;
            public int id;
            public List<Room> connectedRooms;
        }



   




        public struct RoomTile
        {
            public int x;
            public int y;
            public String type ;
            public int id ;
        }

       




        public LevelGenerator(int widthT, int heightT)
        {

            width = widthT;
            height = heightT;

            generateMap();
        }






        private void generateMap()
        {


            int sections = 3;


            //generate rooms


            generateRoom(0, 0, width / 3, height / 3);
            generateRoom(width / 3 , 0, (width / 3) * 2, height / 3);
            generateRoom((width / 3) * 2 , 0, width , height / 3);


            generateRoom(0, height / 3, width / 3, (height / 3) * 2);
            generateRoom(width / 3, height / 3, (width / 3) * 2, (height / 3) * 2);
            generateRoom((width / 3) * 2, height / 3, width, (height / 3) * 2);

            generateRoom(0, (height / 3) * 2, width / 3, height);
            generateRoom(width / 3, (height / 3) * 2, (width / 3) * 2, height);
            generateRoom((width / 3) * 2, (height / 3) * 2, width, height);


            //pick random room 

            int roomID = Director.random.Next(rooms.Count);
            firstRoom = roomID;

            
            Room tRoom = rooms[roomID];
            tRoom.isConnected = true;

            rooms[roomID] = tRoom;

            for (int i = 0; i < 8; i++)
            {
               
                roomID = connectPath(rooms[roomID]);
                rooms[roomID] = currentRoom;

            }
          //  roomID = connectPath(rooms[roomID]);
          //  rooms[roomID] = currentRoom;

          
            //  connectPath(currentRoom);


            //random connections


           

                for (int i = 0; i < rooms.Count; i++)
                {
                    Room r = rooms[i];
                    r.isConnected = false;

                    rooms[i] = r;

                    if (i == rooms.Count - 1)
                    {
                        lastRoom = r.id;
                    }

                }
            

                roomID = Director.random.Next(rooms.Count);

                tRoom = rooms[roomID];
                tRoom.isConnected = true;

                rooms[roomID] = tRoom;

                for (int i = 0; i < Director.random.Next(3); i++)
                {

                    roomID = connectPath(rooms[roomID]);
                    rooms[roomID] = currentRoom;

                }



                wideHalls();

        }




        private void generateRoom(int startX, int startY, int endX, int endY)
        {


            Room room = new Room();
            room.id = rooms.Count;


            room.tiles = new List<RoomTile>();


            int size = Director.random.Next(5, 15);

            int roomWidth = size + Director.random.Next(5);
            int roomHeight = size + Director.random.Next(5);



            bool validPosition = false;

            int xPosition;
            int yPosition;



            do
            {
                validPosition = true;

                xPosition = Director.random.Next(startX  , endX );
                yPosition = Director.random.Next(startY  , endY );



                if (xPosition + roomWidth >= endX -4 )
                {
                    validPosition = false;
                }
                else if (yPosition + roomHeight >= endY -4 )
                {
                    validPosition = false;
                }


            } while (validPosition == false);




            //generate walls




            for (int x = xPosition + 1; x < xPosition + roomWidth; x++)
            {
                //draw top 

                room.tiles.Add(createTile(x, yPosition, room.id, "w"));
            }


            for (int x = xPosition + 1; x < xPosition + roomWidth; x++)
            {
                //draw bottom 


                room.tiles.Add(createTile(x, yPosition + roomHeight, room.id, "w"));

            }


            for (int y = yPosition + 1; y < yPosition + roomHeight; y++)
            {
                //draw left 

                room.tiles.Add(createTile(xPosition, y, room.id, "w"));
            }

            for (int y = yPosition + 1; y < yPosition + roomHeight; y++)
            {
                //draw right 
                room.tiles.Add(createTile(xPosition + roomWidth, y, room.id, "w"));
            }







            //room interior
            for (int y = yPosition + 1; y < yPosition + roomHeight; y++)
            {
                for (int x = xPosition + 1; x < xPosition + roomWidth; x++)
                {


                    room.tiles.Add(createTile(x, y, room.id, "r"));


                }
            }



            //add corner type
            room.tiles.Add(createTile(xPosition, yPosition, room.id, "cw"));
            room.tiles.Add(createTile(xPosition + roomWidth, yPosition, room.id, "cw"));
            room.tiles.Add(createTile(xPosition, yPosition + roomHeight, room.id, "cw"));
            room.tiles.Add(createTile(xPosition + roomWidth, yPosition + roomHeight, room.id, "cw"));




            rooms.Add(room);






        }








        private int connectPath( Room room)
        {


            bool foundConnection = false;

            List<RoomTile> tiles = new List<RoomTile>();

            RoomTile rT = new RoomTile(); ;
            int r;

            do
            {


                int attempts = 1000;

                tiles.Clear();




                bool startValid = false;
                int currentX = 0;
                int currentY = 0;




                do
                {

                    r = Director.random.Next(room.tiles.Count);

                    if (room.tiles[r].type == "w")
                    {
                        startValid = true;
                        currentX = room.tiles[r].x;
                        currentY = room.tiles[r].y;

                        rT = room.tiles[r];

                        room.tiles[r] = rT;
                    }


                } while (startValid == false);







                if (isTileEmpty(currentX, currentY - 1)) // UP
                {
                    currentY -= 1;
                }
                else if (isTileEmpty(currentX, currentY + 1)) //DOWN
                {
                    currentY += 1;
                }
                else if (isTileEmpty(currentX + 1, currentY)) //RIGhT
                {
                    currentX += 1;
                }
                else if (isTileEmpty(currentX - 1, currentY)) //LEFT
                {
                    currentX -= 1;
                }



                tiles.Add(createTile(currentX, currentY, -1, "t"));


                int directionX;
                int directionY;

                bool validTile = false;

                

                do
                {

                    

                    do
                    {

                        directionX = 0;
                        directionY = 0;


                        int rTemp = Director.random.Next(4);

                        if (rTemp == 0)
                        {
                            directionX = 1;
                        }
                        else if (rTemp == 1)
                        {
                            directionX = -1;
                        }
                        else if (rTemp == 2)
                        {
                            directionY = 1;
                        }
                        else if (rTemp == 3)
                        {
                            directionY = -1;
                        }





                        if (isTileEmpty(currentX + directionX, currentY + directionY) == true)
                        {

                            validTile = true;


                        }
                        else
                        {

                            
                            for (int a = 0; a < rooms.Count; a++)
                            {
                                Room tempRoom = rooms[a];

                                if (tempRoom.isConnected == false)
                                {
                                    if (tempRoom.id != room.id)
                                    {
                                        for (int b = 0; b < tempRoom.tiles.Count; b++)
                                        {
                                            RoomTile tempTile = tempRoom.tiles[b];
                                            if ((tempTile.x == currentX + directionX) && (tempTile.y == currentY + directionY))
                                            {
                                                if (tempTile.type != "cw")
                                                {
                                                    currentRoom = tempRoom;
                                                    tempTile.type = "d";
                                                    foundConnection = true;
                                                    tempRoom.tiles[b] = tempTile;
                                                    break;
                                                }
                                                else
                                                {
                                                    
                                                        attempts = - 40;
                                                    
                                                }
                                            }

                                        }
                                    }
                                }
                                rooms[a] = tempRoom;
                            }
                             

                        }









                    } while (validTile == false);



               

                    if (validTile == true)
                    {
                        tiles.Add(createTile(currentX + directionX, currentY + directionY, -1, "t"));




                        for (int i = 2; i < Director.random.Next(5, 30); i++)
                        {
                            if (isTileEmpty(currentX + (directionX * i), currentY + (directionY * i)))
                            {
                                tiles.Add(createTile(currentX + (directionX * i), currentY + (directionY * i), -1, "t"));
                            }
                            else
                            {
                                for(int a = 0 ; a < rooms.Count ; a++) 
                                {
                                    Room tempRoom = rooms[a];


                                    if (tempRoom.isConnected == false)
                                    {
                                        if (tempRoom.id != room.id)
                                        {
                                            for (int b = 0; b < tempRoom.tiles.Count; b++)
                                            {
                                                RoomTile tempTile = tempRoom.tiles[b];
                                                if ((tempTile.x == currentX + directionX) && (tempTile.y == currentY + directionY))
                                                {
                                                    if (tempTile.type != "cw")
                                                    {
                                                        currentRoom = tempRoom;
                                                        tempTile.type = "d";
                                                        foundConnection = true;
                                                        tempRoom.tiles[b] = tempTile;
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        attempts = - 40;
                                                    }
                                                }

                                            }
                                        }
                                    }
                                    rooms[a] = tempRoom;
                                }
                                break;
                            }

                        }



                        currentX = tiles.Last().x;
                        currentY = tiles.Last().y;
                    }



                    attempts--;
                    if (attempts < 0)
                    {
                        break;
                    }


                    if (tiles.Count > 500)
                    {
                        break;
                    }


                } while (foundConnection == false);


            } while (foundConnection == false);





            currentRoom.isConnected = true;
           
                //path =  tiles;


               

                foreach (RoomTile t in tiles)
                {
                    path.Add(t);
                }
                rT.type = "d";
                room.tiles[r] = rT;


           

                           
            

                return currentRoom.id;
      
      

        }









        private bool isTileEmpty(int x, int y)
        {

            foreach (Room r in rooms)
            {
                foreach (RoomTile t in r.tiles)
                {
                    if ((t.x == x) && (t.y == y))
                    {
                        return false;
                    }
                }
            }

            return true;
        }






        private RoomTile createTile(int x, int y, int id, String type)
        {
            RoomTile t = new RoomTile();

            t.id = id;
            t.x = x;
            t.y = y;
            t.type = type;

            return t;
        }





















        private void wideHalls()
        {

            List<RoomTile> newTiles = new List<RoomTile>();




            for (int i = 0; i < path.Count; i++)
            {
                RoomTile t = path[i];


                bool foundTop = false;
                bool foundBottom = false;

                foreach(RoomTile aT in path)
                {
                    if ((t.x == aT.x) && (t.y + 1 == aT.y))
                    { // tile above 
                        foundTop = true;
                        break;
                    }


                 }

                foreach (RoomTile aT in path)
                {
                    if ((t.x == aT.x) && (t.y - 1 == aT.y))
                    {
                        foundBottom = true;
                        break;
                    }
                }

                    if ( ( foundBottom == true) && ( foundTop == true))
                    {
                        if (isPathEmpty(t.x + 1, t.y))
                        {
                            newTiles.Add(createTile(t.x + 1, t.y, -1, "t"));
                        
                        }
                        else if (isPathEmpty(t.x - 1, t.y))
                        {
                            newTiles.Add(createTile(t.x - 1, t.y, -1, "t"));
                        
                        }

                    }





                    bool foundLeft = false;
                    bool foundRight = false;

                    foreach (RoomTile aT in path)
                    {
                        if ((t.x + 1== aT.x) && (t.y  == aT.y))
                        { // tile above 
                            foundRight = true;
                            break;
                        }


                    }

                    foreach (RoomTile aT in path)
                    {
                        if ((t.x -1 == aT.x) && (t.y  == aT.y))
                        {
                            foundLeft = true;
                            break;
                        }
                    }

                    if ((foundLeft == true) && (foundRight == true))
                    {
                        if (isPathEmpty(t.x , t.y + 1))
                        {
                            newTiles.Add(createTile(t.x , t.y + 1, -1, "t"));
                          
                        }
                        else if (isPathEmpty(t.x , t.y -1))
                        {
                            newTiles.Add(createTile(t.x , t.y, -1, "t"));

                        }

                    }
                

                





                path[i] = t;
            }




            foreach (RoomTile t in newTiles)
            {
                path.Add(t);
            }


        }




        private bool isPathEmpty(int x, int y)
        {

            foreach (Room r in rooms)
            {
                foreach (RoomTile t in r.tiles)
                {
                    if ((t.x == x) && (t.y == y))
                    {
                        return false;
                    }
                }
            }


            foreach (RoomTile t in path)
            {
                if ((t.x == x) && (t.y == y))
                {
                    return false;
                }
            }

            return true;
        }





        public List<LevelObjects.Room> getRooms(Director director)
        {
            List<LevelObjects.Room> roomObjects = new List<LevelObjects.Room>();


            foreach (Room r in rooms)
            {
                LevelObjects.Room roomT = new LevelObjects.Room(director);



                if (r.id == firstRoom)
                {
                    roomT.setEntryRoom();
                }
                else if (r.id == lastRoom)
                {
                    roomT.setExitRoom();
                }


                     //Set up tiles
                     foreach (RoomTile t in r.tiles)
                    {
                       // Engine.Tile newTile;

                        //get refrences to tiles
                        if (t.type == "r")
                        {
                            roomT.roomTiles.Add(director.grid.getTiles()[t.x, t.y]);
                        }
                        else if (t.type == "d")
                        {
                            roomT.roomTiles.Add(director.grid.getTiles()[t.x, t.y]);
                        }


                         //get refrences to tiles
                        //roomT.roomTiles.Add(director.grid.tiles[t.x, t.y]);



                         /*
                        if (t.type == "r")
                        {
                             newTile = new Engine.Tiles.RoomTile(t.x, t.y, director);
                             roomT.roomTiles.Add(newTile);
                        }
                        else if (t.type == "d")
                        {
                            newTile = new Engine.Tiles.DoorTile(t.x, t.y, director);
                            roomT.roomTiles.Add(newTile);
                        }
                          * 
                          */ 
                    }
                     roomObjects.Add(roomT);
            }



                return roomObjects;

        }



       


        public string[,] getMap()
        {

            //convert objects to map tiles


            int xOffset = 0;
            int yOffset = 0;

            int xMax =0;
            int yMax =0;
            int xMin =0;
            int yMin =0;






            foreach (Room o in rooms)
            {
                foreach (RoomTile t in o.tiles)
                {

                    if (t.x > xMax)
                    {
                        xMax = t.x ;
                    }

                    if (t.y > yMax)
                    {
                        yMax = t.y ;
                    }

                    if (t.y < yMin)
                    {
                        yMin = t.y ;
                    }

                    if (t.x < xMin)
                    {
                        xMin = t.x ;
                    }
                }
            }




            foreach (RoomTile t in path)
            {
                if (t.x > xMax)
                {
                    xMax = t.x;
                }

                if (t.y > yMax)
                {
                    yMax = t.y;
                }

                if (t.y < yMin)
                {
                    yMin = t.y;
                }

                if (t.x < xMin)
                {
                    xMin = t.x;
                }
            }


            int widthT = Math.Abs(xMin) + Math.Abs(xMax) + 20;
            int heightT = Math.Abs(yMin) + Math.Abs(yMax) + 20;







            for (int i = 0; i < rooms.Count; i++)
            {
                Room r = rooms[i];
                for (int x = 0; x < r.tiles.Count; x++)
                {
                    RoomTile t = r.tiles[x];
                    t.x += Math.Abs(xMin) + 2;
                    t.y += Math.Abs(yMin)  + 1;

                    r.tiles[x] = t;
                }

                rooms[i] = r;
            }



            for (int x = 0; x < path.Count; x++)
            {
                RoomTile t = path[x];
                t.x += Math.Abs(xMin) + 2;
                t.y += Math.Abs(yMin) + 1;

                path[x] = t;
            }



            map = new string[widthT, heightT];

            for (int y = 0; y < heightT; y++)
            {
                for (int x = 0; x < widthT; x++)
                {
                    map[x, y] = "w";
                }
            }





            foreach (RoomTile t in path)
            {
                map[t.x, t.y] = t.type;

            }



            
            foreach (Room r in rooms)
            {
                foreach (RoomTile t in r.tiles)
                {
                    if (t.type == "cw")
                    {
                        map[t.x, t.y] = "w";
                    }
                    else  //if ( t.type == "w")
                    {
                        map[t.x, t.y] = t.type;
                    }
                }
            }
              




            return map;
        }



    }
}
