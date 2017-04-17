using Microsoft.Xna.Framework;
using System;

using TechnicallyDifficult.Core;

namespace TechnicallyDifficult.Entities.EntityComponents
{
    public class Transform : EntityComponent
    {
        // We store the actual variables privately, or more accurately, protected-ly
        // The classes will only actually edit the protected data members, with the public
        // ones being methods that return the protected variables.
        public Vector2 _position;
        protected Vector2 _dimensions;

        public Vector2 position { get { return _position; } }
        public Vector2 dimensions { get { return _dimensions; } }

        public void SetPosition(Vector2 value)
        {
            // Set the position of this object, clamping it to the edges of the screen.
            _position.X = MathHelper.Clamp(value.X, 0, SceneManager.Instance.virtualWidth - dimensions.X);
            _position.Y = MathHelper.Clamp(value.Y, 0, SceneManager.Instance.virtualHeight - dimensions.Y);
        }

        public void SetDimensions(Vector2 value)
        {
            _dimensions = value;
        }
    }
}
