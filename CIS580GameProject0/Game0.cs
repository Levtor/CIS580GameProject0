using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CIS580GameProject0
{
    public class Game0 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private FrogSprite frog;
        private Texture2D atlas;
        private SpriteFont tnr;

        private BoundingSquare Q;
        private BoundingSquare U;
        private BoundingSquare I;
        private BoundingSquare T;

        private bool q = false;
        private bool qu = false;
        private bool qui = false;
        private bool quit = false;
        private bool quitted = false;
        private double quitTimer;

        List<int> order;

        public Game0()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            frog = new FrogSprite();

            order = new List<int>();
            for (int i = 0; i < 26; i++)
            {
                order.Add(i);
            }
            Random rng = new Random();
            int n = order.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                int value = order[k];
                order[k] = order[n];
                order[n] = value;
            }

            Q = new BoundingSquare(116 + 80 * (order[16] % 6), 66 + 80 * (order[16] / 6), 48);
            U = new BoundingSquare(116 + 80 * (order[20] % 6), 66 + 80 * (order[20] / 6), 48);
            I = new BoundingSquare(116 + 80 * (order[8] % 6), 66 + 80 * (order[8] / 6), 48);
            T = new BoundingSquare(116 + 80 * (order[19] % 6), 66 + 80 * (order[19] / 6), 48);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            frog.LoadContent(Content);
            atlas = Content.Load<Texture2D>("colored_packed");
            tnr = Content.Load<SpriteFont>("TNR");

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape) || quitted)
                Exit();

            // TODO: Add your update logic here
            frog.Update(gameTime);

            if (quit) quitTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (quitTimer > 0.4) quitted = true;

            if (Q.CollidesWith(frog.Bound))
                q = true;
            if (q && U.CollidesWith(frog.Bound))
                qu = true;
            if (qu && I.CollidesWith(frog.Bound))
                qui = true;
            if (qui && T.CollidesWith(frog.Bound))
                quit = true;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.BlanchedAlmond);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            Color color = Color.White;
            if (q) color = Color.Yellow;
            if (qu) color = Color.Orange;
            if (qui) color = Color.Red;
            if (quit) color = Color.Black;

            for (int i = 0; i < 26; i++)
            {
                spriteBatch.Draw(atlas, new Vector2(100 + 80 * (order[i] % 6), 50 + 80 * (order[i] / 6)), new Rectangle(16*(35 + i%13), 16*(18 + i/13), 16, 16), color, 0, new Vector2(0,0), 5, SpriteEffects.None, 0);
            }
            
            frog.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(tnr, $"To exit the game, spell \"quit\" by navigating using the arrow keys and pressing space over the appropriate letters", new Vector2(65, 0), Color.Black);

            //spriteBatch.Draw(atlas, new Vector2(Q.X, Q.Y), new Rectangle(0, 0, (int)Q.Length, (int)Q.Length), Color.Black);
            //spriteBatch.Draw(atlas, new Vector2(U.X, U.Y), new Rectangle(0, 0, (int)U.Length, (int)U.Length), Color.Black);
            //spriteBatch.Draw(atlas, new Vector2(I.X, I.Y), new Rectangle(0, 0, (int)I.Length, (int)I.Length), Color.Black);
            //spriteBatch.Draw(atlas, new Vector2(T.X, T.Y), new Rectangle(0, 0, (int)T.Length, (int)T.Length), Color.Black);
            //spriteBatch.Draw(atlas, new Vector2(frog.Bound.X, frog.Bound.Y), new Rectangle(0, 0, (int)frog.Bound.Length, (int)frog.Bound.Length), Color.Black);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
