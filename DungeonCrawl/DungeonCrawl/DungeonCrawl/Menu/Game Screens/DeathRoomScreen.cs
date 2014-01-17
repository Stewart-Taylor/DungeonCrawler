using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using DungeonCrawl.Menu.Screen_Manager;
using DungeonCrawl.Menu.Screens;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using DungeonCrawl.Engine;

namespace DungeonCrawl.Menu
{
    class DeathRoomScreen : GameScreen
    {

        private Texture2D backgroundTexture;
        private Texture2D graveTexture;
        Vector2 gravePosition = new Vector2(555, 115);


        private Texture2D sparkleTexture;

        public Texture2D coinTexture;

        List<Coin> coins = new List<Coin>();
        List<Coin> coinsDisplay = new List<Coin>();
        List<Sparkle> sparkles = new List<Sparkle>();

        private Texture2D darkness;
        int timer;
        int limit = 500;
        Color darkColor = Color.White;


        int level;

        

        public struct Coin
        {
            public Vector2 position;
            public float scale;

        }


        public struct Sparkle
        {
            public Vector2 position;
            public float scale;
            public int timer;
            public int limit;
            public Color color;

        }

        public DeathRoomScreen(ScreenManager s , int amount , int levelT) : base(s)
        {
            sManager = s;

            level = levelT;
            
            addCoins(amount);
        }


        private void addCoins(int amount)
        {

            Random ran = new Random();

            for(int i = 0  ; i < amount ; i++)
            {
                Coin coin = new Coin();

                coin.position = coinPlacement();


                float percent = (float)coin.position.Y / (float)710;
                coin.scale = percent;

                coins.Add(coin);
            }

        }


        private Vector2 coinPlacement()
        {
            Vector2 positionTemp = new Vector2(0, 0);
            positionTemp.Y = Director.random.Next(265, 710);

            if (Director.random.Next(100) < 50)
            {
                positionTemp.X = Director.random.Next(110, 530);
            }
            else
            {
                positionTemp.X = Director.random.Next(870, 1270);
            }



            return positionTemp;

        }


        public override void load(ContentManager content)
	{
		
		
	

            //Load Background
        backgroundTexture = content.Load<Texture2D>("GameScreens//DeathRoom");
        coinTexture = content.Load<Texture2D>("GameScreens//FloorCoin");
        graveTexture = content.Load<Texture2D>("GameScreens//grave");
        darkness = content.Load<Texture2D>("GameScreens//Darkness");
        sparkleTexture = content.Load<Texture2D>("GameScreens//Sparkle"); 


	}




        private void addSparkle()
        {

            if (sparkles.Count < 100)
            {
                if (coinsDisplay.Count > 0)
                {
                    int i = Director.random.Next(0, coinsDisplay.Count - 1);

                    Vector2 pos;
                    pos = coinsDisplay[i].position;


                    Sparkle spark = new Sparkle();
                    spark.limit = Director.random.Next(5, 10);
                    spark.scale = 0.01f;
                    // spark.scale = coins[i].scale;

                    spark.position = pos;
                    spark.color = Color.White;

                    sparkles.Add(spark);
                }

            }

        }




        public override void update()
        {
            if (Director.random.Next(10) == 5)
            {
                addSparkle();
            }

            upadteSparkles();

            timer++;


            if (timer < limit / 2)
            {
                float percent = (float)timer / (float)limit;
                darkColor.A = (byte)(percent * 250);

            }
            else if (timer < limit)
            {
                float percent = (float)timer / (float)limit;
                darkColor.A = (byte)((250 - (percent * 250)) + 10);

            }
            else
            {
                timer = 0;

            }


            if (coins.Count > 0)
            {

                Coin c = coins.Last();

                coinsDisplay.Add(c);
                coins.Remove(c);

            }

            input();

        }




        private void upadteSparkles()
        {
            List<Sparkle> deads = new List<Sparkle>();

            for(int i = 0 ; i < sparkles.Count ; i++)
            {
                Sparkle spark = sparkles[i];
                if (spark.timer < spark.limit)
                {
                    spark.scale += 0.05f;
                    spark.timer++;

                    float percent = (float)spark.timer / (float)spark.limit;
                    spark.color.A = (byte)((250 - (percent * 250)) + 10);



                }
                else
                {
                    deads.Add(spark);
                }
                sparkles[i] = spark;
            }


            foreach (Sparkle s in deads)
            {
                sparkles.Remove(s);
            }

            deads.Clear();
        }



        public void input()
        {


            //check for button press


           

            if (sManager.input.IsNewPress(Keys.Enter))
            {

                MainData.soundBank.PlayCue("menuClick");
                sManager.removeScreen(this);

            }


            if (sManager.input.IsNewPress(Buttons.A))
            {
                MainData.soundBank.PlayCue("menuClick");

                sManager.removeScreen(this);

            }

        
         


        }





      



        public override void draw(SpriteBatch sb)
        {

            sb.Draw(backgroundTexture, Vector2.Zero, null, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 1);





            foreach (Coin coin in coinsDisplay)
            {
                sb.Draw(coinTexture, coin.position, null, Color.White, 0, new Vector2(0, 0), coin.scale, SpriteEffects.None, 1);
            }

            drawSparks(sb);

            sb.Draw(graveTexture, gravePosition, null, new Color(0,0,0,150), 0, new Vector2(0, 0), 1.03f, SpriteEffects.None, 1);
            sb.Draw(graveTexture, gravePosition, null, Color.White, 0, new Vector2(0, 0), 1f, SpriteEffects.None, 1);


            TextManager.drawTextBorder(sManager.font, sb, coinsDisplay.Count.ToString(), new Color(0,0,0,150), Color.Khaki, 1.5f, 0, new Vector2(705, 365));

            sb.Draw(darkness, Vector2.Zero, null, darkColor, 0, new Vector2(0, 0), 1f, SpriteEffects.None, 1);




            // TextManager.drawTextBorder(sManager.font, sb, "Fell at Level " + level.ToString(), new Color(0, 0, 0, 150), Color.Khaki, 1.1f, 0, new Vector2(705, 550));
            
        }



        private void drawSparks(SpriteBatch sb)
        {
            sb.End();
            sb.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);


            foreach (Sparkle spark in sparkles)
            {
                if (spark.timer < spark.limit)
                {
                    sb.Draw(sparkleTexture, spark.position, null, spark.color, 0, new Vector2(0, 0), spark.scale, SpriteEffects.None, 1);
                }
            }

            sb.End();
            sb.Begin();
        }
    }
}

    





