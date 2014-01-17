using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using DungeonCrawl.Engine.Characters;
using Microsoft.Xna.Framework;

namespace DungeonCrawl.Engine.Spells
{
    class SpellManager
    {



        private Director director;


   

        ContentManager content;

        float mana = 100;
        float manaMax = 100;

        //Spells
        List<FireBall> fireballs = new List<FireBall>();

   


        public float getMana()
        {
            return mana;
        }

        public float getManaMax()
        {
            return manaMax;
        }

        public SpellManager(Director directorT, ContentManager contentT)
        {
            director = directorT;
            content = contentT;



        }





        #region addSpells




        public void castFireBall(Character player)
        {
            

            if (canCast(FireBall.cost))
            {

                

                mana -= FireBall.cost;

                director.getEffectManager().addFlame(player.getPosition(), 30, player.getRotation(), player.getSpeed());

                for (int i = 0; i < 30; i++)
                {
                    director.getEffectManager().addFlame(player.getPosition(), i, player.getRotation() + (float)Director.random.NextDouble() / 4, player.getSpeed());
                    director.getEffectManager().addFlame(player.getPosition(), i, player.getRotation() - (float)Director.random.NextDouble() / 4, player.getSpeed());
                }

                FireBall ball = new FireBall(director, player.getPosition(), player.getRotation());
                ball.load(content);
                fireballs.Add(ball);


                MainData.soundBank.PlayCue("castSpell");
                director.player.defend();
                director.player.defendPush();
                director.getRumbleManager().addVibrate(15, 1f);

            }
        }




        private bool canCast(int cost)
        {
            if (mana >= cost)
            {
                return true;
            }
            return false;
        }





#endregion




        public void udpate()
        {



     

            if (mana <= manaMax)
            {
                mana += 0.1f;
            }

            updateFireBalls();
        }



        private void updateFireBalls()
        {
            foreach (FireBall f in fireballs)
            {
                f.update();
            }


        }
        



        public void draw(SpriteBatch sb)
        {

    
            drawFireBall(sb);


            
        }



        private void drawFireBall(SpriteBatch sb)
        {
            foreach(FireBall f in fireballs)
            {
                f.draw(sb);
            }


        }

    }
}
