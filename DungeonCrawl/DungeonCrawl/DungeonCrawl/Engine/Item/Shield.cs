using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DungeonCrawl.Engine.Characters;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace DungeonCrawl.Engine
{
    class Shield : Item
    {

        #region FIELDS






        #endregion







        public Shield(Director d, Player playerT)
            : base(d, playerT)
        {
            director = d;

            player = playerT;

            //  setUpPosition(player.);

        }





        public override void load(ContentManager content)
        {

            texture = content.Load<Texture2D>("Items//Shield");




            setUpHitBox();
        }















        public override void draw(SpriteBatch sb)
        {


            sb.Draw(texture, position, null, Color.White, player.getLeftArm().rotation, new Vector2(64, 64), 1, SpriteEffects.None, 1);



            // TextManager.drawTextBorder(director.font, sb, "Current Speed: " + currentSpeed, Color.Black, Color.White, 1, 0, new Vector2(position.X -100, position.Y - 100));

           // drawHitBox(sb);
        }





    }
}