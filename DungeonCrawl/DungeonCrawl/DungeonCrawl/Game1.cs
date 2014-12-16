using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using DungeonCrawl.Menu.Screen_Manager;

namespace DungeonCrawl
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        ScreenManager screenManager ;  

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            IsMouseVisible = false;

            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.IsFullScreen = MainData.fullScreen;
            graphics.ApplyChanges();
            Window.Title = "Dungeon Crawl";

            //Sound
            MainData.audioEngine = new AudioEngine("Content\\Audio\\dCrawlSounds.xgs");
            MainData.waveBank = new WaveBank(MainData.audioEngine, "Content\\Audio\\waveBank.xwb");
            MainData.soundBank = new SoundBank(MainData.audioEngine, "Content\\Audio\\soundBank.xsb");

            //  MainData.music = MainData.audioEngine.GetCategory("Music");
            MainData.soundEffects = MainData.audioEngine.GetCategory("Default");

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            screenManager = new ScreenManager(this ,spriteBatch , Content);
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
             //   this.Exit();
            }

            screenManager.update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //GraphicsDevice.BlendState = GraphicsDevice.a;

            screenManager.draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
