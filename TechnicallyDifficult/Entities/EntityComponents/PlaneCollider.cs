using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using TechnicallyDifficult.Core;
using TechnicallyDifficult.Interfaces;

namespace TechnicallyDifficult.Entities.EntityComponents
{
    public class PlaneCollider : Collider
    {
        public bool horizontal;

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            this.position = entity.transform.position;
        }
        public void SetColliderDimensions(bool _horizontal, float size)
        {
            horizontal = _horizontal;
            if (horizontal)
            {
                width = size;
            }
            else
            {
                height = size;
            }
        }
    }
}
