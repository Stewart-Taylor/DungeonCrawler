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
    class Spike : LevelObject
    {



       

        private Texture2D upTexture;


        int timer = 0;
        int limit = 200;

        int damage = 3;

        bool setOff = false;
        bool spikesUp = false;



        public Spike(Director directorT , Vector2 positionT) : base(directorT , positionT)
        {
            position = positionT;
            director = directorT;
        }




        public override void load(ContentManager content)
        {
            texture = content.Load<Texture2D>("Traps//SpikeDown");
            upTexture = content.Load<Texture2D>("Traps//SpikeUp");

            isWalkable = true;

            hitBox = new Rectangle((int)position.X + 21, (int)position.Y + 21, 87, 87);
        }




        private void checkForTrigger()
        {


            foreach (Character c in director.getCharacters())
            {
                if (c.isAlive())
                {
                    if (Vector2.Distance(c.getPosition(), getCenterPosition()) < 100)
                    {
                        triggerTrap();
                        break;
                    }
                }
            }

            if (Vector2.Distance(director.getPlayer().getPosition(), getCenterPosition()) < 120)
            {
                triggerTrap();
            }

        }

        private void triggerTrap()
        {
      
                setOff = true;
                spikesUp = true;
                timer = 0;
            
        }


        public override void update()
        {


            if (Camera.onScreen(position))
            {

                if (setOff == false)
                {
                    checkForTrigger();
                }
                else
                {
                    timer++;

                    if (timer < limit / 3)
                    {
                        if (spikesUp)
                        {
                            checkForHit();
                        }
                    }
                    else if (timer < limit)
                    {
                        spikesUp = false;
                    }
                    else
                    {
                        setOff = false;
                    }
                }

            }

        }


        private void checkForHit()
        {
            foreach (Character c in director.getCharacters())
            {
                if (c.isAlive())
                {
                    if (c.getHitBox().Intersects(hitBox))
                    {
                        trapDamage(c);
                        break;
                    }
                }
            }

            if (director.getPlayer().getHitBox().Intersects(hitBox))
            {
                trapDamage(director.getPlayer());
            }

        }



        private void trapDamage(Character person)
        {
            if (person is Player)
            {
                person.damage(damage);
                person.damage(0);
                person.damage(0);
            }
            else
            {
                person.damage(damage * 2);
            }

            spikesUp = false;
        }

        public override void draw(SpriteBatch sb)
        {

            if (Camera.onScreen(position))
            {

                if (spikesUp == false)
                {

                    sb.Draw(texture, position, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);

                }
                else
                {
                    sb.Draw(upTexture, position, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
                }

            }
           
        }






    }
}


