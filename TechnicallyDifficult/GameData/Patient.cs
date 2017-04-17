using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using TechnicallyDifficult.Entities;
using TechnicallyDifficult.Graphics;
using TechnicallyDifficult.Core;
using Microsoft.Xna.Framework.Graphics;
using TechnicallyDifficult.Entities.EntityComponents;
using Microsoft.Xna.Framework.Content;

namespace TechnicallyDifficult.GameData
{
    public class Patient : Entity
    {
        float health;                               // Health of the patient
        List<Image> images;                         // list of images for this patient.
        Image currentImage;                         // The current image. Only draw this.
        CircleCollider collider;                    // This patients collider.
        PhysicsBody physicsBody;                    // This patients physicsBody.

        public override void Initialize()
        {
            base.Initialize();
            // Initialize variables
            // Health starts at 100
            health = 100f;
            // Create a list of images, this is how we will indicate how the patient is feeling.
            images = new List<Image>();
            images.Add(new Image("Graphics/Patient-100", transform.position, 1, 1));
            images.Add(new Image("Graphics/Patient-80", transform.position, 1, 1));
            images.Add(new Image("Graphics/Patient-60", transform.position, 1, 1));
            images.Add(new Image("Graphics/Patient-40", transform.position, 1, 1));
            images.Add(new Image("Graphics/Patient-20", transform.position, 1, 1));

            // Set the initial image to be the first element in the list.
            currentImage = images[0];
            // Initialize EntityComponents.
            collider = AddComponent<CircleCollider>() as CircleCollider;
            transform.SetDimensions(new Vector2(40, 64));
            collider.SetColliderDimensions(transform.dimensions.X, transform.dimensions.Y, 20);

            physicsBody = AddComponent<PhysicsBody>() as PhysicsBody;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            // Lower the health of this patient every Update.
            // Value is multiplied by ElapsedGameTime.TotalSeconds to make it consistent across framerates.
            LowerHealth((float)(4 * gameTime.ElapsedGameTime.TotalSeconds));
            // Select which image is required, based on health.
            currentImage = SelectImage();
            // Set the images position to this objects position.
            currentImage.position = this.transform.position;

            // End the game if any patient dies.
            if(health <= 0)
            {
                SceneManager.Instance.CurrentScene.EndScene();
            }
        }

        public override void OnCollision(CircleCollider other, Vector2 normal)
        {
            // On collision with another CircleCollider, run Physics Simulation
            physicsBody.CollisionResolution(other, normal);
            // If the player collides with a patient, heal them.
            if (other.entity.tag == "Player")
            {
                health = 100f;
            }
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            for(int i = 0; i < images.Count; i++)
            {
                images[i].LoadContent(content);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            currentImage.Draw(spriteBatch);
        }

        public void LowerHealth(float value)
        {
            // Lower health by this value, clamping it to the min and max values.
            health -= value;
            health = Math.Max(health, 0);
            health = Math.Min(health, 100);
        }

        public Image SelectImage()
        {
            // Select which image to use.
            if(health > 80f)
            {
                return images[0];
            }
            else if(health > 60f)
            {
                return images[1];
            }
            else if(health > 40f)
            {
                return images[2];
            }
            else if(health > 20f)
            {
                return images[3];
            }
            else
            {
                return images[4];
            }
        }
    }
}
