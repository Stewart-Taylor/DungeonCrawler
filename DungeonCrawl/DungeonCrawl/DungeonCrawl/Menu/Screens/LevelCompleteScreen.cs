using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using DungeonCrawl.Menu.Screen_Manager;
using DungeonCrawl.Engine;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Threading;

namespace DungeonCrawl.Menu.Screens
{
    class LevelCompleteScreen : GameScreen
    {

        #region FIELDS

        private Texture2D backgroundTexture;
        private Texture2D pauseFade;
        private Vector2 position = new Vector2(490, 200);

        private GameScreen hostScreen;


        private Director director;

  
        

        //Buttons
        private MenuButton continueButton  ;
        private MenuButton options;
        private MenuButton help;
        private MenuButton exit;


        private List<MenuButton> buttons = new List<MenuButton>();

        #endregion


        public LevelCompleteScreen(GameScreen screen ,ScreenManager s , Director directorT) : base(s)
        {
            director = directorT;

            sManager = s;
            hostScreen = screen;

            isPopUpScreen = true;

        }



        public override void load(ContentManager content)
	    {
            //Load Background
            backgroundTexture = content.Load<Texture2D>("GameScreens//LevelCompleteScreen");


         
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
                sManager.removeScreen(this);
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

    
