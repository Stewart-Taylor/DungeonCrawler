using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using DungeonCrawl.Engine.Characters;

namespace DungeonCrawl.Engine
{
    class Sword : Item
    {

        #region FIELDS

       




        #endregion







        public Sword(Director d, Player playerT) : base(d, playerT)
        {
            director = d;

            player = playerT;

            //  setUpPosition(player.);

        }

     



        public override void load(ContentManager content)
        {

            texture = content.Load<Texture2D>("Items//Sword");




            setUpHitBox();
        }


    

    










        public virtual void draw(SpriteBatch sb)
        {


            sb.Draw(texture, position, null, Color.White, player.getRightArm().rotation, new Vector2(64, 64), 1, SpriteEffects.None, 1);



            // TextManager.drawTextBorder(director.font, sb, "Current Speed: " + currentSpeed, Color.Black, Color.White, 1, 0, new Vector2(position.X -100, position.Y - 100));

         //   drawHitBox(sb);
        }





    }
}