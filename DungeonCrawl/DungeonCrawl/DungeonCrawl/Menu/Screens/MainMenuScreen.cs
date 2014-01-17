#region USING
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using DungeonCrawl.Menu.Screen_Manager;
using Microsoft.Xna.Framework.Content;
using DungeonCrawl.Engine;
using DungeonCrawl.Engine.Effects;
using Microsoft.Xna.Framework.Input;

#endregion

namespace DungeonCrawl.Menu.Screens
{
    class MainMenuScreen : GameScreen
    {

        #region FIELDS

        private Texture2D backgroundTexture;
        private Texture2D gameTitle;
        private Vector2 titlePosition = new Vector2(280, 70);


        ContentManager content;

        //BUTTONS
        private List<MenuButton> buttons = new List<MenuButton>();

        private MenuButton playGame = new MenuButton("Menu//MainMenu//PlayButton", new Vector2(590, 200));
        private MenuButton options = new MenuButton("Menu//MainMenu//OptionsButton", new Vector2(790, 300));
        private MenuButton about = new MenuButton("Menu//MainMenu//AboutButton", new Vector2(390, 300));
        private MenuButton exit = new MenuButton("Menu//MainMenu//ExitButton", new Vector2(590, 600));



        //Sword DATA
        private Texture2D swordTexture;
        private Vector2 swordPosition = new Vector2(650, 470);
        private float swordRotation = 0;
        private Vector2 swordOrigin = new Vector2(70, 200);
        private float swordRotationTarget = 0;
        private float swordSpeed = 0.09f;


        Texture2D torchTexture;
        Vector2 torchPositionLeft = new Vector2(150, 300);
        Vector2 torchPositionRight = new Vector2(1000, 300);

        List<MenuFlame> flames = new List<MenuFlame>();



        int timer = 0;
        int limit = 3;

        private bool newLevel = false;

        #endregion


        public MainMenuScreen(ScreenManager s) : base(s)
        {
            sManager = s;
        }



        public override void load(ContentManager contentT)
	    {
            content = contentT;

              //Load Background
              backgroundTexture = content.Load<Texture2D>("Menu//MainMenu//MainMenuBackground");
              swordTexture = content.Load<Texture2D>("Menu//MainMenu//MenuSword");
              gameTitle = content.Load<Texture2D>("Menu//MainMenu//GameTitle");
              torchTexture = content.Load<Texture2D>("Menu//MainMenu//Torch"); 

            buttons.Add(playGame);
            buttons.Add(options);
            buttons.Add(about);
            buttons.Add(exit);
		
            foreach(MenuButton b in  buttons)
            {
                b.load(content);
            }


            if(MainData.xboxMode == true)
            {
                playGame.setHighlighted(true);
                swordRotateToPoint(new Vector2(playGame.getPosition().X + (int)playGame.getHitBox().Width / 2, playGame.getPosition().Y + (int)playGame.getHitBox().Height / 2));
                }
	}



        private void addFlames()
        {
            for (int i = 0; i < Director.random.Next(1,5); i++)
            {

                //Left
                Vector2 posTemp = new Vector2(torchPositionLeft.X + 65, torchPositionLeft.Y + 5);
                MenuFlame f = new MenuFlame(posTemp, content);
                flames.Add(f);


                //Right
                posTemp = new Vector2(torchPositionRight.X + 65, torchPositionRight.Y + 5);
                f = new MenuFlame(posTemp, content);
                flames.Add(f);

            }

        }


        private void updateFlames()
        {
            List<MenuFlame> deads = new List<MenuFlame>();

            foreach (MenuFlame f in flames)
            {
                if (f.isDead() == false)
                {
                    f.update();
                }
                else
                {
                    deads.Add(f);
                }
            }

            foreach (MenuFlame f in deads)
            {
                flames.Remove(f);
            }

            deads.Clear();



            addFlames();
        }


        public override void update()
        {
            swordUpdate();

         // swordRotateToPoint(new Vector2(sManager.input.MousePosition.X, sManager.input.MousePosition.Y));


            if (newLevel == true)
            {
                timer++;
                if (timer > limit)
                {
                    newLevel = false;
                    sManager.addScreen(new GamePlayScreen(sManager));
                }
            }


            input();

            updateFlames();
        }





        private void swordUpdate()
        {
            //rotates towards target at a limited speed
            if (Math.Abs(swordRotation - swordRotationTarget) > 0.1f)
            {
                if (swordRotation <= swordRotationTarget)
                {
                    swordRotation += swordSpeed;
                }
                else if (swordRotation >= swordRotationTarget)
                {
                    swordRotation -= swordSpeed;
                }
            }
            else
            {
                    //Makes sure its on exact point
                    swordRotation = swordRotationTarget;
            }
        }



        private void swordRotateToPoint(Vector2 point)
        {
            //Gets rotation to the point

            //Calculates the distance from the square to the mouse's X and Y position
            float xDistance = swordPosition.X - point.X;
            float yDistance = swordPosition.Y - point.Y;

            //Calculate the required rotation by doing a two-variable arc-tan
            swordRotationTarget = (float)Math.Atan2(yDistance, xDistance);
            swordRotationTarget -= MathHelper.ToRadians(90);
        }



        public void input()
        {


            if (MainData.xboxMode == false)
            {
                pcInput();
            }
            else
            {
                xboxInput();
            }

            
        }





