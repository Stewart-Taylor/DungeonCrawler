using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace DungeonCrawl.Engine.LevelObjects
{
    class WallTorch : LevelObject
    {











        Texture2D glowTexture;



        public WallTorch(Director directorT , Vector2 positionT) : base(directorT , positionT)
        {
            position = positionT;
            director = directorT;

            over = true;
        }




        public override void load(ContentManager content)
        {
            texture = content.Load<Texture2D>("Map//Objects//WallTorch");
            glowTexture = content.Load<Texture2D>("Effects//Fog");

            isWalkable = true;

            setUpHitBox();
        }





        public override void update()
        {
            if (Camera.onScreen(position))
            {
                director.getEffectManager().addTorchFlame(new Vector2(position.X + 64, position.Y + 64), 1);
            }
        }



        public override void draw(SpriteBatch sb)
        {

            if (Camera.onScreen(position))
            {
                sb.Draw(glowTexture, new Vector2(position.X - 250 , position.Y -250), null, new Color(130,30,0,50), 0, Vector2.Zero, 5f, SpriteEffects.None, 1);
                    sb.Draw(texture, position, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
            }

        }





    }
}

