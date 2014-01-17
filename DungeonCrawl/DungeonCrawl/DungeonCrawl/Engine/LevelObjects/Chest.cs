using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonCrawl.Engine.LevelObjects
{
    class Chest : LevelObject
    {



        private bool opened = false;

        private Texture2D openTexture;
        private Texture2D popUp;

        bool showPopUp = false;



        public bool isOpened()
        {
            return opened;
        }



        public Chest(Director directorT , Vector2 positionT) : base(directorT , positionT)
        {
            position = positionT;
            director = directorT;
        }




        public override void load(ContentManager content)
        {
            texture = content.Load<Texture2D>("Map//Objects//Chest");
            openTexture = content.Load<Texture2D>("Map//Objects//ChestOpen");
            popUp = content.Load<Texture2D>("Interface//UsePopUp");

            isWalkable = false;

            setUpHitBox();
        }




        public void open()
        {
            if (opened == false)
            {
                MainData.soundBank.PlayCue("menuClick");
                director.getEffectManager().addFog(new Vector2(hitBox.Center.X , hitBox.Center.Y), 10, Color.Gold, true);
                opened = true;
                isWalkable = true;


                for (int i = 20; i < Director.random.Next(200); i++)
                {
                    Treasure t = new Treasure(director, new Vector2(hitBox.Center.X + Director.random.Next(-100, 100), hitBox.Center.Y + Director.random.Next(-100, 100)));
                    director.addTreasure(t);

                }
            }

        }




        public override void update()
        {
            showPopUp = false;

            if (Vector2.Distance(director.getPlayer().getPosition(), getCenterPosition()) < 150)
            {
                showPopUp = true;
            }


        }



        public override void draw(SpriteBatch sb)
        {

            if (Camera.onScreen(position))
            {

                if (opened == true)
                {
                    sb.Draw(openTexture, new Vector2(position.X - 20, position.Y - 20), null, new Color(0, 0, 0, 150), 0, Vector2.Zero, 1.2f, SpriteEffects.None, 1);
                    sb.Draw(openTexture, position, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
                }
                else
                {
                    sb.Draw(texture, new Vector2(position.X - 20, position.Y - 20), null, new Color(0, 0, 0, 150), 0, Vector2.Zero, 1.2f, SpriteEffects.None, 1);
                    sb.Draw(texture, position, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);

                    if (showPopUp == true)
                    {
                        sb.Draw(popUp, new Vector2(position.X - 30, position.Y - 40), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);

                        TextManager.drawTextBorder(director.font, sb, "Open", Color.Black, Color.White, 1.1f, 0, new Vector2(position.X + 50, position.Y + 5));
                    }
                    //  drawHitBox(sb);
                }

            }

        }





    }
}

