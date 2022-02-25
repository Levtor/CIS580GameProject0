using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace CIS580GameProject0
{
    /// <summary>
    /// A class representing a frog
    /// </summary>
    public class LetterSprite
    {
        private KeyboardState keyboardState;

        private Texture2D texture;

        private int ID;

        private Vector2 position = Vector2.Zero;
        private Vector2 center;
        private short radius;
        private double circleTimer;
        private double speedMult = 100;

        public BoundingSquare Bound = new BoundingSquare(0, 0, 32);


        public LetterSprite(int id)
        {
            Random rng = new Random();
            ID = id;
            radius = (short)rng.Next(8, 104);
            float X = Math.Clamp((int)rng.Next(80, 800-80), 100 + radius + 16, 700 - radius - 16);
            float Y = Math.Clamp((int)rng.Next(48, 480-48), 60 + radius + 16, 420 - radius - 16);
            center = new Vector2(X, Y);
            circleTimer = rng.NextDouble() * 2 * Math.PI;
        }

        /// <summary>
        /// Loads the sprite texture using the provided ContentManager
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("colored_packed");
        }

        /// <summary>
        /// Updates the sprite's position based on user input
        /// </summary>
        /// <param name="gameTime">The GameTime</param>
        public void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();

            // Apply keyboard movement
            circleTimer += gameTime.ElapsedGameTime.TotalSeconds * speedMult / radius;
            if (circleTimer >= 2 * Math.PI)
            {
                circleTimer -= 2 * Math.PI;
            }

            float X = (float)Math.Cos(circleTimer) * radius + center.X;
            float Y = (float)Math.Sin(circleTimer) * radius + center.Y;

            Bound.X = X;
            Bound.Y = Y;
            position.X = X - 16;
            position.Y = Y - 16;
        }            

        /// <summary>
        /// Draws the sprite using the supplied SpriteBatch
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The spritebatch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Color color)
        {
            // Update animation frame
            spriteBatch.Draw(texture, position, new Rectangle(16 * (35 + ID % 13), 16 * (18 + ID / 13), 16, 16), color, 0, new Vector2(0, 0), 4, SpriteEffects.None, 0);
        }
    }
}
