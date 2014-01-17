using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using DungeonCrawl.Engine.Tiles;

namespace DungeonCrawl.Engine
{
    class MiniMap
    {



        #region FIELDS


        private Director director;


        private int clearDistance = 10;

        float dotScale = 1.6f;

        private Texture2D dot;

        private Texture2D mapTexture;
        private Vector2 mapPosition = new Vector2(1000, 00);

        private int xOffset = 30;
        private int yOffset = 30;

        private Vector2 dotOffSet  ;

        Vector2 playerDot;

        List<Dot> dots = new List<Dot>();

        public struct Dot
        {
            public bool visible;
            public Vector2 position;
            public Color color;

        }


        #endregion






        public MiniMap(Director directorT)
        {

            director = directorT;


            dotOffSet = new Vector2(mapPosition.X + xOffset , mapPosition.Y + yOffset );
        }




        public void load(ContentManager content)
        {

            

            mapTexture = content.Load<Texture2D>("Interface//MiniMap");
            dot = content.Load<Texture2D>("Dot");


            setMap();
        }




        private void setMap()
        {

            dots.Clear();

            foreach (Tile t in director.getGrid().getTiles())
            {
                Dot dot = new Dot();

                dot.position = new Vector2(t.getX() * dotScale, t.getY() * dotScale);

                if (t is WallTile)
                {
                    WallTile temp = (WallTile)t;
                    if (temp.getDrawable())
                    {
                        dot.color = Color.Black;
                        dots.Add(dot);
                    }
                }
                else if (t is RoomTile)
                {
                    dot.color = Color.LightBlue;
                    dots.Add(dot);
                }
                else if (t is RoomTile)
                {
                    dot.color = Color.Blue;
                    dots.Add(dot);
                }
                else
                {
                    dot.color = Color.Beige;
                    dots.Add(dot);
                }
           
            }

        }




        public void clearFog()
        {
            for (int i = 0; i < dots.Count; i++)
            {
                Dot d = dots[i];


                        d.visible = true;
                        dots[i] = d;

            }


        }


        #region Update



        public void update()
        {


            getPlayerPosition();
            updateFog();
        }


        private void updateFog()
        {

            for(int i = 0 ; i < dots.Count ; i++)
            {
                Dot d = dots[i];

                if (d.visible == false)
                {
                    if (Vector2.Distance(playerDot, d.position) < clearDistance)
                    {
                        d.visible = true;
                        dots[i] = d;
                    }


                }


            }


        }


        private void getPlayerPosition()
        {
            float maxPosReal = director.getGrid().getMaxX();
            float maxPosMap = maxPosReal / 128;


            float playerPosX = (director.getPlayer().getPosition().X / maxPosReal);
            playerPosX = maxPosMap * playerPosX;

            playerDot.X = playerPosX * dotScale;


             maxPosReal = director.getGrid().getMaxY();
             maxPosMap = maxPosReal / 128;

            float playerPosY = (director.getPlayer().getPosition().Y / maxPosReal);
            playerPosY = maxPosMap * playerPosY;

            playerDot.Y = playerPosY * dotScale;
        }






        #endregion







        #region DRAW


        public void draw(SpriteBatch sb)
        {
            sb.Draw(mapTexture, mapPosition, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);





            foreach (Dot d in dots)
            {
                if (d.visible)
                {
                    sb.Draw(dot, d.position + dotOffSet, null, d.color, 0, Vector2.Zero, 4f, SpriteEffects.None, 1);
                }

            }


            sb.Draw(dot, playerDot + dotOffSet , null, Color.Red, 0, Vector2.Zero, 5f , SpriteEffects.None, 1);
        }




     



      

        #endregion



    }
}