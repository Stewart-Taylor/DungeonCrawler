using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DungeonCrawl.Engine.LevelObjects;
using DungeonCrawl.Engine.Spells;

namespace DungeonCrawl.Engine.Characters
{
    class Player : Character
    {


        #region Fields


        private String texturePath = "Characters//Player";



        //Attacking
        int attackTimer = 0;
        int attackLimit = 50;
        bool attacking = false;
        float attackSwing = 0;
        bool attacked = false;
        int attackCharge = 0;
        int attackChargeLimit = 70;
        bool attackcharging = false;


        //defending
        bool pushing = false;
        bool pushed = false;
        float pushSwing = 0;
        int pushTimer = 0;
        int pushLimit = 50;

        private float swayRot = -0.1f;
        int swayTimer = 0;
        int swayLimit = 30;



        SpellManager spells;

        float stamina = 100;
        float staminaMax = 100;

        //Items
        private Sword sword;
        private Shield shield;


        private bool defending = false;

        private int gold ;



        BodyPart head = new BodyPart();
        BodyPart body = new BodyPart();
        BodyPart feet = new BodyPart();
        BodyPart rightArm = new BodyPart();
        BodyPart leftArm = new BodyPart();

        public struct BodyPart
        {
            public Vector2 position;
            public float rotation;
            public Rectangle partBox;
            public Vector2 origin;
        }


        #endregion



        public SpellManager getSpellManager()
        {
            return spells;
        }





        public float getStamina()
        {
            return stamina;
        }

        public float getStaminaMax()
        {
            return staminaMax;
        }


        public BodyPart getRightArm()
        {
            return rightArm;
        }

        public BodyPart getLeftArm()
        {
            return leftArm;
        }

        public int getGold()
        {
            return gold;
        }


        public void giveGold(int amount)
        {
            gold += amount;
        }


        public Player(Director d, Vector2 pos): base(d, pos)
        {
            director = d;
            strength = 4;
            health = 5;
            setUpPosition(pos);
        }







        public void sprint()
        {

            if (stamina > 0)
            {
                if (defending == false)
                {

                    if ((attacking == false) && (attackcharging == false))
                    {
                        director.getEffectManager().addFog(position,1, new Color(15, 10, 0), true);
                        stamina -= 1f;
                        director.player.moveForward();
                        director.player.moveForward();
                    }
                }
            }
        }


        public override void damage(int damageAmount)
        {
            MainData.soundBank.PlayCue("bodyHit");

            if ((defending == false) || ( stamina <= 0))
            {
                director.getRumbleManager().addVibrate(15, 1f);

                if (damageCounter >= damageLimit)
                {
                    damageCounter = 0;


                    director.bloodSplatter();
                    director.getEffectManager().addBlood(position, 5 +health, 20 );
                    health -= damageAmount;

                    if (health < 0)
                    {
                        isDead = true;
                        health = 0;
                    }


                    knockBack();
                }
            }
            else
            {
                director.getRumbleManager().addVibrate(10, 1f);
                knockBack();
                stamina -= 10+ (damageAmount * 2);
            }
        }


        protected override void knockBack()
        {

         

            attackcharging = false;
            attacking = false;
            attackCharge = 0;
            attackTimer = 0;
            attackSwing = 0;

            for (int i = 0; i < Director.random.Next(10,20); i++)
            {
                moveBackward();
            }
        }



        public void useAction()
        {
            openChest();


        }



        private  void openChest()
        {
            foreach (LevelObject objectTemp in director.getLevelObjects())
            {
                if (objectTemp is Chest)
                {
                    Chest chest = (Chest)objectTemp;
                    if (chest.isOpened() == false)
                    {
                        if (Vector2.Distance(position, objectTemp.getCenterPosition()) < 150)
                        {
                            chest.open();
                        }
                    }
                    
        

                }



            }

        }



        public void chargeAttack()
        {
            if ((defending == false) && (pushing == false))
            {
                if (attacking == false)
                {
                    if (stamina > 0)
                    {
                        director.getRumbleManager().addVibrate(5, 1);
                        float percent = (float)attackCharge / (float)attackChargeLimit;
                        director.getRumbleManager().addVibrate(5, percent);
                       // director.getEffectManager().addFog(position, 1, new Color(50, 50, 50), true);
                        stamina -= 0.3f;
                        attackcharging = true;
                    }
                    else
                    {
                        attackcharging = false;
                        attack();
                    }
                }
            }

        }





