using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

using TechnicallyDifficult.Core;
using TechnicallyDifficult.Graphics;
using TechnicallyDifficult.Entities.EntityComponents;


namespace TechnicallyDifficult.Entities
{
    public class Player : Entity
    {

        protected Animation playerIdle;                 // The animation that will be played when no other animation is active
        protected Animation currentAnimation;           // The animation currently playing. We only draw this.
        protected Animation playerRun;                  // Walking Animation
        protected Animation playerJump;                 // Jumping Animation
        SoundEffect sound;                              // Sound that plays when you bump into something.
        SoundEffect funnySound;                         // A funnier one
        SoundEffectInstance soundInstance;              // The sound instance, used to ensure sound only plays if it's not already.
        SoundEffectInstance funnySoundInstance;         // The funnier sound instance.
        protected SpriteEffects s = SpriteEffects.None; // SpriteEffects used to flip sprite depending on movement direction.
        protected StateMachine stateMachine;            // The state machine used by the player.
        public CircleCollider circleCollider;           // Collider used for physics and stuff!!!!!
        public PhysicsBody physicsBody;                 // The PhysicsBody used for the player.
        public float playerMoveSpeed;                   // Player Horizontal Move Speed
        public float jumpVelocity;                      // Upwards move speed of player.
        public bool hasJumped;                          // Bool to determine if player should get a boost in upwards velocity. Essentially jumping.
        public bool canJump;                            // Can the player jump? Only true if player is touching ground with their feet.
        public bool xblock, yBlock;

        public Player()
        {
            Initialize();
        }

        public override void Initialize()
        {
            base.Initialize();
            _tag = "Player";
            // Initialise EntityComponeents.
            stateMachine = AddComponent<StateMachine>() as StateMachine;
            physicsBody = AddComponent<PhysicsBody>() as PhysicsBody;
            circleCollider = AddComponent<CircleCollider>() as CircleCollider;
            // Set our starting position.
            transform.SetPosition(new Vector2(50f, 400f));
            // Set our dimensions.
            transform.SetDimensions(new Vector2(64, 64));
            // Set the dimensions for the circle collider. Width and Height are used to center the collider,
            // radius is equal to half the width of the character.
            circleCollider.SetColliderDimensions(transform.dimensions.X, transform.dimensions.Y, 32);
            playerRun = new Animation();
            playerIdle = new Animation();
            playerJump = new Animation();
            //Image playerRunSpriteSheet = new Image();
            //playerRunSpriteSheet.Init("Graphics/playerRun", Vector2.Zero, 1, 1);
            //playerRun.Init(playerRunSpriteSheet, Vector2.Zero, 40, 35, 6, 100, true);

            // Initializing the animation files
            Image playerIdleSpriteSheet = new Image("Graphics/Animations/Charles/Idle_New", Vector2.Zero, 1, 1);
            playerIdle.Init(playerIdleSpriteSheet, Vector2.Zero, 64, 64, 1, 0.2, true);

            Image playerRunSpriteSheet = new Image("Graphics/Animations/Charles/WalkR_New", Vector2.Zero, 1, 1);
            playerRun.Init(playerRunSpriteSheet, Vector2.Zero, 64, 64, 4, 0.2, true);

            Image playerJumpSpriteSheet = new Image("Graphics/Animations/Charles/Jump_New", Vector2.Zero, 1, 1);
            playerJump.Init(playerJumpSpriteSheet, Vector2.Zero, 64, 64, 1, 0.2, true);

            hasJumped = false;
            playerMoveSpeed = 1.5f;
            jumpVelocity = -50f;
            canJump = false;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            // Trigger state transitions based on user input
            InputState();
            // Apply animation based on current state
            CheckState();
            // Move the draw position for the animation.
            currentAnimation.position = transform.position;
            // Ensure the animation is active.
            currentAnimation.active = true;
            // Update the animation. This is where it will actually animate.
            currentAnimation.Update(gameTime);
            // Manage Movement of the Player
            ManageMovement();
            // and apply jumping if the player has jumped.
            ApplyJumpPhysics();
        }

