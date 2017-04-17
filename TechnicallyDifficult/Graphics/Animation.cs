using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace TechnicallyDifficult.Graphics
{
    public class Animation
    {
        // ------------------------------------------------------------ Data Members ------------------------------------------------------------
        Image spriteSheet;                              // The sprite sheet
        int elapsedTime;                                // Time elapsed since last Update frame
        double timePerFrame;                               // How long each frame should be displayed. Animation speed.
        int totalFrames;                                 // How many frames in the animation
        Rectangle sourceRect = new Rectangle();         // Rectangle used to determine which part of the sheet to draw
        Rectangle destinationRect = new Rectangle();    // Rectangle used to move the image.
        public int frameWidth, frameHeight;             // Height and Width of individual frames
        public bool active;                            // Should this animation play?
        int currentFrame;                               // Current frame to be drawn
        public bool looping;                            // Should this animation loop?
        public Vector2 position;                        // Where to draw?
        // ------------------------------------------------------------ End Data Members --------------------------------------------------------

        // ------------------------------------------------------------ Methods -----------------------------------------------------------------
        public void Init(Image texture, Vector2 Position, int framewidth, int frameheight, int framecount, double frametime, bool loop)
        {
            spriteSheet = texture;
            this.totalFrames = framecount;
            this.timePerFrame = frametime;
            this.frameHeight = frameheight;
            this.frameWidth = framewidth;
            position = Position;
            looping = loop;
        }
        // ------------------------------------------------------------------------------------------------
        public void Update(GameTime gameTime)
        {
            if (active == false) return;
            elapsedTime += (int)gameTime.ElapsedGameTime.Seconds;

            // In this way of handling varying frame rates, the current frame of the animation is based entirely on how long the game has been running.
            // We divide the elapsedTime by the amount of time given to each frame. frameTime is a double, generally between 0 and 1, so the currentFrame will
            // be a large number. We work out this, and then we use the Modulo operator to divide this number by the amount of frames we have, and return the remainder.

            // There's a problem with this. It's based on when the GAME started, not the animation.
            // We get around this by storing elapsedTime, which starts from when the Animation first runs
            // and we subtract this from the TotalSeconds.
            currentFrame = (int)((gameTime.TotalGameTime.TotalSeconds - elapsedTime) / timePerFrame);
            currentFrame = currentFrame % totalFrames;

            // Define which part of the sprite sheet to draw.
            sourceRect = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);
            // and where to draw it on the screen.
            destinationRect = new Rectangle((int)position.X, (int)position.Y, frameWidth, frameHeight);
        }
        // ------------------------------------------------------------------------------------------------
        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw the animation as is.
            spriteBatch.Draw(spriteSheet.texture, destinationRect, sourceRect, Color.White);
        }
        // ------------------------------------------------------------------------------------------------
        public void Draw(SpriteBatch spriteBatch, SpriteEffects s)
        {
            // Draw the animation with a sprite effect to flip it vertically or horizontally.
            spriteBatch.Draw(spriteSheet.texture, destinationRect, sourceRect, Color.White, 0, Vector2.Zero, s, 1);
        }
        // ------------------------------------------------------------------------------------------------
        public void LoadContent(ContentManager Content)
        {
            spriteSheet.LoadContent(Content);
        }
        // ------------------------------------------------------------------------------------------------
        public void UnloadContent()
        {
            spriteSheet.UnloadContent();
        }
        // ------------------------------------------------------------ End Methods -------------------------------------------------------------
    }
}