        public override void attack()
        {

            if ((defending == false) && (pushing == false))
            {
                if (attackcharging == false)
                {
                    if (attacking == false)
                    {
                        stamina -= 10;
                        MainData.soundBank.PlayCue("SwordSwing");

                        attackcharging = false;
                        attacked = false;
                        attacking = true;
                        attackTimer = 0;
                       
                    }
                }
            }

        }


        public void defendPush()
        {
            if (defending == true)
            {
                if (pushing == false)
                {
                    MainData.soundBank.PlayCue("SwordSwing");
                    attackcharging = false;
                    attacking = false;
                    pushTimer = 0;
                    pushing = true;

                }
            }


        }


        public override void defend()
        {
            
            if(attacking == false)
            {
                attackcharging = false;
    
                attackSwing = 0;
                attackTimer = 0;

                defending = true;
            }
        }


        public override void load(ContentManager content)
        {
            texture = content.Load<Texture2D>(texturePath);

            setUpParts();
            setUpHitBox();

            sword = new Sword(director , this);
            sword.load(content);

            shield = new Shield(director, this);
            shield.load(content);

            spells = new SpellManager(director, content);

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

            mainUpdate();

            updateBody();

            if ((defending == false) && ( pushing == false))
            {
                attackUpdate();
            }
            else
            {
                defendUpdate();
            }

            sword.update();
            shield.update();
            updateStamina();
            spells.udpate();

            moving = false;
        }



        private void updateStamina()
        {

            stamina += 0.1f;

            if (stamina > staminaMax)
            {
                stamina = staminaMax;
            }
            else if (stamina < -10)
            {
                stamina = -10;
            }


        }



        private void defendUpdate()
        {


            if (pushing == true)
            {
                pushTimer++;

                if (pushTimer <= pushLimit / 2)
                {

                    if (pushed == false)
                    {
                        pushedMobs();
                    }
                

                    pushSwing -= 0.08f;

                    leftArm.rotation = rotation + pushSwing + MathHelper.ToRadians(70);
                    rightArm.rotation = rotation + MathHelper.ToRadians(70);
                    body.rotation = rotation + MathHelper.ToRadians(50);

                }
                else if (pushTimer <= pushLimit)
                {

                    if (pushed == false)
                    {
                        pushedMobs();
                    }

                    pushSwing += 0.06f;

                    leftArm.rotation = rotation + pushSwing + MathHelper.ToRadians(70);
                    rightArm.rotation = rotation + MathHelper.ToRadians(70);
                    body.rotation = rotation + MathHelper.ToRadians(50);
                }
                else
                {
                    pushed = false;
                    pushSwing = 0;
                    pushing = false;
                    pushTimer = 0;
                }
            }
            else
            {
                leftArm.rotation = rotation + MathHelper.ToRadians(70);
                rightArm.rotation = rotation + MathHelper.ToRadians(70);
                body.rotation = rotation + MathHelper.ToRadians(50);
            }


            defending = false;
        }


        private void pushedMobs()
        {
            foreach (Character c in director.getCharacters())
            {
                if (c.isAlive())
                {
                    if (Vector2.Distance(position, c.getPosition()) < 140)
                    {
                        director.getRumbleManager().addVibrate(15, 0.8f);
                        pushed = true;
                        c.damage(0);
                        for (int i = 1; i < Director.random.Next(3); i++)
                        {
                            c.damage(0);
                        }
                    }
                }
            }
        }


