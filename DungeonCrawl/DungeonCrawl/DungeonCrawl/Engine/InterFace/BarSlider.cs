using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonCrawl.Engine.InterFace
{
    class BarSlider
    {



        #region FIELDS


        private Texture2D outlineTexture;
        private Texture2D barTexture;


        private float scale = 1f;

        private Vector2 position;



        Rectangle barSize;
       
        //attributes
        private int maxValue;

        private float sizePercent;
        private int barWidth;


        private Color color;
        #endregion






        public BarSlider(Vector2 positionT , int maxValueT ,Color colorT)
        {

            position = positionT;
            maxValue = maxValueT;

            color = colorT;

           
        }




        public void load(ContentManager content)
        {



            barTexture = content.Load<Texture2D>("Interface//Bar");
            outlineTexture = content.Load<Texture2D>("Interface//BarOutlay");


            barSize.Height = outlineTexture.Height;
            barSize.Width = outlineTexture.Width;
            barWidth = barSize.Width;
        }




       


        #region Update



        public void update(int currentValue)
        {

            if (currentValue > maxValue)
            {
                currentValue = maxValue;

            }
            else if (currentValue < 0)
            {
                currentValue = 0;
            
            }




            sizePercent = (float)currentValue / (float)maxValue;

            barSize.Width = (int)((float)barWidth * sizePercent);


        }









        #endregion







        #region DRAW


        public void draw(SpriteBatch sb)
        {


            //draws outline
            sb.Draw(outlineTexture, position, null, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 1);

            //draws bar    
            sb.Draw(barTexture, position, barSize, color, 0, new Vector2(0, 0), scale, SpriteEffects.None, 1);
        }


        #endregion



    }
}