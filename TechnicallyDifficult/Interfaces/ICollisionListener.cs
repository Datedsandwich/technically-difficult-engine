using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TechnicallyDifficult.Entities.EntityComponents;

using Microsoft.Xna.Framework;

namespace TechnicallyDifficult.Interfaces
{
    interface ICollisionListener
    {
        void OnCollision(BoxCollider other);
        void OnCollision(CircleCollider other, Vector2 normal);
        void OnCollision(PlaneCollider other, Vector2 normal);
    }
}
