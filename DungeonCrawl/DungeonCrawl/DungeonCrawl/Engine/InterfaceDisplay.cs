using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace DungeonCrawl.Engine
{
    class InterfaceDisplay
    {


        #region FIELDS


        private Director director;

        private SpriteFont font;

        private int level;
        private Vector2 levelPosition = new Vector2(1060, 260);

        private Texture2D heartTexture;
        private Vector2 heartPosition = new Vector2(50, 30);

        private Texture2D goldTexture;
        private Vector2 goldPosition = new Vector2(850, 30);

        private int health;
        private int gold;

        #endregion






        public InterfaceDisplay(Director directorT)
        {

            director = directorT;



        }




        public void load(ContentManager content)
        {

            font = director.font;

            heartTexture = content.Load<Texture2D>("Interface//Heart");
            goldTexture = content.Load<Texture2D>("Interface//gold");

        }



        #region Update



        public void update()
        {


            level = director.getDungeonLevel();
            health = director.getPlayer().getHealth();
            gold = director.getPlayer().getGold();
        }




        #endregion







        #region DRAW


        public void draw(SpriteBatch sb)
        {


            drawHealth(sb);

            drawTreasure(sb);
            drawLevel(sb);
        }




        private void drawHealth(SpriteBatch sb)
        {

            sb.Draw(heartTexture, heartPosition, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
            TextManager.drawTextBorder(font, sb, health.ToString(), Color.Black, Color.White, 1.4f, 0, new Vector2(heartPosition.X + 35, heartPosition.Y + 20), Vector2.Zero);


        }


        private void drawLevel(SpriteBatch sb)
        {

           
            TextManager.drawTextBorder(font, sb, "Level : " + level.ToString(), Color.Black, Color.White, 1.4f, 0, levelPosition, Vector2.Zero);
        }




        private void drawTreasure(SpriteBatch sb)
        {
            sb.Draw(goldTexture, goldPosition, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
            TextManager.drawTextBorder(font, sb,  gold.ToString(), Color.Black, Color.Gold, 1.4f, 0, new Vector2(goldPosition.X + 55, goldPosition.Y), Vector2.Zero);

        }

        #endregion



    }
}