        private void pcInput()
        {

            //check for button press
            foreach (MenuButton b in buttons)
            {
                if (b.getHitBox().Contains((int)sManager.input.MousePosition.X, (int)sManager.input.MousePosition.Y))
                {
                    b.setHighlighted(true);

                    swordRotateToPoint(new Vector2(b.getPosition().X + (int)b.getHitBox().Width / 2, b.getPosition().Y + (int)b.getHitBox().Height / 2));
                }
                else
                {
                    b.setHighlighted(false);
                }
            }



            buttonSelectedPC();


        }


        private void buttonSelectedPC()
        {
            if (sManager.input.IsNewPress(Buttons.A))
            {
                MainData.xboxMode = true;
            }
  

                if (sManager.input.IsNewPress(MouseButtons.LeftButton))
                {
                    if (playGame.isButtonHighlighted())
                    {
                        MainData.soundBank.PlayCue("menuClick");
                        newLevel = true;
                        // sManager.addScreen(new GamePlayScreen(sManager));
                    }
                    else if (options.isButtonHighlighted())
                    {
                        MainData.soundBank.PlayCue("menuClick");
                        sManager.addScreen(new OptionsMenuScreen(sManager));
                    }
                    else if (about.isButtonHighlighted())
                    {
                        MainData.soundBank.PlayCue("menuClick");
                        sManager.addScreen(new AboutMenuScreen(sManager));
                    }
                    else if (exit.isButtonHighlighted())
                    {
                        MainData.soundBank.PlayCue("menuClick");
                        sManager.addScreen(new MessageBoxScreen("Are you sure you want to quit?", this, sManager, true));
                    }

                
            }
        }


        private void clearHighlights()
        {
            foreach (MenuButton b in buttons)
            {
                b.setHighlighted(false);
            }
        }

        private void xboxInput()
        {
            if (sManager.input.IsNewPress(MouseButtons.LeftButton))
            {
                MainData.xboxMode = false;
            }


            if (sManager.input.IsNewPress(Buttons.LeftThumbstickUp))
            {
                clearHighlights();
                playGame.setHighlighted(true);
                swordRotateToPoint(new Vector2(playGame.getPosition().X + (int)playGame.getHitBox().Width / 2, playGame.getPosition().Y + (int)playGame.getHitBox().Height / 2));
            }

            if (sManager.input.IsNewPress(Buttons.LeftThumbstickRight))
            {
                clearHighlights();
                options.setHighlighted(true);
                swordRotateToPoint(new Vector2(options.getPosition().X + (int)options.getHitBox().Width / 2, options.getPosition().Y + (int)options.getHitBox().Height / 2));
            }

            if (sManager.input.IsNewPress(Buttons.LeftThumbstickLeft))
            {
                clearHighlights();
                about.setHighlighted(true);
                swordRotateToPoint(new Vector2(about.getPosition().X + (int)about.getHitBox().Width / 2, about.getPosition().Y + (int)about.getHitBox().Height / 2));
            }

            if (sManager.input.IsNewPress(Buttons.LeftThumbstickDown))
            {
                clearHighlights();
                exit.setHighlighted(true);
                swordRotateToPoint(new Vector2(exit.getPosition().X + (int)exit.getHitBox().Width / 2, exit.getPosition().Y + (int)exit.getHitBox().Height / 2));
            }


            if (sManager.input.IsNewPress(Buttons.A))
            {
                if (playGame.isButtonHighlighted())
                {
                    MainData.soundBank.PlayCue("menuClick");
                    newLevel = true;
                }
                else if (options.isButtonHighlighted())
                {
                    MainData.soundBank.PlayCue("menuClick");
                    sManager.addScreen(new OptionsMenuScreen(sManager));
                }
                else if (about.isButtonHighlighted())
                {
                    MainData.soundBank.PlayCue("menuClick");
                    sManager.addScreen(new AboutMenuScreen(sManager));
                }
                else if (exit.isButtonHighlighted())
                {
                    MainData.soundBank.PlayCue("menuClick");
                    sManager.addScreen(new MessageBoxScreen("Are you sure you want to quit?", this, sManager, true));
                }

            }


        }





        public override void draw(SpriteBatch sb)
        {
           

            sb.Draw(backgroundTexture, Vector2.Zero, null, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 1);


            //Draw TITLE
           // TextManager.drawTextBorder(sManager.largeFont, sb, "Dungeon Crawl ", Color.Black, Color.White, 1, 0, new Vector2(500, 100), Vector2.Zero);
            sb.Draw(gameTitle, titlePosition, null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 1);

            //Draw Sword
            sb.Draw(swordTexture, swordPosition, null, new Color(0, 0, 0, 150), swordRotation, swordOrigin, 1.2f, SpriteEffects.None, 1);
            sb.Draw(swordTexture, swordPosition, null, Color.White, swordRotation, swordOrigin, 1, SpriteEffects.None, 1);
       

            foreach (MenuButton b in buttons)
            {
                b.draw(sb);
            }

           
            drawFlames(sb);
            drawTorch(sb);

            if (newLevel == true)
            {
                TextManager.drawTextBorder(sManager.font, sb, "Generating level!", Color.Black, Color.White, 1, 0, new Vector2(650, 180));
            }

            
            TextManager.drawTextBorder( sManager.font,sb, "Created By Stewart Taylor", Color.Black, Color.White, 1, 0,new Vector2(250, 600));
        }


        private void drawTorch(SpriteBatch sb)
        {

            sb.Draw(torchTexture, torchPositionLeft, null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 1);
              sb.Draw(torchTexture, torchPositionRight, null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 1);
        
        }

        private void drawFlames(SpriteBatch sb)
        {
            sb.End();
            sb.Begin(SpriteSortMode.Deferred, BlendState.Additive);

            foreach (MenuFlame f in flames)
            {
                f.draw(sb);
            }

            sb.End();
            sb.Begin();

        }

    }
}