        public void attackUpdate()
        {


            if (attackcharging == true)
            {
                if (attackCharge <= attackChargeLimit)
                {
                    attackCharge++;
                    attackSwing += 0.01f;
                }


                rightArm.rotation = rotation + (attackSwing * 2);
                leftArm.rotation = rotation + (attackSwing * 2 / 3);
                body.rotation = rotation + attackSwing ;

            }
            else if (attacking == true)
            {
                attackTimer++;

                if (attackTimer < (attackLimit ) /2)
                {

              

                    attackSwing -= 0.05f;
                    rightArm.rotation = rotation + (attackSwing * 2);
                    leftArm.rotation = rotation + attackSwing * 1.3f;
                    body.rotation = rotation + attackSwing ;
                }
                else if (attackTimer < (attackLimit ) )
                {

                    if (attacked == false)
                    {
                        attackMobs();
                    }

                    attackSwing += 0.03f;
                    rightArm.rotation = rotation + (attackSwing * 2);
                    leftArm.rotation = rotation + attackSwing * 1.3f;
                    body.rotation = rotation + attackSwing ;
                }
                else
                {

                    attackCharge = 0;
                    attacking = false;
                    attackSwing = 0;
                }

            }
            else
            {

                rightArm.rotation = rotation + swayRot * 2 / 3;
                leftArm.rotation = rotation + swayRot * 2 / 3;
                body.rotation = rotation + swayRot;
            }

            attackcharging = false;
        }



        private void attackMobs()
        {
            foreach (Character c in director.getCharacters())
            {
                if (c.isAlive())
                {
                    if (Vector2.Distance(position, c.getPosition()) < 180)
                    {
                        director.getRumbleManager().addVibrate(10, 1f);
                        damageMob(c);
                        attacked = true;

                    }
                }
            }
        }



        private void damageMob(Character c)
        {
            float damage;

            damage =   ( (float)strength * ((float)attackCharge / (float)attackChargeLimit) + 1);

            c.damage((int)damage);

            if (attackCharge >= attackChargeLimit)
            {

          
                c.damage(strength);

                for (int i = 1; i < Director.random.Next(3,5); i++)
                {
                    c.damage(0);
                }
            }

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
          //  body.rotation = rotation + swayRot ;

            head.position = position;
            head.rotation = rotation;

            feet.position = position;
            feet.rotation = rotation + swayRot * 2 ;

            leftArm.position = position;
         //   leftArm.rotation = rotation + swayRot *2/3;

            rightArm.position = position;
         //   rightArm.rotation = rotation + swayRot *2/3;

        }




        #region DRAW

        public override void draw(SpriteBatch sb)
        {
            drawBody(sb);
           // drawHitBox(sb);
            sword.draw(sb);
            shield.draw(sb);
            spells.draw(sb);
        }



        private void drawBody(SpriteBatch sb)
        {
            drawShadow(sb);
            
            sb.Draw(texture, feet.position, feet.partBox, Color.White, feet.rotation, feet.origin, 1, SpriteEffects.None, 1);
            sb.Draw(texture, rightArm.position, rightArm.partBox, Color.White, rightArm.rotation, rightArm.origin, 1, SpriteEffects.None, 1);
            sb.Draw(texture, leftArm.position, leftArm.partBox, Color.White, leftArm.rotation, leftArm.origin, 1, SpriteEffects.None, 1);
            sb.Draw(texture, body.position, body.partBox, Color.White, body.rotation, body.origin, 1, SpriteEffects.None, 1);
            sb.Draw(texture, head.position, head.partBox, Color.White, head.rotation, head.origin, 1, SpriteEffects.None, 1);
           
        }

        private void drawShadow(SpriteBatch sb)
        {
            Color color = new Color(0, 0, 0, 160);
            Vector2 offSet = new Vector2(10, 10);

            sb.Draw(texture, feet.position, feet.partBox, color, feet.rotation, feet.origin, 1.1f, SpriteEffects.None, 1);
            sb.Draw(texture, rightArm.position, rightArm.partBox, color, rightArm.rotation, rightArm.origin, 1.1f, SpriteEffects.None, 1);
            sb.Draw(texture, leftArm.position, leftArm.partBox, color, leftArm.rotation, leftArm.origin, 1.1f, SpriteEffects.None, 1);
            sb.Draw(texture, body.position, body.partBox, color, body.rotation, body.origin, 1.1f, SpriteEffects.None, 1);
            sb.Draw(texture, head.position, head.partBox, color, head.rotation, head.origin, 1.1f, SpriteEffects.None, 1);
        }



        #endregion




    }
}