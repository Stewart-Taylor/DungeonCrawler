using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using DungeonCrawl.Engine.Tiles;
using Microsoft.Xna.Framework.Content;

namespace DungeonCrawl.Engine.LevelObjects
{
    class Wall
    {


        int xTile;
        int yTile;

        Vector2 position;
        float rotation;


        Texture2D texture;
        static Texture2D textureMiddle;
        static Texture2D textureSide;
        static Texture2D textureCorner;
        static Texture2D textureStraight;
        static Texture2D textureEnd;
        static Texture2D texturePillar;

        static Director director;


        Rectangle hitBox;

        #region GETS


        //  public void

        public Rectangle getHitBox()
        {
            return hitBox;
        }


        public Vector2 getPosition()
        {
            return position;
        }

        #endregion


        public Wall(int x, int y, Director d)
        {
            director = d;

            xTile = x;
            yTile = y;

            calculatePosition();
            setType();



          
        }





        public bool IsColliding(Rectangle hitBoxT)
        {


            //if on screen


            if (hitBox.Intersects(hitBoxT))
            {
                return true;
            }


            return false;

        }


        private void calculatePosition()
        {

            position = new Vector2(xTile * 128, yTile * 128);


            //offSet
            position.X += 64;
            position.Y += 64;


        }








        public void setType()
        {
           // director.getGrid().wallsSet = false;

            //SEE MANUAL ON TYPES HELP
            // LOL I MEAN MY SHEET OF PAPER

            int type = getType();

            if (type == 0)
            {
                texture = textureMiddle;
            }
            else if (type == 1)
            {
                texture = textureSide;
            }
            else if (type == 2)
            {
                texture = textureSide;
                rotation = MathHelper.ToRadians(180);
            }
            else if (type == 3)
            {
                texture = textureSide;
                rotation = MathHelper.ToRadians(-90);
            }
            else if (type == 4)
            {
                texture = textureSide;
                rotation = MathHelper.ToRadians(90);
            }
            else if (type == 5)
            {
                texture = textureCorner;
            }
            else if (type == 6)
            {
                texture = textureCorner;
                rotation = MathHelper.ToRadians(-90);
            }
            else if (type == 7)
            {
                texture = textureCorner;
                rotation = MathHelper.ToRadians(90);
            }
            else if (type == 8)
            {
                texture = textureCorner;
                rotation = MathHelper.ToRadians(180);
            }
            else if (type == 9)
            {
                texture = textureStraight;
            }
            else if (type == 10)
            {
                texture = textureStraight;
                 rotation = MathHelper.ToRadians(90);
            }
            else if (type == 11)
            {
                texture = textureEnd;
            }
            else if (type == 12)
            {
                texture = textureEnd;
                rotation = MathHelper.ToRadians(90);
            }
            else if (type == 13)
            {
                texture = textureEnd;
                rotation = MathHelper.ToRadians(-90);
            }
            else if (type == 14)
            {
                texture = textureEnd;
                rotation = MathHelper.ToRadians(180);
            }
            else if (type == 15)
            {
                texture = texturePillar;
            }
            else
            {
                //if something went wrong
                texture = textureMiddle;
            }
        }




        private bool isValidTile()
        {
            if ((xTile - 1) < 0)
            {
                return false;
            }
            else if ((yTile - 1) < 0)
            {
                return false;
            }

            return true;
        }


        private int getType()
        {

            int type = 0;









            if (isValidTile() == false)
            {
                return 0;
            }






            if (isBlank())
            {
                return 15;
            }


            if (isType9())
            {
                return 9;
            }

            if (isType10())
            {
                return 10;
            }

            if (isType11())
            {
                return 11;
            }

            if (isType12())
            {
                return 12;
            }

            if (isType13())
            {
                return 13;
            }

            if (isType14())
            {
                return 14;
            }

            


            if ((isType1()) && (isType3()))
            {
                type = 5;
            }
            else if ((isType3()) && (isType2()))
            {
                type = 6;
            }
            else if ((isType1()) && (isType4()))
            {
                type = 7;
            }
            else if ((isType4()) && (isType2()))
            {
                type = 8;
            }
            else
            {

                if (isType0())
                {
                    type = 0;
                }
                else if (isType1())
                {
                    type = 1;
                }
                else if (isType2())
                {
                    type = 2;
                }
                else if (isType3())
                {
                    type = 3;
                }
                else if (isType4())
                {
                    type = 4;
                }




            }


            return type;
        }





