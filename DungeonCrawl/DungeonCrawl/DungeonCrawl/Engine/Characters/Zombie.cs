using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using DungeonCrawl.Engine.LevelObjects;

namespace DungeonCrawl.Engine.Characters
{
    class Zombie : Character
    {

        #region Fields

        AiManager aiManager;



        //Attacking
        int attackTimer = 0;
        int attackLimit = 100;
        bool attacking = false;
        float attackSwing = 0;
        bool attacked = false;

        protected bool canMove = true;
        private int moveTimer;
        private int moveLimit = 40;




        private String texturePath = "Characters//Zombie";


        //Sway Animation
        private float swayRot = -0.1f;
        int swayTimer = 0;
        int swayLimit = 30;



        BodyPart head = new BodyPart();
        BodyPart body = new BodyPart();
        BodyPart feet = new BodyPart();
        public BodyPart rightArm = new BodyPart();
        BodyPart leftArm = new BodyPart();


        public struct BodyPart
        {
            public Vector2 position;
            public float rotation;
            public Rectangle partBox;
            public Vector2 origin;
        }


        #endregion




        public Zombie(Director d, Vector2 pos) : base(d, pos)
        {
            director = d;

            health = 10;
            speed = 1.6f;
            strength = 1;

            setUpPosition(pos);


            aiManager = new AiManager(director , this);
        }






        #region Commands


        protected override void knockBack()
        {

            director.getEffectManager().addBlood(position, 7);

            speed = 2.6f;

            rightArm.rotation += (float)Director.random.NextDouble() - (float)Director.random.NextDouble();
            leftArm.rotation -= (float)Director.random.NextDouble() + (float)Director.random.NextDouble();
            head.rotation += (float)Director.random.NextDouble() - (float)Director.random.NextDouble();

            canMove = false;
        }




        public override void attack()
        {
            if (attacking == false)
            {
                attacked = false;
                attacking = true;
                attackTimer = 0;
                attackSwing = 0;
            }
        }



        private void attackPlayer()
        {
            if (Vector2.Distance(position, director.getPlayer().getPosition()) < 130)
            {
                director.getPlayer().damage(strength);
            }

        }




        #endregion



        public override void load(ContentManager content)
        {
            texture = content.Load<Texture2D>(texturePath);
            setUpHitBox();
            setUpParts();
        }



        private void setUpParts()
        {
            head.origin = new Vector2(64, 64);
            head.partBox = new Rectangle(0, 0, 128, 128);

            body.origin = new Vector2(64, 64);
            body.partBox = new Rectangle(128, 0, 128, 128);

            feet.origin = new Vector2(64, 64);
            feet.partBox = new Rectangle(256, 0, 128, 128);

            leftArm.origin = new Vector2(64, 64);
            leftArm.partBox = new Rectangle(384, 0, 128, 128);

            rightArm.origin = new Vector2(64, 64);
            rightArm.partBox = new Rectangle(512, 0, 128, 128);
        }



        public override void update()
        {
            if (isDead == false)
            {
           
                mainUpdate();
                aiManager.update();
                updateBody();
                attackUpdate();
                moveUpdate();

            }
            else
            {
                updateDead();
            }

            moving = false;
        }



        public void attackUpdate()
        {
            if (attacking == true)
            {
                attackTimer++;

                if (attackTimer < (attackLimit /2)  )
                {
                    attackSwing += 0.013f;
                    rightArm.rotation = rotation + (attackSwing * 2);
                    leftArm.rotation = rotation + (attackSwing * 2 / 3);
                    body.rotation = rotation + (attackSwing /2);
                }
                else if (attackTimer < (attackLimit / 2) + (attackLimit / 4))
                {

                    attackSwing -= 0.07f;


                    rightArm.rotation = rotation + (attackSwing * 2);
                    leftArm.rotation = rotation + (attackSwing * 2/3);
                    body.rotation = rotation + (attackSwing / 2);

                }
                else if (attackTimer < attackLimit)
                {
                    if (attacked == false)
                    {
                        attackPlayer();
                        attacked = true;
                    }
                   

                        attackSwing += 0.04f;
                    

                    rightArm.rotation = rotation + (attackSwing * 2);
                    leftArm.rotation = rotation + (attackSwing * 2 / 3);
                    body.rotation = rotation + (attackSwing/2);

                }
                else
                {
                    attacking = false;
                    rightArm.rotation = rotation + swayRot * 2 / 3;
                    leftArm.rotation = rotation + swayRot * 2 / 3;
                }

            }
            else
            {
                rightArm.rotation = rotation + swayRot * 2 / 3;
                leftArm.rotation = rotation + swayRot * 2 / 3;
            }
            
        }



