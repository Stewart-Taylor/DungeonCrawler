#region USING
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using DungeonCrawl.Menu.Screen_Manager;
using Microsoft.Xna.Framework.Input;
using DungeonCrawl.Engine.Characters;
using DungeonCrawl.Engine.Level;
using DungeonCrawl.Menu.Screens;
using System.Threading;
using DungeonCrawl.Engine.InterFace;
using DungeonCrawl.Menu.Game_Screens;



#endregion

namespace DungeonCrawl.Engine
{

    class Director
    {

        #region FIELD

        public static Random random = new Random();
        public Camera camera = new Camera();
        public SpriteFont font;
        private InterfaceDisplay interfaceDisplay;
        private MiniMap miniMap;

        private List<Character> characters = new List<Character>();
        private List<Treasure> treasure = new List<Treasure>();
        private List<LevelObject> levelObjects = new List<LevelObject>();

        public bool dynamicCamera = false;


        private EffectManager effectManager;

        private Texture2D splatterTexture;
        private int splatterTimer = 44444;
        private int splatterLimit = 100;
        private Color splatterColor;


        private int dungeonLevel = 1;



        RumbleManager rumbleManager;

        //darkness filter
        private Texture2D darkness;
        private Texture2D darknessBorder;
        int darkTimer;
        int darkLimit = 500;
        Color darkColor = Color.White;

        private ScreenManager screenManager;
        private GameScreen gameScreen;

        public Player player;


        public Grid grid;
        public ContentManager content;

        InputManager inputManager;



        private BarSlider staminaBar ;
        private BarSlider manaBar; 

        #endregion



    




        #region SETS





        #endregion




        #region GETS

        public Grid getGrid()
        {
            return grid;
        }


        public RumbleManager getRumbleManager()
        {
            return rumbleManager;
        }


        public EffectManager getEffectManager()
        {
            return effectManager;
        }


        public int getDungeonLevel()
        {
            return dungeonLevel;
        }


        public Camera getCamera()
        {
            return camera;
        }

        public List<LevelObject> getLevelObjects()
        {
            return levelObjects;
        }

        public Player getPlayer()
        {
            return player;
        }

        public List<Character> getCharacters()
        {
            return characters;
        }


        #endregion


        public void bloodSplatter()
        {
            splatterTimer = 0;
            splatterColor = Color.White;
        }


        public Director( ScreenManager sManager , GameScreen gameScreenT)
        {

            gameScreen = gameScreenT;
            screenManager = sManager;

            inputManager = new InputManager(screenManager.input , this);

            interfaceDisplay = new InterfaceDisplay(this);
            miniMap = new MiniMap(this);

            rumbleManager = new RumbleManager();
        }




        public void load(ContentManager contentT)
        {
            content = contentT;

            font = content.Load<SpriteFont>("Fonts//MenuFont");
            splatterTexture = content.Load<Texture2D>("Interface//ScreenBlood");
            darkness = content.Load<Texture2D>("GameScreens//Darkness");
            darknessBorder = content.Load<Texture2D>("Interface//DarknessBorder");

            interfaceDisplay.load(content);

         



            

            player = new Player(this, Vector2.Zero);
            player.load(content);

            generateLevel();

            staminaBar = new BarSlider(new Vector2(1000,660) , (int)player.getStaminaMax() , Color.Green);
            staminaBar.load(content);

            manaBar = new BarSlider(new Vector2(100, 660), (int)player.getSpellManager().getManaMax(), Color.CornflowerBlue);
            manaBar.load(content);
        }



        public void generateLevel()
        {
            levelObjects.Clear();
            treasure.Clear();
            characters.Clear();


            grid = new Grid(this);
            grid.load(content);
            
            Vector2 posStart = grid.getStartPosition();
            posStart.X += 64;
            posStart.Y += 64;
            player.setPosition(posStart);




            LevelObjectGenerator objectGenerator = new LevelObjectGenerator(this);
            effectManager = new EffectManager(content); // clears old particles

            objectGenerator.spawnMobs();
            objectGenerator.placeTreasure();
            objectGenerator.placeTraps();
            objectGenerator.placeTorches();

           // objectGenerator.placeCobWeb();

            miniMap.load(content);
        }



        public void addCharacter(Character character)
        {
            character.load(content);
            characters.Add(character);
        }

        public void addTreasure(Treasure gold)
        {
            gold.load(content);
            treasure.Add(gold);
        }


        public void addLevelObject(LevelObject levelObject)
        {
            levelObject.load(content);
            levelObjects.Add(levelObject);
        }




        private void levelComplete()
        {
            screenManager.addScreen(new LevelCompleteScreen(gameScreen, screenManager, this));
        }


        public  void newLevel()
        {
            dungeonLevel++;
            generateLevel();

        }



