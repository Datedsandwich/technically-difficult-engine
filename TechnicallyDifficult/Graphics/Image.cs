using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using TechnicallyDifficult.Core;

namespace TechnicallyDifficult.Graphics
{
    public class Image
    {
        public Texture2D texture;
        public string filePath;
        public float alpha;
        public Vector2 position;
        public float scale;
        //public Rectangle sourceRect;

        public Image(String FilePath, Vector2 Position, float Scale, float Alpha)
        {
            filePath = FilePath;
            position = Position;
            scale = Scale;
            alpha = Alpha;
        }

        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>(filePath);
        }

        public void UnloadContent()
        {
            texture.Dispose();
        }

        //draw the image at a set cords set for each image
        public void Draw(SpriteBatch spriteBatch)
        {
            //Take in the floatPostion and determine the drawing position in reference to the current Screen resoultionas
            //Vector2 position = new Vector2((SceneManager.Instance.ScreenDimensions.X*floatPosition.X),(SceneManager.Instance.ScreenDimensions.Y*floatPosition.Y));
            //spriteBatch.Draw(image, position, Color.White);
            if(texture != null)
            {
                spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, Math.Min((int)texture.Width, 1900), Math.Min((int)texture.Height, 1080)), Color.White);
            }
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle rect)
        {
            spriteBatch.Draw(texture, rect, Color.White);
        }

        //Draw the image via a set cord, e.g at the button location
        public void Draw(SpriteBatch spriteBatch, Vector2 imagepos)
        {
            //Take in the floatPostion and determine the drawing position in reference to the current Screen resoultionas
            //Vector2 position = new Vector2((SceneManager.Instance.ScreenDimensions.X*floatPosition.X),(SceneManager.Instance.ScreenDimensions.Y*floatPosition.Y));
            //spriteBatch.Draw(image, position, Color.White);

            spriteBatch.Draw(texture, new Rectangle((int)imagepos.X, (int)imagepos.Y, Math.Min((int)texture.Width, 1900), Math.Min((int)texture.Height, 1080)), Color.White);

        }



        /*public void Draw(SpriteBatch spriteBatch, Rectangle sourceRect, float Scale, float Alpha)
        {
            //Come back here
        }*/
    }

}
