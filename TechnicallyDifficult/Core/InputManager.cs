using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using TechnicallyDifficult.Core;


namespace TechnicallyDifficult.Core
{
    public class InputManager
    {
        KeyboardState currentKeyState, previousKeyState;
        MouseState currentMouseState, previousMouseState;
        Vector2 mouseCoords;

        private static InputManager instance;
        public static InputManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new InputManager();

                return instance;
            }
        }

        public void Update()
        {
            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            

            previousKeyState = currentKeyState;
            currentKeyState = Keyboard.GetState();

            mouseCoords = TranslatedMouseCoords();
        }

        public Vector2 TranslatedMouseCoords()
        {   Vector2 coords;
            coords.X = currentMouseState.X;
            coords.Y = currentMouseState.Y;
            //translate mouse coords to to correct resolution from virtual
            coords.X = coords.X / ((SceneManager.Instance.ScreenDimensions.X / SceneManager.Instance.virtualWidth));
            coords.Y = coords.Y / ((SceneManager.Instance.ScreenDimensions.Y / SceneManager.Instance.virtualHeight));
            //Console.WriteLine("Translated X = " + coords.X);
            //Console.WriteLine("Translated Y = " + coords.Y);

            return coords;
        }

        public bool MouseLeftClicked()
        {
            if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
            {
                return true;
            }
            return false;
        }

        public bool MouseRightClicked()
        {
            if (currentMouseState.RightButton == ButtonState.Pressed && previousMouseState.RightButton == ButtonState.Released)
            {
                return true;
            }
            return false;
        }

        public bool MouseLeftHold()//Hold also functions are a click atm, maybe fix this
        {
            if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Pressed)
            {
                return true;
            }
            return false;
        }

        public bool MouseRightHold()
        {
            if (currentMouseState.RightButton == ButtonState.Pressed && previousMouseState.RightButton == ButtonState.Pressed)
            {
                return true;
            }
            return false;
        }

        public bool MouseLeftRelease()
        {
            if (currentMouseState.LeftButton == ButtonState.Released && previousMouseState.LeftButton == ButtonState.Pressed)
            {
                return true;
            }
            return false;
        }

        public bool MouseRightRelease()
        {
            if (currentMouseState.RightButton == ButtonState.Released && previousMouseState.RightButton == ButtonState.Pressed)
            {
                return true;
            }
            return false;
        }

        public bool KeyPressed(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (currentKeyState.IsKeyDown(key) && previousKeyState.IsKeyUp(key))
                    return true;
            }
            return false;
        }

        public bool KeyReleased(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (currentKeyState.IsKeyUp(key) && previousKeyState.IsKeyDown(key))
                    return true;
            }
            return false;
        }

        public bool KeyDown(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (currentKeyState.IsKeyDown(key))
                    return true;
            }
            return false;
        }

        public float GetAxisHorizontal()
        {
            // Returns a value between -1 and 1, depending on keys being pressed
            float value = 0;
            if(KeyDown(Keys.D))
            {
                value += 1;
            }
            
            if(KeyDown(Keys.A))
            {
                value -= 1;
            }
            return value;
        }

        public float GetAxisVertical()
        {
            // Returns a value between -1 and 1, depending on keys being pressed
            float value = 0;
            if(KeyDown(Keys.W))
            {
                value -= 1;
            }

            if(KeyDown(Keys.S))
            {
                value += 1;
            }
            return value;
        }

        public float GetAxisHorizontalAlt()
        {
            // Returns a value between -1 and 1, depending on keys being pressed
            float value = 0;
            if (KeyDown(Keys.Right))
            {
                value += 1;
            }

            if (KeyDown(Keys.Left))
            {
                value -= 1;
            }
            return value;
        }

        public float GetAxisVerticalAlt()
        {
            // Returns a value between -1 and 1, depending on keys being pressed
            float value = 0;
            if (KeyDown(Keys.Up))
            {
                value -= 1;
            }

            if (KeyDown(Keys.Down))
            {
                value += 1;
            }
            return value;
        }
    }
}
