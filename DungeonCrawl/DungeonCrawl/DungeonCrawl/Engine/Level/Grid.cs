using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using DungeonCrawl.Engine.Tiles;
using Microsoft.Xna.Framework.Graphics;
using DungeonCrawl.Engine.LevelObjects;
using Microsoft.Xna.Framework;

namespace DungeonCrawl.Engine
{

        class Grid
        {

            //Fields
            public int xTiles = 300;
            public int yTiles = 300;

            private Director director;

            private Tile[,] tiles;

            private List<Room> rooms = new List<Room>();
            private List<Wall> walls = new List<Wall>();




      



            #region SETS





            #endregion




            #region GETS


            public List<Wall> getWalls()
            {
                return walls;
            }


            public int getMaxX()
            {


                return xTiles * 128;
            }

            public int getMaxY()
            {


                return yTiles * 128;
            }

            public Tile[,] getTiles()
            {
                return tiles;
            }


            public List<Room> getRooms()
            {
                return rooms;
            }



            public Vector2 getStartPosition()
            {
                foreach (Room r in rooms)
                {
                    if (r.isRoomStart())
                    {
                        Stair stair = r.getStair();
                        return stair.getPositon();


                    }
                }

                return Vector2.Zero;
            }




            public Rectangle getExit()
            {
                foreach (Room r in rooms)
                {
                    if (r.isRoomExit())
                    {
                        Stair stair = r.getStair();
                        return stair.getHitBox();


                    }
                }

                return new Rectangle();
            }



            #endregion







            public Grid(Director d)
            {
                director = d;
                xTiles = 80;
                yTiles = 80;
            }





            public void load(ContentManager content)
            {
                setUpLevel();


                for (int y = 0; y < yTiles; y++)
                {
                    for (int x = 0; x < xTiles; x++)
                    {
                        if (tiles[x, y] is WallTile)
                        {
                            WallTile t = (WallTile)tiles[x, y];
                            t.setDrawable();
                        }

                    }
                }





                foreach (Wall w in walls)
                {
                    w.load(content);

                }


        

                foreach (Room r in rooms)
                {
                    r.load(content);
                }

                foreach (Wall w in walls)
                {
                    w.setType();
                }

            }





            private void setUpLevel()
            {
                LevelGenerator levelGen = new LevelGenerator(xTiles, yTiles);

                string[,] map = levelGen.getMap();


                int widthT = map.GetLength(0);
                int heightT = map.GetLength(1);


                xTiles = widthT;
                yTiles = heightT;

                tiles = new Tile[widthT, heightT];

                for (int y = 0; y < heightT; y++)
                {
                    for (int x = 0; x < widthT; x++)
                    {
                        

                       // Tile t = new Tile(x, y, director);
                        Tile t  = TileFactory.getTile(map[x,y], director , x , y);
                        t.load(director.content);
                        tiles[x, y] = t;
                    }
                }


                rooms = levelGen.getRooms(director);


            }















            public void update()
            {

                for (int y = 0; y < yTiles; y++)
                {
                    for (int x = 0; x < xTiles; x++)
                    {
                        tiles[x, y].update(); // Not sure if needed
                    }
                }



                //update room
                foreach (Room r in rooms)
                {
                    r.update();
                }



            }









            public void draw(SpriteBatch sb)
            {

                for (int y = 0; y < yTiles; y++)
                {
                    for (int x = 0; x < xTiles; x++)
                    {
                        if (!(tiles[x, y] is RoomTile))
                        {
                            if (!(tiles[x, y] is DoorTile))
                            {
                                tiles[x, y].draw(sb);
                            }
                        }
                    }
                }




                foreach (Room r in rooms)
                {
                    r.draw(sb);
                }


            }




            public void drawWalls(SpriteBatch sb)
            {

                foreach (Wall w in walls)
                {
                    w.draw(sb);

                }
            }
        }
    }



    
