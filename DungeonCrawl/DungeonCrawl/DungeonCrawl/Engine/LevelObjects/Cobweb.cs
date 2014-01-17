using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace DungeonCrawl.Engine.LevelObjects
{
    class Cobweb: LevelObject
    {



        float rotation;
      






        public Cobweb(Director directorT , Vector2 positionT) : base(directorT , positionT)
        {
            position = positionT;
            rotation = Director.random.Next(300);
            
            director = directorT;
        }




        public override void load(ContentManager content)
        {
            texture = content.Load<Texture2D>("Map//Objects//Cobweb");


            isWalkable = false;

            setUpHitBox();
            position += new Vector2(64, 64);
        }




   


        public override void draw(SpriteBatch sb)
        {




            sb.Draw(texture, position, null, new Color(255, 255, 255, 40), rotation, new Vector2(64, 64), 1, SpriteEffects.None, 1);

                drawHitBox(sb);
            


        }





    }
}

