using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace DungeonCrawl.Engine
{
    class LevelObject
    {

        #region FIELDS


        protected Vector2 position;
        protected Rectangle hitBox;
        protected Texture2D texture;
        protected Director director;

        protected bool isWalkable;


        protected bool over = false;


        #endregion





        public bool getOver()
        {
            return over;
        }



        #region SETS



        public void setPosition(Vector2 posT)
        {
            position = posT;

        }


        #endregion






        #region GETS


        public Vector2 getPosition()
        {
            return position;
        }


        public Vector2 getCenterPosition()
        {


            return new Vector2(hitBox.Center.X, hitBox.Center.Y);
        }


        public bool canWalkThrough()
        {
            return isWalkable;
        }

        public Rectangle getHitBox()
        {
            return hitBox;
        }




        #endregion





    


     


       



 


        public LevelObject(Director d , Vector2 positionT)
        {
            director = d;

            position = positionT;

        }





        public virtual void load(ContentManager content)
        {

            texture = content.Load<Texture2D>("Map//Objects//crash");



            
       
        }

        protected void setUpHitBox()
        {
            hitBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }



        public virtual void update()
        {



          


           




 
         
        }




    


       


        public virtual void draw(SpriteBatch sb)
        {

            if (Camera.onScreen(position))
            {

                sb.Draw(texture, position, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);

            }

           // TextManager.drawTextBorder(director.font, sb, "Current Speed: " + currentSpeed, Color.Black, Color.White, 1, 0, new Vector2(position.X -100, position.Y - 100));

           // drawHitBox(sb);
        }



        protected  void drawHitBox(SpriteBatch sb)
        {


            sb.Draw(texture, new Vector2(hitBox.X, hitBox.Y), null, Color.Red, 0, new Vector2(0, 0), 0.1f, SpriteEffects.None, 1);
            sb.Draw(texture, new Vector2(hitBox.X + hitBox.Width, hitBox.Y), null, Color.Red, 0, new Vector2(0, 0), 0.1f, SpriteEffects.None, 1);
            sb.Draw(texture, new Vector2(hitBox.X, hitBox.Y + hitBox.Height), null, Color.Red, 0, new Vector2(0, 0), 0.1f, SpriteEffects.None, 1);
            sb.Draw(texture, new Vector2(hitBox.X + hitBox.Width, hitBox.Y + hitBox.Height), null, Color.Red, 0, new Vector2(0, 0), 0.1f, SpriteEffects.None, 1);


        }


    }
}