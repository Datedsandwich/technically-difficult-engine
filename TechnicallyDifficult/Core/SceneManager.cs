using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using TechnicallyDifficult.Graphics;
using TechnicallyDifficult.Core;
using TechnicallyDifficult.Scenes;
using TechnicallyDifficult.GameData;
using TechnicallyDifficult.Interfaces;

namespace TechnicallyDifficult.Core
{
    /*This is a singleton class
     *This class is basically the game controller
     *It manages the screen the player is currently on and runs the exit node unpon completion
     *Transition effects will go here
     *It also unloads the old screens content while loading the next screen content
     *The virtual rendering and rerendering is also done here
     */
    class SceneManager : IGameObject
    {
        private static SceneManager instance;       // Singleton.
        public Vector2 ScreenDimensions;            // Dimensions of the Screen.
        public Scene CurrentScene;                  // Current Scene.

        public RenderTarget2D virtualRenderTarget;  // Reference to the game window.

        public Vector2 x0, x1, y0, y1;              // Corner points.

        public List<Scene> scenes;

        public int virtualHeight = 1080;            // Virtual Screen Height
        public int virtualWidth = 1900;             // Virtual Screen Width


        public static SceneManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new SceneManager();

                return instance;
            }
        }

        public void Initialize()
        {
            scenes = new List<Scene>();
            scenes.Add(new Level());
            scenes.Add(new Scoreboard());

            CurrentScene = scenes[0];
        }

        public void ChangeScene(Scene nextScene)
        {
            // Unload the previous scene and load the new one.
            // UnloadContent();
            CurrentScene = nextScene;
            CurrentScene.OnSceneActive();
        }

        public void LoadContent(ContentManager Content)
        {
            for(int i = 0; i < scenes.Count; i++)
            {
                scenes[i].LoadContent(Content);
            }
        }

        public void UnloadContent()
        {
            GameManager.Instance.Content.Unload();
            GameManager.Instance.ReLoadContent();
        }

        public void Update(GameTime gameTime)
        {
            CurrentScene.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }

        public void Draw(GraphicsDevice GraphicsDevice, SpriteBatch spriteBatch)
        {
            VirtualDraw(GraphicsDevice, spriteBatch);
            //This re-renders the game to the correct resolution. Though this WILL mess with mouseinput unless the changed resolution IS ACCOUNTED For
            GraphicsDevice.Clear(Color.Pink);
            spriteBatch.Begin();
            //Redraw the game at the correct size
            spriteBatch.Draw(virtualRenderTarget, new Rectangle(0, 0, (int)ScreenDimensions.X, (int)ScreenDimensions.Y), Color.White);
            spriteBatch.End();
        }

        public void VirtualDraw(GraphicsDevice GraphicsDevice, SpriteBatch spriteBatch)
        {
            GraphicsDevice.SetRenderTarget(virtualRenderTarget);
            GraphicsDevice.Clear(Color.Transparent);
            spriteBatch.Begin();
            //This SHOULD be the the only thing that needs drawing, as hopefully the GameManager will be fed by the SceneManager
            CurrentScene.Draw(spriteBatch);
            spriteBatch.End();
            GraphicsDevice.SetRenderTarget(null);  //Reset the render target to the screen
        }

        public void SetCornerPoints()
        {

            x0 = new Vector2(0, ScreenDimensions.Y);
            x1 = new Vector2(ScreenDimensions.X, ScreenDimensions.Y);
            y0 = new Vector2(0, 0);
            y1 = new Vector2(ScreenDimensions.X, 0);
        }



    }
}
