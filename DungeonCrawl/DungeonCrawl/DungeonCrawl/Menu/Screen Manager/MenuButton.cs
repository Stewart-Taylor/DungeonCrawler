#region USING

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


#endregion

namespace DungeonCrawl.Menu.Screen_Manager
{

    class MenuButton
    {


        #region FIELDS

       private Vector2 position;
       private Texture2D backgroundTexture;
       private Rectangle hitbox;
       private String imagePath;
       private bool isHighlighted = false;


        #endregion


        #region GETS

        public Rectangle getHitBox()
        {
            return hitbox;
        }

        public Vector2 getPosition()
        {
            return position;
        }

        public bool isButtonHighlighted()
        {
            return isHighlighted;
        }


        #endregion


        #region SETS


        public void setHighlighted(bool highlighted)
        {
            if ((highlighted == true) && ( isHighlighted == false))
            {
                MainData.soundBank.PlayCue("menuMove");
            }
            isHighlighted = highlighted;
        }


        #endregion



   


        public MenuButton(String texturePath, Vector2 positionT)
        {
            imagePath = texturePath;
            position = positionT;
        }





        public void load(ContentManager content)
	    {
            //Load image using provided path
            backgroundTexture = content.Load<Texture2D>(imagePath);
    
             //Set up hitbox
             hitbox = new Rectangle((int)position.X, (int)position.Y, (int)backgroundTexture.Width, (int)backgroundTexture.Height); 
	    }



        public void draw(SpriteBatch sb)
        {

                if (isHighlighted == true) //Shows button is selected
                {
                    sb.Draw(backgroundTexture, new Vector2(position.X - 15, position.Y - 7), null, new Color(0, 0, 0, 150), 0, new Vector2(0, 0), 1.35f, SpriteEffects.None, 1);
                    sb.Draw(backgroundTexture, new Vector2(position.X -15, position.Y -7 ), null, Color.White, 0, new Vector2(0, 0), 1.3f, SpriteEffects.None, 1);
                }
                else
                {
                    sb.Draw(backgroundTexture, position, null, new Color(0, 0, 0, 150), 0, new Vector2(0, 0), 1.05f, SpriteEffects.None, 1);
                    sb.Draw(backgroundTexture, position, null, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 1);
                }
        }


    }
}
