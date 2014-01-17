#region USING
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using DungeonCrawl.Menu.Screen_Manager;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using DungeonCrawl.Engine;

#endregion


namespace DungeonCrawl.Menu.Screens
{
    class PauseScreen : GameScreen
    {

        #region FIELDS

        private Texture2D backgroundTexture;
        private Texture2D pauseFade;
        private Vector2 position = new Vector2(490, 200);

        private GameScreen hostScreen;


        private int pauseTimer;
        private int pauseLimit = 50;
        private Color fadeColor = new Color(255, 255, 255, 0);
        

        //Buttons
        private MenuButton continueButton  ;
        private MenuButton options;
        private MenuButton help;
        private MenuButton exit;


        private MenuButton selected;
        int selectedPointer = 0;


        private List<MenuButton> buttons = new List<MenuButton>();

        #endregion


        public PauseScreen(GameScreen screen ,ScreenManager s) : base(s)
        {
            sManager = s;
            hostScreen = screen;

            isPopUpScreen = true;

        }



        public override void load(ContentManager content)
	    {
            //Load Background
            backgroundTexture = content.Load<Texture2D>("Menu//PauseMenu//PauseMenu");
            pauseFade = content.Load<Texture2D>("Menu//PauseMenu//PauseFade");



            //Offsets for positioning buttons
            const int xOffset = 100;
            const int yOffset = 70;

            continueButton = new MenuButton("Menu//MainMenu//PlayButton", new Vector2(position.X + xOffset, position.Y + yOffset ));
            options = new MenuButton("Menu//MainMenu//OptionsButton", new Vector2(position.X + xOffset, position.Y + yOffset * 2));
            help = new MenuButton("Menu//MainMenu//AboutButton", new Vector2(position.X + xOffset, position.Y + yOffset * 3));
            exit = new MenuButton("Menu//MainMenu//ExitButton", new Vector2(position.X + xOffset, position.Y + yOffset * 4));


            //add buttons to list
            buttons.Add(continueButton);
            buttons.Add(options);
            buttons.Add(help);
            buttons.Add(exit);


            foreach (MenuButton b in buttons)
            {
                b.load(content);
            }

            selected = continueButton;
	    }




        public override void update()
        {
            if (sManager.input.IsNewPress(Keys.Escape))
            {
                MainData.soundBank.PlayCue("menuClick");
                sManager.removeScreen(this);
            }
            
            input();



            if (pauseTimer <= pauseLimit)
            {
                pauseTimer++;


                float percent = (float)pauseTimer / (float)pauseLimit;
                fadeColor.A = (byte)(percent * 250);

            }
        }





        public void input()
        {
            if (MainData.xboxMode)
            {
                xboxInput();
            }
            else
            {
                pcInput();
            }

        }






        private void xboxInput()
        {
            selected.setHighlighted(true);


            if (sManager.input.IsNewPress(Buttons.Start))
            {
                MainData.soundBank.PlayCue("menuClick");
                sManager.removeScreen(this);
            }

            if (sManager.input.IsNewPress(Buttons.A))
            {
                if (continueButton.isButtonHighlighted())
                {
                    MainData.soundBank.PlayCue("menuClick");
                    sManager.removeScreen(this);
                }
                else if (options.isButtonHighlighted())
                {
                    MainData.soundBank.PlayCue("menuClick");
                    sManager.addScreen(new OptionsMenuScreen(sManager));
                }
                else if (help.isButtonHighlighted())
                {
                    MainData.soundBank.PlayCue("menuClick");
                    sManager.addScreen(new AboutMenuScreen(sManager));
                }
                else if (exit.isButtonHighlighted())
                {
                    MainData.soundBank.PlayCue("menuClick");
                    sManager.addScreen(new MessageBoxScreen("Are you sure you want to quit?", this, sManager, false));
                }

            }

            if (sManager.input.IsNewPress(Buttons.LeftThumbstickUp))
            {
                if (selectedPointer > 0)
                {
                    selectedPointer--;
                }

            }

            if (sManager.input.IsNewPress(Buttons.LeftThumbstickDown))
            {
                if (selectedPointer < buttons.Count -1)
                {
                    selectedPointer++;
                }

            }

            highlightUpdate();

        }


        private void highlightUpdate()
        {
            foreach (MenuButton b in buttons)
            {
                if (b != selected)
                {
                    b.setHighlighted(false);
                }
            }

            selected = buttons[selectedPointer];
            selected.setHighlighted(true);
        }




        private void pcInput()
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

        }



        private void buttonSelected()
        {
            //if highlighted button is clicked

            if (sManager.input.IsNewPress(Keys.Escape))
            {
                MainData.soundBank.PlayCue("menuClick");
                sManager.removeScreen(this);
            }

            if (sManager.input.IsNewPress(MouseButtons.LeftButton))
            {
                if (continueButton.isButtonHighlighted())
                {
                    MainData.soundBank.PlayCue("menuClick");
                    sManager.removeScreen(this);
                }
                else if (options.isButtonHighlighted())
                {
                    MainData.soundBank.PlayCue("menuClick");
                    sManager.addScreen(new OptionsMenuScreen(sManager));
                }
                else if (help.isButtonHighlighted())
                {
                    MainData.soundBank.PlayCue("menuClick");
                    sManager.addScreen(new AboutMenuScreen(sManager));
                }
                else if (exit.isButtonHighlighted())
                {
                    MainData.soundBank.PlayCue("menuClick");
                    sManager.addScreen(new MessageBoxScreen("Are you sure you want to quit?", this, sManager , false));
                }
            }
        }



        public override void draw(SpriteBatch sb)
        {

            //draws host screen first
            hostScreen.draw(sb);

            sb.Draw(pauseFade, Vector2.Zero, null, fadeColor, 0, new Vector2(0, 0), 1, SpriteEffects.None, 1);
            sb.Draw(backgroundTexture, position, null, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 1);

            TextManager.drawTextBorder(sManager.font, sb, "Paused", Color.Black, Color.White, 1, 0, new Vector2(position.X + 120, position.Y + 10), Vector2.Zero);


            foreach (MenuButton b in buttons)
            {
                b.draw(sb);
            }

        }


    }
}

    
