using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TechnicallyDifficult.Interfaces
{
    public interface IEntityComponent
    {
        IEntity entity { get; }
        void SetEntity(IEntity value);
    }
}
