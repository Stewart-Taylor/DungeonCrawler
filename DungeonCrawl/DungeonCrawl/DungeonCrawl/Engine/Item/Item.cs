using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using DungeonCrawl.Engine.Characters;

namespace DungeonCrawl.Engine
{
    class Item
    {

        #region FIELDS

        protected Vector2 position;
        protected Vector2 origin = new Vector2(64, 64);
        protected Rectangle hitBox;
        protected Texture2D texture;
        protected Director director;
       



        protected Player player;


        #endregion







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

        public Rectangle getHitBox()
        {
            return hitBox;
        }




        #endregion





    


     


       



 


        public Item(Director d, Player playerT)
        {
            director = d;

            player = playerT;

          //  setUpPosition(player.);

        }

        protected void setUpPosition(Vector2 pos)
        {
            position = pos;
            position.X += 64;
            position.Y += 64;

        }



        public virtual void load(ContentManager content)
        {

            texture = content.Load<Texture2D>("Items//Item");




            setUpHitBox();
        }


        protected void setUpHitBox()
        {
            //set up hitbox
            hitBox = new Rectangle((int)position.X + 115, (int)position.Y + 88, 96, 40);
        }


        public virtual void update()
        {



            updateHitBox();


            position = player.getPosition();




 
         
        }




        protected void updateHitBox()
        {
            hitBox.X = (int)position.X - 50;
            hitBox.Y = (int)position.Y - 50;
        }




       


        public virtual void draw(SpriteBatch sb)
        {


            sb.Draw(texture, position, null, Color.White, player.getRightArm().rotation, new Vector2(64, 64), 1, SpriteEffects.None, 1);



           // TextManager.drawTextBorder(director.font, sb, "Current Speed: " + currentSpeed, Color.Black, Color.White, 1, 0, new Vector2(position.X -100, position.Y - 100));

            drawHitBox(sb);
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