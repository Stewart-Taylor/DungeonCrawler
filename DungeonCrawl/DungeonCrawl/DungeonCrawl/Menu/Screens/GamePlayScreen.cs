#region USING
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DungeonCrawl.Menu.Screen_Manager;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using DungeonCrawl.Engine;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

#endregion

namespace DungeonCrawl.Menu.Screens
{
    class GamePlayScreen : GameScreen
    {

        private Director director ;


        

        public GamePlayScreen(ScreenManager s) : base(s)
        {
            sManager = s;
            director = new Director(sManager , this);
        }


        public override void load(ContentManager content)
	    {
            director.load(content);
	    }



        public override void update()
        {
            director.update();

            if (sManager.input.IsNewPress(Keys.Escape))
            {
                sManager.addScreen(new PauseScreen( this ,sManager ));
            }

            if (sManager.input.IsNewPress(Buttons.Start))
            {
                sManager.addScreen(new PauseScreen(this, sManager));
            }
        }


        public override void draw(SpriteBatch sb)
        {

            sManager.graphicsDevice.Clear(Color.Black);

            sb.End();

            director.draw(sb ,sManager.graphicsDevice);

            sb.Begin();
        }


    }
}

    




