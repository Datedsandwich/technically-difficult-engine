using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TechnicallyDifficult.Entities.EntityComponents;
using TechnicallyDifficult.Core;
using TechnicallyDifficult.Graphics;

namespace TechnicallyDifficult.Entities
{
    class UpperFloor : Entity
    {
        public PlaneCollider collider;
        public Image image;

        public UpperFloor()
        {
            Initialize();
        }

        public UpperFloor(Vector2 position)
        {
            Initialize();
            transform.SetPosition(position);
        }

        public override void Initialize()
        {
            base.Initialize();
            this.transform.SetPosition(new Vector2(240f, 840f));
            collider = (PlaneCollider)AddComponent<PlaneCollider>();
            collider.SetColliderDimensions(true, 1400);
            image = new Image("Graphics/Tower", transform.position, 1, 1);
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            image.LoadContent(content);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            Rectangle rect = new Rectangle((int)transform.position.X, (int)transform.position.Y, (int)collider.width, (int)10);
            //image.Draw(spriteBatch, rect);
        }
    }
}