        private bool isBlank()
        {
   
                   
                


                if (director.getGrid().getTiles()[xTile - 1, yTile - 1] is WallTile)
                {
                    return false;
                }
                else if (director.getGrid().getTiles()[xTile, yTile - 1] is WallTile)
                {
                    return false;
                }
                else if (director.getGrid().getTiles()[xTile + 1, yTile - 1] is WallTile)
                {
                    return false;
                }
                else if (director.getGrid().getTiles()[xTile - 1, yTile] is WallTile)
                {
                    return false;
                }
                else if (director.getGrid().getTiles()[xTile + 1, yTile] is WallTile)
                {
                    return false;
                }
                else if (director.getGrid().getTiles()[xTile - 1, yTile + 1] is WallTile)
                {
                    return false;
                }
                else if (director.getGrid().getTiles()[xTile, yTile + 1] is WallTile)
                {
                    return false;
                }
                else if (director.getGrid().getTiles()[xTile + 1, yTile + 1] is WallTile)
                {
                    return false;
                }

        

            return true;

        }

        private bool isType0()
        {
            if (!(director.getGrid().getTiles()[xTile - 1, yTile - 1] is WallTile))
            {
                return false;
            }
            else if (!(director.getGrid().getTiles()[xTile, yTile - 1] is WallTile))
            {
                return false;
            }
            else if (!(director.getGrid().getTiles()[xTile + 1, yTile - 1] is WallTile))
            {
                return false;
            }
            else if (!(director.getGrid().getTiles()[xTile - 1, yTile] is WallTile))
            {
                return false;
            }
            else if (!(director.getGrid().getTiles()[xTile + 1, yTile] is WallTile))
            {
                return false;
            }
            else if (!(director.getGrid().getTiles()[xTile - 1, yTile + 1] is WallTile))
            {
                return false;
            }
            else if (!(director.getGrid().getTiles()[xTile, yTile + 1] is WallTile))
            {
                return false;
            }
            else if (!(director.getGrid().getTiles()[xTile + 1, yTile + 1] is WallTile))
            {
                return false;
            }

            return true;
        }





        private bool isType1()
        {


            if (!(director.getGrid().getTiles()[xTile + 1, yTile] is WallTile))
            {
                return true;
            }



            return false;
        }





        private bool isType2()
        {

            if (!(director.getGrid().getTiles()[xTile - 1, yTile] is WallTile))
            {
                return true;
            }

            return false;
        }

        private bool isType3()
        {

            if (!(director.getGrid().getTiles()[xTile, yTile - 1] is WallTile))
            {
                return true;
            }

            return false;
        }

        private bool isType4()
        {

            if (!(director.getGrid().getTiles()[xTile, yTile + 1] is WallTile))
            {
                if (director.getGrid().getTiles()[xTile, yTile - 1] is WallTile)
                {
                    return true;
                }
            }


            return false;
        }



        private bool isType9()
        {

            if (director.getGrid().getTiles()[xTile, yTile + 1] is WallTile)
            {
                if (director.getGrid().getTiles()[xTile, yTile - 1] is WallTile)
                {
                    if (!(director.getGrid().getTiles()[xTile - 1, yTile] is WallTile))
                    {
                        if (!(director.getGrid().getTiles()[xTile + 1, yTile] is WallTile))
                        {
                            return true;
                        }
                    }
                 
                }
            }
            return false;
        }


        private bool isType10()
        {

            if (!(director.getGrid().getTiles()[xTile, yTile + 1] is WallTile))
            {
                if (!(director.getGrid().getTiles()[xTile, yTile - 1] is WallTile))
                {
                    if (director.getGrid().getTiles()[xTile - 1, yTile] is WallTile)
                    {
                        if (director.getGrid().getTiles()[xTile + 1, yTile] is WallTile)
                        {
                            return true;
                        }
                    }

                }
            }
            return false;
        }

