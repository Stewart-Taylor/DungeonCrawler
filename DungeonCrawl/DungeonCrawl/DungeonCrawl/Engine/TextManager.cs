using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace DungeonCrawl.Engine
{
    class TextManager
    {



        Director director;


        //Fonts 
        SpriteFont font;


        List<TextBanner> texts = new List<TextBanner>();
        List<TextBanner> deadTexts = new List<TextBanner>();

        public struct TextBanner
        {
            public String text;
            public Vector2 position;
            public Color color;
            public Color backColor;
            public float scale;
            public float rotation;
            public int timer;
            public int limit;
            public bool enabled;
            public bool isStatic;


            public void update()
            {
                if (isStatic == false)
                {
                    if (timer <= limit)
                    {
                        timer++;
                    }
                    else
                    {
                        enabled = false;
                    }

                    color.A -= (byte)4;  // CHANGE

                    scale -= 0.004f;
                }
            }
        }


        #region SETS




        #endregion


        #region GETS



        #endregion




        public void changeText(TextBanner tBanner, String text)
        {
            tBanner.text = text;
            TextBanner tDel = new TextBanner();

            foreach (TextBanner t in texts)
            {
                if (t.position == tBanner.position)
                {
                    tDel = t;
                    texts.Add(tBanner);
                    break;
                }
            }


            texts.Remove(tDel);




        }

        #region ADD TEXT


        public void addPositivePoints(String text, Vector2 position, float s)
        {
            TextBanner textBanner = new TextBanner();

            textBanner.text = text;
            textBanner.position = position;
            textBanner.color = Color.Gold;
            textBanner.backColor = Color.Black;
            textBanner.scale = s + 0.2f;
            textBanner.enabled = true;

            textBanner.limit = 100;
            textBanner.timer = 0;



            texts.Add(textBanner);

        }


        public void addrockPoints(String text, Vector2 position, int multiplier, float s)
        {
            TextBanner textBanner = new TextBanner();

            textBanner.text = text;
            textBanner.position = position;
            textBanner.color = Color.Green;
            textBanner.backColor = Color.Black;
            textBanner.scale = s;
            textBanner.enabled = true;

            textBanner.limit = 40 + (multiplier * 10);
            textBanner.timer = 0;


            texts.Add(textBanner);

        }


        public void addNegativePoints(String text, Vector2 position)
        {
            TextBanner textBanner = new TextBanner();

            textBanner.text = text;
            textBanner.position = position;
            textBanner.color = Color.DarkRed;
            textBanner.backColor = Color.Black;
            textBanner.scale = 1.6f;
            textBanner.enabled = true;

            textBanner.limit = 100;
            textBanner.timer = 0;


            texts.Add(textBanner);
        }




        public TextBanner addStaticText(String text, Vector2 position)
        {
            TextBanner textBanner = new TextBanner();

            textBanner.text = text;
            textBanner.position = position;
            textBanner.color = Color.Gold;
            textBanner.backColor = Color.Black;
            textBanner.scale = 1.6f;
            textBanner.enabled = true;

            textBanner.limit = 100;
            textBanner.timer = 0;
            textBanner.isStatic = true;


            texts.Add(textBanner);


            return textBanner;

        }


        #endregion



        public TextManager(Director d)
        {
            director = d;
        }



        #region LOAD

        public void load(ContentManager content)
        {

            font = content.Load<SpriteFont>("Fonts//SpriteFont");


        }



        #endregion






        public void update()
        {



            for (int i = 0; i < texts.Count; i++)
            {
                TextBanner t = texts[i];
                if (t.enabled == true)
                {
                    t.update();
                    texts[i] = t;
                }
                else
                {
                    deadTexts.Add(t);
                }
            }




            foreach (TextBanner t in deadTexts)
            {
                texts.Remove(t);
            }
            deadTexts.Clear();


        }







        #region DRAW

        public void draw(SpriteBatch spriteBatch)
        {
            // spriteBatch.Draw(highlight, highlightPosition, null, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 1);

            // spriteBatch.End();

            //   spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.Additive);



            foreach (TextBanner t in texts)
            {
                drawText(spriteBatch, t);
            }


            //  spriteBatch.End();
            //  spriteBatch.Begin();
        }




        private void drawText(SpriteBatch sb, TextBanner t)
        {
            // sb.DrawString(font, t.text , t.position, t.color);



            Vector2 origin = new Vector2(font.MeasureString(t.text).X / 2, font.MeasureString(t.text).Y / 2);

            sb.DrawString(font, t.text, t.position + new Vector2(1 * t.scale, 1 * t.scale), t.backColor, t.rotation, origin, t.scale, SpriteEffects.None, 1f);

            sb.DrawString(font, t.text, t.position + new Vector2(-1 * t.scale, -1 * t.scale), t.backColor, t.rotation, origin, t.scale, SpriteEffects.None, 1f);

            sb.DrawString(font, t.text, t.position + new Vector2(-1 * t.scale, 1 * t.scale), t.backColor, t.rotation, origin, t.scale, SpriteEffects.None, 1f);

            sb.DrawString(font, t.text, t.position + new Vector2(1 * t.scale, -1 * t.scale), t.backColor, t.rotation, origin, t.scale, SpriteEffects.None, 1f);


            //This is the top layer which we draw in the middle so it covers all the other texts except that displacement.

            sb.DrawString(font, t.text, t.position, t.color, t.rotation, origin, t.scale, SpriteEffects.None, 1f);
        }

        public static void drawTextBorder(SpriteFont font, SpriteBatch sb, string text, Color backColor, Color frontColor, float scale, float rotation, Vector2 position)
        {
            Vector2 origin = new Vector2(font.MeasureString(text).X / 2, font.MeasureString(text).Y / 2);

            sb.DrawString(font, text, position + new Vector2(1 * scale, 1 * scale), backColor, rotation, origin, scale, SpriteEffects.None, 1f);

            sb.DrawString(font, text, position + new Vector2(-1 * scale, -1 * scale), backColor, rotation, origin, scale, SpriteEffects.None, 1f);

            sb.DrawString(font, text, position + new Vector2(-1 * scale, 1 * scale), backColor, rotation, origin, scale, SpriteEffects.None, 1f);

            sb.DrawString(font, text, position + new Vector2(1 * scale, -1 * scale), backColor, rotation, origin, scale, SpriteEffects.None, 1f);


            //This is the top layer which we draw in the middle so it covers all the other texts except that displacement.

            sb.DrawString(font, text, position, frontColor, rotation, origin, scale, SpriteEffects.None, 1f);
        }


        public static void drawTextBorder(SpriteFont font, SpriteBatch sb, string text, Color backColor, Color frontColor, float scale, float rotation, Vector2 position, Vector2 origin)
        {

            sb.DrawString(font, text, position + new Vector2(1 * scale, 1 * scale), backColor, rotation, origin, scale, SpriteEffects.None, 1f);

            sb.DrawString(font, text, position + new Vector2(-1 * scale, -1 * scale), backColor, rotation, origin, scale, SpriteEffects.None, 1f);

            sb.DrawString(font, text, position + new Vector2(-1 * scale, 1 * scale), backColor, rotation, origin, scale, SpriteEffects.None, 1f);

            sb.DrawString(font, text, position + new Vector2(1 * scale, -1 * scale), backColor, rotation, origin, scale, SpriteEffects.None, 1f);


            //This is the top layer which we draw in the middle so it covers all the other texts except that displacement.

            sb.DrawString(font, text, position, frontColor, rotation, origin, scale, SpriteEffects.None, 1f);
        }



        #endregion
    }
}