        public void test()
        {
            levelComplete();
        }




        public void update()
        {
            updateCharacters();
            updateTreasure();

            updateLevelObjects();

            player.update();
            grid.update();
            inputManager.update();
            hasPlayerGotToExit();

            updateSplatter();
            interfaceDisplay.update();
            miniMap.update();
            rumbleManager.update();

            playersDeath();

            updateBarSliders();

            effectManager.update();
            updateDarkness();
        }




        private void updateDarkness()
        {
            darkTimer++;
            if (darkTimer < darkLimit / 2)
            {
                float percent = (float)darkTimer / (float)darkLimit;
                darkColor.A = (byte)(percent * 250);

            }
            else if (darkTimer < darkLimit)
            {
                float percent = (float)darkTimer / (float)darkLimit;
                darkColor.A = (byte)((250 - (percent * 250)) + 10);

            }
            else
            {
                darkTimer = 0;

            }

        }


        private void playersDeath()
        {
            if (player.getHealth() <= 0)
            {
                screenManager.addScreen(new GameOverScreen(gameScreen, screenManager, this));


            }
        }


        private void updateBarSliders()
        {
            staminaBar.update((int)player.getStamina());
            manaBar.update((int)player.getSpellManager().getMana());
        }

        private void updateLevelObjects()
        {
            foreach (LevelObject levelObject in levelObjects)
            {
                levelObject.update();
            }
        }

        private void updateTreasure()
        {
            foreach (Treasure t in treasure)
            {
                t.update();
            }
        }


        private void updateCharacters()
        {
            foreach (Character c in characters)
            {
                    c.update();
            }
        }


        private void updateSplatter()
        {
            if (splatterTimer <= splatterLimit)
            {
                splatterTimer++;


                float percent = (float)splatterTimer / (float)splatterLimit;
                splatterColor.A = (byte)(255 - (percent * 250));
  
            }

        }

        private void hasPlayerGotToExit()
        {
            Rectangle exitBox = grid.getExit();

            if (player.getHitBox().Intersects(exitBox))
            {
                levelComplete();

            }
        }





        #region DRAW

        public void draw(SpriteBatch sb, GraphicsDevice device)
        {
            drawDynamic(sb, device);
            drawStatic(sb);
        }




        private void drawDynamic(SpriteBatch sb, GraphicsDevice device)
        {

            sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, camera.get_transformation(device));


            grid.draw(sb);
            drawLevelObjects(sb);
            effectManager.drawUnder(sb);
            drawTreasure(sb);
            drawCharacters(sb);
            player.draw(sb);
            effectManager.drawOver(sb , device , camera);
            grid.drawWalls(sb);
            drawLevelObjectsOver(sb);
            effectManager.drawHighest(sb, device, camera);

            sb.End(); 
        }



        private void drawCharacters(SpriteBatch sb)
        {
            foreach (Character c in characters)
            {
                c.draw(sb);
            }
        }


        private void drawTreasure(SpriteBatch sb)
        {
            foreach (Treasure t in treasure)
            {
                t.draw(sb);
            }
        }


        private void drawLevelObjects(SpriteBatch sb)
        {
            foreach (LevelObject levelObject in levelObjects)
            {
                if (levelObject.getOver() == false)
                {
                    levelObject.draw(sb);
                }
            }
        }


        private void drawLevelObjectsOver(SpriteBatch sb)
        {
            foreach (LevelObject levelObject in levelObjects)
            {
                if (levelObject.getOver())
                {
                    levelObject.draw(sb);
                }
            }


        }


        private void drawStatic(SpriteBatch sb)
        {
            //Interface sprites . Static positions
            sb.Begin();
            drawDarkness(sb);
            miniMap.draw(sb);
            interfaceDisplay.draw(sb);
            drawBarSliders(sb);
            drawSplatter(sb);
            
            sb.End();
        }


        private void drawDarkness(SpriteBatch sb)
        {
            sb.Draw(darknessBorder, Vector2.Zero, null, Color.White, 0, new Vector2(0, 0), 1f, SpriteEffects.None, 1);
            sb.Draw(darkness, Vector2.Zero, null, darkColor, 0, new Vector2(0, 0), 1f, SpriteEffects.None, 1);
        }


        private void drawBarSliders(SpriteBatch sb)
        {
            staminaBar.draw(sb);
            manaBar.draw(sb);

        }

        private void drawSplatter(SpriteBatch sb)
        {
            if (splatterTimer <= splatterLimit)
            {
                sb.End();
                sb.Begin(SpriteSortMode.FrontToBack, BlendState.Additive);

                sb.Draw(splatterTexture, Vector2.Zero, null, splatterColor, 0, Vector2.Zero, 1, SpriteEffects.None, 1);

                sb.End();
                sb.Begin();
            }
        }

        #endregion

    }
}
