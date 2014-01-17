using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace DungeonCrawl.Engine
{

    class Tile
    {
        //Fields



        protected int xTilePosition;
        protected int yTilePositon;



        protected Vector2 position;
        protected Texture2D tileTexture;
        protected Rectangle hitbox;


       

        protected bool walkable = true;

        //Attributes
        protected bool isBuildable = false;




        protected String tileString = "t";



        protected static Director director;


        protected const int WIDTH = 128;
        protected const int HEIGHT = 128;








        //DEBUG
        private static Texture2D debugTexture;
        private static Texture2D tileHighlight;



        private bool isHighlighted = false;


        protected  String texturePath = "Map//Tiles//Floor";

        #region SETS



        public void setTile(Tile t)
        {
            setUp(t);
        }



        protected virtual void setUp(Tile t)
        {
            xTilePosition = t.xTilePosition;
            yTilePositon = t.yTilePositon;
            position = t.position;
            hitbox = t.hitbox;
        }




        #endregion




        #region GETS




        public bool isWalkable()
        {
            return walkable;
        }

        public String getChar()
        {
            return tileString;
        }

        public int getX()
        {
            return xTilePosition;
        }

        public int getY()
        {
            return yTilePositon;
        }

        public Rectangle getHitBox()
        {
            return hitbox;
        }


        public Vector2 getPosition()
        {
            return position;
        }



        #endregion



        #region SETS


        public void highlight()
        {
            isHighlighted = true;
        }


        public void setPosition(Vector2 positionT)
        {
            position = positionT;
        }



        #endregion


        public Tile(Director d)
        {
            texturePath = "Map//Tiles//Floor";
            director = d;
        }


        public Tile(int x, int y, Director d)
        {
            xTilePosition = x;
            yTilePositon = y;
            director = d;




            position.X = (x * WIDTH);
            position.Y = (y * HEIGHT);


            hitbox = new Rectangle((int)position.X, (int)position.Y, WIDTH, HEIGHT);

        }



      




        public void load(ContentManager content)
        {


      
            tileTexture = content.Load<Texture2D>(texturePath);
           


          

        }







        public void update()
        {




        }







        public virtual void draw(SpriteBatch sb)
        {

          

            if (Camera.onScreen(position))
            {

                localDraw(sb);

            }
        }



            public virtual void localDraw(SpriteBatch sb)
            {
                   sb.Draw(tileTexture, position, null, Color.DarkGray, 0, new Vector2(0, 0), 1, SpriteEffects.None, 1);
            }

   
        











     

    }
}