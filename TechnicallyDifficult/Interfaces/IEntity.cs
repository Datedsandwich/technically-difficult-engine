using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using TechnicallyDifficult.Entities.EntityComponents;

namespace TechnicallyDifficult.Interfaces
{
    public interface IEntity
    {
        Transform transform
        {
            get;
        }

        string tag
        {
            get;
        }

        IEntityComponent AddComponent<T>() where T : IEntityComponent, new();
        IEntityComponent GetComponent<T>() where T : IEntityComponent;
    }
}
