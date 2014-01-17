#region USING


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using DungeonCrawl.Menu.Screens;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using DungeonCrawl.Engine;


#endregion

namespace DungeonCrawl.Menu.Screen_Manager
{

    class ScreenManager
    {

        #region FIELDS


        //Mouse
        private Texture2D mouseTexture;

        private List<GameScreen> screens = new List<GameScreen>();
        public InputHelper input = new InputHelper();

        public SpriteFont font;
        public SpriteFont largeFont;

        private ContentManager content;
        private SpriteBatch spriteBatch;

        public Game game;

        public GraphicsDevice graphicsDevice;


        GameTime gameTime ;

        #endregion





        public ScreenManager(Game gameT ,SpriteBatch spriteBatchT , ContentManager contentT)
        {
            spriteBatch = spriteBatchT;
            content = contentT;
            game = gameT;

            graphicsDevice = game.GraphicsDevice;

            load();

            openMainMenu();
        }



        public void load()
        {
            //Load fonts
            font = content.Load<SpriteFont>("Fonts//MenuFont");
            largeFont = content.Load<SpriteFont>("Fonts//LargeFont");

            mouseTexture = content.Load<Texture2D>("Menu//MousePointer");
        }




        public void openMainMenu()
        {
            screens.Clear(); // clears clutter
            MainMenuScreen screenT = new MainMenuScreen(this);
            screenT.load(content);
            screens.Add(screenT);
        }


        public void addScreen(GameScreen screen)
        {
            GamePad.SetVibration(PlayerIndex.One, 0f, 0f);
            screen.load(content);
            screens.Add(screen);
        }

        public void removeScreen(GameScreen screen)
        {
            GamePad.SetVibration(PlayerIndex.One, 0f, 0f);
            screens.Remove(screen);
        }




        private void toolInput()
        {
            if (input.IsNewPress(Keys.F1))
            {
                MainData.xboxMode = !MainData.xboxMode;
            }

            if (input.IsNewPress(Keys.F2))
            {
                MainData.developerMode = !MainData.developerMode;
            }

        }

        public void update(GameTime gameTimeT)
        {

            //update input
            input.Update();


            toolInput();

            if (gameTime == null) //REMOVE!!!
            {
                gameTime = gameTimeT;
            }

            //Flag for when updating screens
            bool popUpVisible = false;


            //check for pop up screens so only they get updated
            for (int i = screens.Count - 1; i > 0; i--) //starts from the last screen added
            {
                GameScreen s = screens[i];

                if (s.isPopUpScreen == true)
                {
                    if (i == screens.Count - 1)
                    {
                        s.update();
                        popUpVisible = true;
                        break;
                    }
                }
                screens[i] = s;
            }


            if (popUpVisible == false)
            {
                //updates last screen as active if no popups
                screens.Last().update();
            }

        }




        public void draw(SpriteBatch sb)
        {
            sb.Begin();


            //only last screen needs drawn
            //pop ups will draw their host screen
            screens.Last().draw(sb);
 

            //draw mouse
            if (MainData.xboxMode == false)
            {
                sb.Draw(mouseTexture, new Vector2((int)input.MousePosition.X, (int)input.MousePosition.Y), null, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 1);
            }

            drawFPS(spriteBatch ,gameTime);

            sb.End();
        }




        private void drawFPS(SpriteBatch sb, GameTime gameTime)
        {
            if (MainData.developerMode)
            {
                int fps = (int)Math.Round(1.0 / gameTime.ElapsedGameTime.TotalSeconds);
                spriteBatch.DrawString(font, "FPS: " + fps, new Vector2(500, 50), Color.Yellow);
                spriteBatch.DrawString(font, "Developer Mode " , new Vector2(500, 10), Color.White);
            }
        }

    }
}
