using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace DungeonCrawl.Engine
{
    class RumbleManager
    {




        int timer = -3;


    





        public RumbleManager()
        {



        }



        public void addVibrate(int timeT, float strengthT)
        {
            if (MainData.rumbleOn)
            {
                timer = timeT;
                GamePad.SetVibration(PlayerIndex.One, strengthT, strengthT);
            }

        }



        public void update()
        {


            
            if (timer >= 0)
            {
                timer--;
            }
            else
            {
                 GamePad.SetVibration(PlayerIndex.One, 0f, 0f);
            }


  
        }

    }
}
