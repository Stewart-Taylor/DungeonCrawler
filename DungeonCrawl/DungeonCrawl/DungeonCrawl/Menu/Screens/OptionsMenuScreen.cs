using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DungeonCrawl.Menu.Screen_Manager;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using DungeonCrawl.Engine;

namespace DungeonCrawl.Menu.Screens
{
    class OptionsMenuScreen : GameScreen
    {

        #region FIELDS

        private Texture2D backgroundTexture;


        //Buttons
        private MenuButton gamePad;
        private MenuButton developerMode;
        private MenuButton rumble;


        private MenuButton selected;
        int selectedPointer = 0;


        private List<MenuButton> buttons = new List<MenuButton>();

        #endregion





        public OptionsMenuScreen(ScreenManager s) : base(s)
        {
            sManager = s;

        }



        public override void load(ContentManager content)
	    {

        //Load Background
        backgroundTexture = content.Load<Texture2D>("Menu//MainMenu//MainMenuBackground");
      


        //Offsets for positioning buttons
        float positionX = 400;
        float positionY = 200;
        float yGap = 100;

        gamePad = new MenuButton("Menu//MainMenu//PlayButton", new Vector2(positionX, positionY  + (0 * yGap)));
        developerMode = new MenuButton("Menu//MainMenu//OptionsButton", new Vector2(positionX, positionY + (1 * yGap)));
        rumble = new MenuButton("Menu//MainMenu//AboutButton", new Vector2(positionX, positionY + (2 * yGap)));
       


        //add buttons to list
        buttons.Add(gamePad);
        buttons.Add(developerMode);
        buttons.Add(rumble);
    


        foreach (MenuButton b in buttons)
        {
            b.load(content);
        }

        selected = gamePad;

	}




        public override void update()
        {
            if (sManager.input.IsNewPress(Keys.Escape))
            {
                MainData.soundBank.PlayCue("menuClick");
                sManager.removeScreen(this);
            }
            if (sManager.input.IsNewPress(Buttons.B))
            {
                MainData.soundBank.PlayCue("menuClick");
                sManager.removeScreen(this);
            }

            input();

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
                if (gamePad.isButtonHighlighted())
                {
                    gamePadSelected();
                }
                else if (developerMode.isButtonHighlighted())
                {
                    devModeSelected();
                }
                else if (rumble.isButtonHighlighted())
                {
                    rumbleSelected();
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
                if (selectedPointer < buttons.Count - 1)
                {
                    selectedPointer++;
                }

            }

            highlightUpdate();


            if (sManager.input.IsNewPress(MouseButtons.LeftButton))
            {
                MainData.xboxMode = false;
            }

        }



        private void gamePadSelected()
        {
            MainData.soundBank.PlayCue("menuClick");
            MainData.xboxMode = !MainData.xboxMode;
        }

        private void devModeSelected()
        {
            MainData.soundBank.PlayCue("menuClick");
            MainData.developerMode = !MainData.developerMode;
        }

        private void rumbleSelected()
        {
            MainData.soundBank.PlayCue("menuClick");
            MainData.rumbleOn = !MainData.rumbleOn;
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
                sManager.removeScreen(this);
            }

            if (sManager.input.IsNewPress(MouseButtons.LeftButton))
            {
                if (gamePad.isButtonHighlighted())
                {
                    gamePadSelected();
                }
                else if (developerMode.isButtonHighlighted())
                {
                    devModeSelected();
                }
                else if (rumble.isButtonHighlighted())
                {
                    rumbleSelected();
                }
            }
        }





      



        public override void draw(SpriteBatch sb)
        {

            //draws host screen first
          

      
            sb.Draw(backgroundTexture, Vector2.Zero, null, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 1);

            TextManager.drawTextBorder(sManager.font, sb, "Options", Color.Black, Color.White, 1, 0, new Vector2(600, 100), Vector2.Zero);

            TextManager.drawTextBorder(sManager.font, sb, "You can press F1 in game to change between GamePad or PC Controls!", Color.Black, Color.White, 1, 0, new Vector2(300, 600), Vector2.Zero);
           
            foreach (MenuButton b in buttons)
            {
                b.draw(sb);
            }


            float yOffSet = 10;
            float xOffSet = 150;

            //draw Text
            if (MainData.xboxMode)
            {
                TextManager.drawTextBorder(sManager.font, sb, "GamePad (ON) ", Color.Black, Color.White, 1, 0, new Vector2(gamePad.getPosition().X + xOffSet, gamePad.getPosition().Y + yOffSet), Vector2.Zero);
            }
            else
            {
                TextManager.drawTextBorder(sManager.font, sb, "GamePad (OFF) ", Color.Black, Color.White, 1, 0, new Vector2(gamePad.getPosition().X + xOffSet, gamePad.getPosition().Y + yOffSet), Vector2.Zero);
            }

            if (MainData.developerMode)
            {
                TextManager.drawTextBorder(sManager.font, sb, "Developer Mode (ON) ", Color.Black, Color.White, 1, 0, new Vector2(developerMode.getPosition().X + xOffSet, developerMode.getPosition().Y + yOffSet), Vector2.Zero);
            }
            else
            {
                TextManager.drawTextBorder(sManager.font, sb, "Developer Mode (OFF) ", Color.Black, Color.White, 1, 0, new Vector2(developerMode.getPosition().X + xOffSet, developerMode.getPosition().Y + yOffSet), Vector2.Zero);
            }


            if (MainData.rumbleOn)
            {
                TextManager.drawTextBorder(sManager.font, sb, "Vibrate (ON) ", Color.Black, Color.White, 1, 0, new Vector2(rumble.getPosition().X + xOffSet, rumble.getPosition().Y + yOffSet), Vector2.Zero);
            }
            else
            {
                TextManager.drawTextBorder(sManager.font, sb, "Vibrate (OFF) ", Color.Black, Color.White, 1, 0, new Vector2(rumble.getPosition().X + xOffSet, rumble.getPosition().Y + yOffSet), Vector2.Zero);
            }




        }


    }
}

    