        private bool isType11()
        {

            if (director.getGrid().getTiles()[xTile, yTile - 1] is WallTile)
            {
                if (!(director.getGrid().getTiles()[xTile, yTile + 1] is WallTile))
                {
                    if (!(director.getGrid().getTiles()[xTile - 1, yTile] is WallTile))
                    {
                        if (!(director.getGrid().getTiles()[xTile + 1, yTile] is WallTile))
                        {
                            return true;
                        }
                    }

                }
            }
            return false;
        }

        private bool isType12()
        {

            if (!(director.getGrid().getTiles()[xTile, yTile - 1] is WallTile))
            {
                if (!(director.getGrid().getTiles()[xTile, yTile + 1] is WallTile))
                {
                    if (director.getGrid().getTiles()[xTile + 1, yTile] is WallTile)
                    {
                        if (!(director.getGrid().getTiles()[xTile - 1, yTile] is WallTile))
                        {
                            return true;
                        }
                    }

                }
            }
            return false;
        }

        private bool isType13()
        {

            if (!(director.getGrid().getTiles()[xTile, yTile - 1] is WallTile))
            {
                if (!(director.getGrid().getTiles()[xTile, yTile + 1] is WallTile))
                {
                    if (director.getGrid().getTiles()[xTile - 1, yTile] is WallTile)
                    {
                        if (!(director.getGrid().getTiles()[xTile + 1, yTile] is WallTile))
                        {
                            return true;
                        }
                    }

                }
            }
            return false;
        }

        private bool isType14()
        {

            if (!(director.getGrid().getTiles()[xTile, yTile - 1] is WallTile))
            {
                if (director.getGrid().getTiles()[xTile, yTile + 1] is WallTile)
                {
                    if (!(director.getGrid().getTiles()[xTile - 1, yTile] is WallTile))
                    {
                        if (!(director.getGrid().getTiles()[xTile + 1, yTile] is WallTile))
                        {
                            return true;
                        }
                    }

                }
            }
            return false;
        }

    





        public void load(ContentManager content)
        {


            //Load all Textures
            texture = content.Load<Texture2D>("Map//Walls//WallSide");
            textureMiddle = content.Load<Texture2D>("Map//Walls//Wall");
            textureCorner = content.Load<Texture2D>("Map//Walls//WallCorner");
            textureSide = content.Load<Texture2D>("Map//Walls//WallSide");
            textureStraight = content.Load<Texture2D>("Map//Walls//WallMiddle");
            textureEnd = content.Load<Texture2D>("Map//Walls//WallEnd");
            texturePillar = content.Load<Texture2D>("Map//Walls//Pillar");



            hitBox = new Rectangle((int)position.X -56, (int)position.Y -56, 90, 90);

        }






        public void update()
        {





        }









        public void draw(SpriteBatch sb)
        {
            

                           if (Camera.onScreen(position))
            {
                                        sb.Draw(texture, position, null, Color.White, rotation, new Vector2(64, 64), 1, SpriteEffects.None, 1);

                                     //   drawHitBox(sb);
                                    }
                       
        }


        private void drawHitBox(SpriteBatch sb)
        {


            sb.Draw(texture, new Vector2(hitBox.X, hitBox.Y), null, Color.Gold, 0, new Vector2(0, 0), 0.1f, SpriteEffects.None, 1);
            sb.Draw(texture, new Vector2(hitBox.X + hitBox.Width, hitBox.Y), null, Color.Gold, 0, new Vector2(0, 0), 0.1f, SpriteEffects.None, 1);
            sb.Draw(texture, new Vector2(hitBox.X, hitBox.Y + hitBox.Height), null, Color.Gold, 0, new Vector2(0, 0), 0.1f, SpriteEffects.None, 1);
            sb.Draw(texture, new Vector2(hitBox.X + hitBox.Width, hitBox.Y + hitBox.Height), null, Color.Gold, 0, new Vector2(0, 0), 0.1f, SpriteEffects.None, 1);


        }


    }
}