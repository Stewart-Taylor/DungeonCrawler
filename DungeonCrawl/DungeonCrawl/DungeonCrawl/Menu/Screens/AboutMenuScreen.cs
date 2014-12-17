using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using DungeonCrawl.Menu.Screen_Manager;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace DungeonCrawl.Menu.Screens
{
    class AboutMenuScreen : GameScreen
    {
        private Texture2D backgroundTexture;

        public AboutMenuScreen(ScreenManager s) : base(s)
        {
            sManager = s;
        }

        public override void load(ContentManager content)
        {
        
            //Load Background
            backgroundTexture = content.Load<Texture2D>("Menu//HelpMenu");
        }

        public override void update()
        {
            if (sManager.input.IsOldPress(Keys.Escape))
            {
                MainData.soundBank.PlayCue("menuClick");
                sManager.removeScreen(this);
            }

            if (sManager.input.IsOldPress(Buttons.B))
            {
                MainData.soundBank.PlayCue("menuClick");
                sManager.removeScreen(this);
            }

            input();
        }

        public void input()
        {

        }

        public override void draw(SpriteBatch sb)
        {
            sb.Draw(backgroundTexture, Vector2.Zero, null, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 1);
        }
    }
}
