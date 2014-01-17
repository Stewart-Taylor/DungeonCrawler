#region USING
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DungeonCrawl.Menu.Screen_Manager;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using DungeonCrawl.Engine;

#endregion

namespace DungeonCrawl.Menu.Screens
{
    class MessageBoxScreen : GameScreen
    {

        #region FIELDS

        private Texture2D backgroundTexture;
        private Vector2 position = new Vector2(300, 400);

        private String messageText;

        private GameScreen hostScreen;

        private List<MenuButton> buttons = new List<MenuButton>();

        private MenuButton accept;
        private MenuButton cancel;

        private bool exitGame = false;


        #endregion



        public MessageBoxScreen(String message ,GameScreen screen , ScreenManager s , bool exitApp) : base(s)
        {
            sManager = s;
            hostScreen = screen;
            messageText = message;
            isPopUpScreen = true;
            exitGame = exitApp;
        }



        public override void load(ContentManager content)
    	{
		
            //Load Background
            backgroundTexture = content.Load<Texture2D>("Menu//MainMenu//ExitMenu");


            //Set up buttons
            accept = new MenuButton("Menu//MessageBox//AcceptButton", new Vector2(position.X + 580, position.Y + 100));
            cancel = new MenuButton("Menu//MessageBox//CancelButton", new Vector2(position.X + 30, position.Y + 100));

     		 buttons.Add(accept);
		     buttons.Add(cancel);
		
             foreach(MenuButton b in  buttons)
             {
                    b.load(content);
             }
	}





        public override void update()
        {
            if (sManager.input.IsNewPress(Keys.Escape))
            {
                sManager.removeScreen(this);
                MainData.soundBank.PlayCue("menuClick");
            }
            
            input();
        }




        public void input()
        {
            //check for button press
            foreach (MenuButton b in buttons)
            {
                if (b.getHitBox().Contains((int)sManager.input.MousePosition.X, (int)sManager.input.MousePosition.Y))
                {
                    b.setHighlighted(true);
                }
                else
                {
                    b.setHighlighted(false);
                }
            }

            buttonSelected();

            if (MainData.xboxMode == true)
            {

                if (sManager.input.IsNewPress(Buttons.A))
                {
                    MainData.soundBank.PlayCue("menuClick");
                    if (exitGame == true)
                    {
                        sManager.game.Exit();
                    }
                    else
                    {
                        sManager.openMainMenu();
                    }
                }
                else if (sManager.input.IsNewPress(Buttons.B))
                {
                    MainData.soundBank.PlayCue("menuClick");
                    sManager.removeScreen(this);
                }

            }


        }



        private void buttonSelected()
        {
            if (sManager.input.IsNewPress(MouseButtons.LeftButton))
            {

                if (accept.isButtonHighlighted())
                {
                    MainData.soundBank.PlayCue("menuClick");
                    if (exitGame == true)
                    {
                        sManager.game.Exit();
                    }
                    else
                    {
                        sManager.openMainMenu();
                    }
                }
                else if (cancel.isButtonHighlighted())
                {
                    MainData.soundBank.PlayCue("menuClick");
                    sManager.removeScreen(this);
                }
            }
        }

      



        public override void draw(SpriteBatch sb)
        {

            //draw host screen
            hostScreen.draw(sb);

            sb.Draw(backgroundTexture, position, null, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 1);

          //  sb.DrawString(sManager.font, messageText, new Vector2(position.X +100, position.Y + 40), Color.White);
            TextManager.drawTextBorder(sManager.font, sb, messageText, Color.Black, Color.Gold, 1, 0, new Vector2(position.X + 100, position.Y + 40), Vector2.Zero);

            foreach (MenuButton b in buttons)
            {
                b.draw(sb);
            }

        }


    }
}
