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
    public class FrogSprite
    {
        private KeyboardState keyboardState;

        private Texture2D texture;

        private double animationTimer;

        private short animationFrame = 1;

        private bool flipped;

        private bool sit;

        public bool space;

        private short speed = 3;

        private Vector2 position = new Vector2(32, 32);

        public BoundingSquare Bound = new BoundingSquare(0, 0, 32);

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
            sit = true;
            space = false;
            // Apply keyboard movement
            if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
            {
                position += new Vector2(0, -1*speed);
                sit = false;
            }
            else if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
            {
                position += new Vector2(0, speed);
                sit = false;
            }
            else if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
            {
                position += new Vector2(-1*speed, 0);
                flipped = true;
                sit = false;
            }
            else if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
            {
                position += new Vector2(speed, 0);
                flipped = false;
                sit = false;
            }

            if (keyboardState.IsKeyDown(Keys.Space))
            {
                space = true;
            }

            // Update animation timer
            if (!sit) animationTimer += gameTime.ElapsedGameTime.TotalSeconds;


            Bound.X = position.X + 16;
            Bound.Y = position.Y + 16;
        }            

        /// <summary>
        /// Draws the sprite using the supplied SpriteBatch
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The spritebatch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Update animation frame
            if (animationTimer > 0.4)
            {
                animationFrame++;
                animationFrame %= 2;
                animationTimer -= 0.4;
            }

            int index;
            if (sit) index = -1;
            else index = animationFrame;

            Color color;
            if (space) color = Color.Blue;
            else color = Color.White;
            SpriteEffects spriteEffects = (flipped) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            spriteBatch.Draw(texture, position, new Rectangle(16*(19 + index), 16*9, 16, 16), color, 0, new Vector2(0, 0), 4, spriteEffects, 0);
        }
    }
}