        public virtual void InputState()
        {
            // Trigger state transitions based on user input.
            if (InputManager.Instance.GetAxisHorizontal() != 0 && stateMachine.CurrentState != StateMachine.State.Running)
            {
                // If the user is pressing keys for horizontal movement, they are running
                stateMachine.MoveNext(StateMachine.Command.Run);
            }

            if (physicsBody.velocity.Y < 0 && stateMachine.CurrentState != StateMachine.State.Jumping)
            {
                // If the user is moving upwards, they are jumping.
                // This will set state to jump even if user is also holding horizontal movement keys,
                // as this is performed after the run.
                stateMachine.MoveNext(StateMachine.Command.Jump);
            }

            if (InputManager.Instance.GetAxisHorizontal() == 0 && InputManager.Instance.GetAxisVertical() == 0 && stateMachine.CurrentState != StateMachine.State.Idle)
            {
                // If no keys are being pressed, we are idle.
                stateMachine.MoveNext(StateMachine.Command.Stop);
            }
        }

        public void CheckState()
        {
            // Check what state the player is currently in.
            if (stateMachine.CurrentState == StateMachine.State.Idle)
            {
                // If the player is idle, we use the Idle anim.
                currentAnimation = playerIdle;
            }
            else if (stateMachine.CurrentState == StateMachine.State.Running)
            {
                // and if we're running...
                currentAnimation = playerRun;
            }
            else if (stateMachine.CurrentState == StateMachine.State.Jumping)
            {
                // Jumping...
                currentAnimation = playerJump;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Flip the sprites based on the direction the player is moving.
            if (InputManager.Instance.GetAxisHorizontal() < 0)
            {
                s = SpriteEffects.FlipHorizontally;
            }
            else
            {
                s = SpriteEffects.None;
            }
            currentAnimation.Draw(spriteBatch, s);
        }

        public override void LoadContent(ContentManager content)
        {
            // LoadContent for our animations, sound, etc.
            playerRun.LoadContent(content);
            playerIdle.LoadContent(content);
            playerJump.LoadContent(content);
            sound = GameManager.Instance.Content.Load<SoundEffect>("Sound/Bounce");
            funnySound = GameManager.Instance.Content.Load<SoundEffect>("Sound/BouncyBounce");
            soundInstance = sound.CreateInstance();
            funnySoundInstance = funnySound.CreateInstance();
        }

        public void Jump()
        {
            // Check if the player can jump. Basically, are they grounded?
            if(canJump)
            {
                // If they are, allow jumping to occur.
                hasJumped = true;
                // Don't allow jumping once we are airborn.
                canJump = false;
            }
        }

        public virtual void ManageMovement()
        {
            //Apply input axis as force to the player.
            float horizontalMovement = InputManager.Instance.GetAxisHorizontal();
            // Move on the horizontal axis.
            physicsBody.AddForce(new Vector2(horizontalMovement * playerMoveSpeed, 0), ForceMode.Acceleration);
            // Check if the player is hitting W
            float verticalMovement = InputManager.Instance.GetAxisVertical();
            if(verticalMovement < 0)
            {
                // Jump!
                Jump();
            }
        }

        public void ApplyJumpPhysics()
        {
            // If the player has hit the input for jumping
            if (hasJumped)
            {
                // Apply an upwards force to the character, equal to jumpVelocity, which is set in Initialize.
                physicsBody.AddForce(new Vector2(physicsBody.velocity.X, jumpVelocity), ForceMode.VelocityChange);
                hasJumped = false;
            }
        }

        public override void OnCollision(CircleCollider other, Vector2 normal)
        {
            // On collision with another CircleCollider, run Physics Simulation
            physicsBody.CollisionResolution(other, normal);
            // and play a bounce sound.
            if (soundInstance.State == SoundState.Stopped && Vector2.Distance(physicsBody.velocity, Vector2.Zero) > 5)
            {
                soundInstance.Play();
            }
        }

        public override void OnCollision(PlaneCollider other, Vector2 normal)
        {
            base.OnCollision(other, normal);
            // On collision with a plane collider, if we are above the collider, we can jump.
            if(other.position.Y > this.circleCollider.center.Y)
            {
                canJump = true;
            }
        }
    }
}
