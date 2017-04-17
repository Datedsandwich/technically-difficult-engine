using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

using TechnicallyDifficult.Graphics;
using TechnicallyDifficult.Entities;
using TechnicallyDifficult.Core;
using TechnicallyDifficult.Scenes;
using TechnicallyDifficult.Entities.EntityComponents;

using System;
using TechnicallyDifficult.GameData;

namespace TechnicallyDifficult
{
    /// <summary>
    /// This is the main type for your game.
    /// The Game initilizes systems and Loads the content neccessary to start the game
    /// The Core update/Draw commands are located here which feed into SceneManager
    /// </summary>
    public class GameManager : Game
    {
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        Vector2 screenDimensions = new Vector2(1080, 720);

        public ScoreManager scoreManager;

        private static GameManager instance;
        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new GameManager();

                return instance;
            }
        }


        public GameManager()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Here we'll set the defualt resoultion, maybe will make this loaded from file later
        /// Call the ContentFactory to initilize all the objects we need and set the opening scene
        /// </summary>
        protected override void Initialize()
        {
            this.IsMouseVisible = true;
            graphics.PreferredBackBufferHeight = (int)screenDimensions.Y;
            graphics.PreferredBackBufferWidth = (int)screenDimensions.X;
            SceneManager.Instance.ScreenDimensions = screenDimensions;
            graphics.ApplyChanges();
            //Screens are tied together within the object itself so DO NOT change this!
            SceneManager.Instance.Initialize();
            base.Initialize();
        }

        /// <summary>
        /// Creates the vitrual render target and spriteBatch, loads data for the initial gamescreen;
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            SceneManager.Instance.virtualRenderTarget = new RenderTarget2D(GraphicsDevice, 1900, 1080, false, SurfaceFormat.Color, DepthFormat.None);
            SceneManager.Instance.LoadContent(Content);
        }

        public void ReLoadContent()
        {
            LoadContent();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            Content.Unload();
            LoadContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio/animations.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //Runs commands or prints info for Debugging purposes 
            //Debug();

            //This updates the scenemanager which then updates the rest of the game
            SceneManager.Instance.Update(gameTime);

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //This tells the scene manager to draw the current screen, the Graphics device is also passed in so scenemanager can set rendertargets
            SceneManager.Instance.Draw(GraphicsDevice, spriteBatch);

            base.Draw(gameTime);
        }


        protected void Debug()
        {
            //Used for displaying mouse cords
            //Console.Clear();
            //Console.Write(InputManager.Instance.TranslatedMouseCoords().ToString());

            //Temp code for exiting and resolution changing for debugging
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //For changing the resolutions while the program is running put in an opitions meny later
            if (Keyboard.GetState().IsKeyDown(Keys.F1))
            {
                graphics.PreferredBackBufferHeight = 720;
                graphics.PreferredBackBufferWidth = 1080;
                screenDimensions = new Vector2(1080, 720);
                graphics.ApplyChanges();
                SceneManager.Instance.ScreenDimensions = screenDimensions;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.F2))
            {
                graphics.PreferredBackBufferHeight = 1080;
                graphics.PreferredBackBufferWidth = 1900;
                screenDimensions = new Vector2(1900, 1080);
                graphics.ApplyChanges();
                SceneManager.Instance.ScreenDimensions = screenDimensions;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.F3))
            {
                if (graphics.IsFullScreen == true) { graphics.IsFullScreen = false; }
                if (graphics.IsFullScreen == false) { graphics.IsFullScreen = true; }
                graphics.ApplyChanges();
            }
        }
    }
}
