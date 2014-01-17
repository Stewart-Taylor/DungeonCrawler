#region USING

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using DungeonCrawl.Menu.Screen_Manager;
using Microsoft.Xna.Framework.Content;


#endregion


namespace DungeonCrawl.Menu.Screens
{
    class GameScreen
    {


        #region FIELDS

        public ScreenManager sManager;
        public bool isPopUpScreen = false; 



        #endregion



        public GameScreen(ScreenManager s)
        {
            sManager = s;
        }




        public virtual void load(ContentManager content)
        {


        }




        public virtual void update()
        {


        }



        public virtual void draw(SpriteBatch sb)
        {



        }
	



    }
}
