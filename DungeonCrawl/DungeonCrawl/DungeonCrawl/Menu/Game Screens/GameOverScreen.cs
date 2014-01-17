using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DungeonCrawl.Menu.Screens;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using DungeonCrawl.Engine;
using DungeonCrawl.Menu.Screen_Manager;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace DungeonCrawl.Menu.Game_Screens
{
    class GameOverScreen : GameScreen
    {

        #region FIELDS

        private Texture2D backgroundTexture;
        
        private Vector2 position = new Vector2(0, 0);

        private GameScreen hostScreen;


        private Director director;

  
        

     

        #endregion


        public GameOverScreen(GameScreen screen ,ScreenManager s , Director directorT) : base(s)
        {
            director = directorT;

            sManager = s;
            hostScreen = screen;

            isPopUpScreen = true;

        }



        public override void load(ContentManager content)
	    {
            //Load Background
            backgroundTexture = content.Load<Texture2D>("GameScreens//GameOverScreen");


         
	    }




        public override void update()
        {
            input();

        }





        public void input()
        {
            if (sManager.input.IsNewPress(Keys.Enter))
            {
                director.newLevel();
                sManager.removeScreen(hostScreen);
                sManager.removeScreen(this);
                MainData.soundBank.PlayCue("menuClick");
                sManager.addScreen(new DeathRoomScreen(sManager , director.getPlayer().getGold() ,  director.getDungeonLevel() -1 ));
            }


            if (sManager.input.IsNewPress(Buttons.A))
            {
                director.newLevel();
                sManager.removeScreen(hostScreen);
                sManager.removeScreen(this);
                MainData.soundBank.PlayCue("menuClick");
                sManager.addScreen(new DeathRoomScreen(sManager, director.getPlayer().getGold(), director.getDungeonLevel()));
            }
            
        }







        public override void draw(SpriteBatch sb)
        {

            //draws host screen first
            hostScreen.draw(sb);


            sb.Draw(backgroundTexture, position, null, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 1);

           

        }


    }
}

    
