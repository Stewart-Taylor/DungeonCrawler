using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using DungeonCrawl.Engine.Effects;

namespace DungeonCrawl.Engine
{
    class EffectManager
    {



        ContentManager content;



        //Effects
        List<Blood> blood = new List<Blood>();
        List<Flame> flame = new List<Flame>();
        List<Fog> fog = new List<Fog>();
        List<TorchFlame> torchFlame = new List<TorchFlame>();

        public EffectManager(ContentManager contentT)
        {

            content = contentT;

        }






        #region ADD EFFECT




        public void addFog(Vector2 position, int amount , Color color , bool addT)
        {
            for (int i = 0; i < amount; i++)
            {


                Fog b = new Fog(position, content , color ,addT);
                fog.Add(b);
            }
        }


        public void addTorchFlame(Vector2 position, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                TorchFlame b = new TorchFlame(position, content);
                torchFlame.Add(b);
            }
        }


        public void addFlame(Vector2 position, int amount)
        {
            for (int i = 0; i < amount; i++)
            {


                Flame b = new Flame(position, content );
                flame.Add(b);
            }
        }

        public void addFlame(Vector2 position, int amount , float angle , float speed)
        {
            for (int i = 0; i < amount; i++)
            {


                Flame b = new Flame(position, content, angle , speed);
                flame.Add(b);
            }
        }




        public void addBlood(Vector2 position, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Vector2 posT = new Vector2(position.X + Director.random.Next(-100,100) , position.Y + Director.random.Next(-100,100));

                Blood b = new Blood(posT , content);
                blood.Add(b);
            }
        }


        public void addBlood(Vector2 position, int minAmount , int maxAmount)
        {
            addBlood(position , Director.random.Next(minAmount ,maxAmount));
        }



        #endregion








        public void update()
        {


            updateBlood();
            updateFlame();
            updateFog();
            updateTorchFlame();
        }



        private void updateTorchFlame()
        {

            List<TorchFlame> deads = new List<TorchFlame>();

            foreach (TorchFlame f in torchFlame)
            {
                if (f.isDead() == false)
                {
                    f.update();
                }
                else
                {
                    deads.Add(f);
                }
            }

            foreach (TorchFlame f in deads)
            {
                torchFlame.Remove(f);
            }

            deads.Clear();

        }


        private void updateFog()
        {
          
            List<Fog> deads = new List<Fog>();

            foreach (Fog f in fog)
            {
                if (f.isDead() == false)
                {
                    f.update();
                }
                else
                {
                    deads.Add(f);
                }
            }

            foreach (Fog f in deads)
            {
                fog.Remove(f);
            }

            deads.Clear();
           
        }


        private void updateFlame()
        {
            List<Flame> deads = new List<Flame>();

            foreach (Flame f in flame)
            {
                if (f.isDead() == false)
                {
                    f.update();
                }
                else
                {
                    deads.Add(f);
                }
            }

            foreach (Flame f in deads)
            {
                flame.Remove(f);
            }

            deads.Clear() ;
        }

        private void updateBlood()
        {


        }





        public void drawUnder(SpriteBatch sb)
        {
            drawBlood(sb);
        }


        public void drawOver(SpriteBatch sb, GraphicsDevice device , Camera camera)
        {


            sb.End();
            sb.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, camera.get_transformation(device));

            drawFlame(sb);
            
            drawFogAd(sb);


            sb.End();
            sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, camera.get_transformation(device));

            drawFog(sb);



        }


        public void drawHighest(SpriteBatch sb, GraphicsDevice device, Camera camera)
        {
            sb.End();
            sb.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, camera.get_transformation(device));

            drawTorchFlame(sb);

            sb.End();
            sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, camera.get_transformation(device));


        }

        private void drawTorchFlame(SpriteBatch sb)
        {
            foreach (TorchFlame b in torchFlame)
            {
                b.draw(sb);
            }

        }

        private void drawBlood(SpriteBatch sb)
        {
            foreach (Blood b in blood)
            {
                b.draw(sb);
            }


        }



        private void drawFogAd(SpriteBatch sb)
        {


            foreach (Fog f in fog)
            {
                if (f.isAdditve())
                {
                    f.draw(sb);
                }
            }
             
        }


        private void drawFog(SpriteBatch sb)
        {


            foreach (Fog f in fog)
            {
                if (f.isAdditve() == false)
                {
                    f.draw(sb);
                }
            }
        }

        private void drawFlame(SpriteBatch sb)
        {
         

            foreach (Flame f in flame)
            {
                f.draw(sb);
            }
        }

    }
}