        private void updateDead()
        {

            canMove = true;
  
            if (moveTimer <= moveLimit /4)
            {
                moveTimer++;

                if (Director.random.Next(100) < 20)
                {

                    Treasure t = new Treasure(director, new Vector2(position.X + Director.random.Next(-50, 50), position.Y + Director.random.Next(-50, 50)));
                    director.addTreasure(t);
                }

                director.getEffectManager().addBlood(head.position, 4);
                director.getEffectManager().addBlood(rightArm.position, 4);
                director.getEffectManager().addBlood(leftArm.position, 4);
                director.getEffectManager().addBlood(body.position, 4);
       

                head = tossPart(head);
                rightArm = tossPart(rightArm);
                leftArm = tossPart(leftArm);
                body = tossPart(body);

            }
       

        }


        private void moveUpdate()
        {

            if (canMove == false)
            {
               
                moveTimer++;

                if(moveTimer <  8)
                {
                speed = 16f;
                moveBackward();
                speed = 0;
                }

                if (moveTimer > moveLimit)
                {
                    canMove = true;
                    moveTimer = 0;

                    speed = (float)(1.6f + Director.random.NextDouble());
                }
            }
        }


        private BodyPart tossPart(BodyPart part)
        {
            Vector2 velocity;
            velocity.X = (float)Math.Cos(part.rotation) * 18.5f;
            velocity.Y = (float)Math.Sin(part.rotation) * 18.5f;

            part.position -= velocity;

            Rectangle r = new Rectangle((int)position.X, (int)position.Y, 128, 128);

            foreach(Wall w in director.getGrid().getWalls())
            {
                if(r.Intersects(w.getHitBox()))
                {
                    part.position = new Vector2(-800, -800);
                }
            }

            return part;
        }

        private void sway()
        {
            swayTimer++;

            if (swayTimer < swayLimit / 2)
            {
                swayRot += 0.03f;
            }
            else if (swayTimer < swayLimit)
            {
                swayRot -= 0.03f;
            }
            else if (swayTimer > swayLimit)
            {
                swayTimer = 0;
                swayRot = -0.1f;
            }

        }


        private void updateBody()
        {

            if (moving == true)
            {
                sway();
            }


            body.position = position;
            body.rotation = rotation + swayRot;

            head.position = position;
            head.rotation = rotation;

            feet.position = position;
            feet.rotation = rotation + swayRot * 2;

            leftArm.position = position;
            rightArm.position = position;
        }




        public override void draw(SpriteBatch sb)
        {
            if (Camera.onScreen(position))
            {
                if (canMove == true)
                {
                    drawBody(sb);
                }
                else
                {
                    drawDamaged(sb);
                }
            }
        }


        private void drawDamaged(SpriteBatch sb)
        {

            Color color = new Color(255, 0, 0);

            float percent = (float)((float)moveTimer / (float)moveLimit);
            color.G = (byte)( (255 * percent));
            color.B = (byte)((255 * percent)); 


            sb.Draw(texture, feet.position, feet.partBox, color, feet.rotation, feet.origin, 1, SpriteEffects.None, 1);
            sb.Draw(texture, rightArm.position, rightArm.partBox, color, rightArm.rotation, rightArm.origin, 1, SpriteEffects.None, 1);
            sb.Draw(texture, leftArm.position, leftArm.partBox, color, leftArm.rotation, leftArm.origin, 1, SpriteEffects.None, 1);
            sb.Draw(texture, body.position, body.partBox, color, body.rotation, body.origin, 1, SpriteEffects.None, 1);
            sb.Draw(texture, head.position, head.partBox, color, head.rotation, head.origin, 1, SpriteEffects.None, 1);

        }



        private void drawBody(SpriteBatch sb)
        {
            sb.Draw(texture, feet.position, feet.partBox, Color.White, feet.rotation, feet.origin, 1, SpriteEffects.None, 1);
            sb.Draw(texture, rightArm.position, rightArm.partBox, Color.White, rightArm.rotation, rightArm.origin, 1, SpriteEffects.None, 1);
            sb.Draw(texture, leftArm.position, leftArm.partBox, Color.White, leftArm.rotation, leftArm.origin, 1, SpriteEffects.None, 1);
            sb.Draw(texture, body.position, body.partBox, Color.White, body.rotation, body.origin, 1, SpriteEffects.None, 1);
            sb.Draw(texture, head.position, head.partBox, Color.White, head.rotation, head.origin, 1, SpriteEffects.None, 1);
        }


    }
}
