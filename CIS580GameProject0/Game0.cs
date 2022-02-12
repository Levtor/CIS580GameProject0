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

        private LetterSprite Q;
        private LetterSprite U;
        private LetterSprite I;
        private LetterSprite T;

        private bool q = false;
        private bool qu = false;
        private bool qui = false;
        private bool quit = false;
        private bool quitted = false;
        private double quitTimer;

        List<LetterSprite> letter = new List<LetterSprite>();

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

            
            for (int i = 0; i < 26; i++)
            {
                letter.Add(new LetterSprite(i));
            }

            Q = letter[16];
            U = letter[20];
            I = letter[8];
            T = letter[19];

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            frog.LoadContent(Content);
            for (int i = 0; i < 26; i++)
            {
                letter[i].LoadContent(Content); 
            }
            atlas = Content.Load<Texture2D>("colored_packed");
            tnr = Content.Load<SpriteFont>("TNR");

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape) || quitted)
                Exit();

            // TODO: Add your update logic here
            frog.Update(gameTime);
            for (int i = 0; i < 26; i++)
            {
                letter[i].Update(gameTime);
            }

            if (quit) quitTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (quitTimer > 0.4) quitted = true;

            if (Q.Bound.CollidesWith(frog.Bound) && frog.space)
                q = true;
            if (q && U.Bound.CollidesWith(frog.Bound) && frog.space)
                qu = true;
            if (qu && I.Bound.CollidesWith(frog.Bound) && frog.space)
                qui = true;
            if (qui && T.Bound.CollidesWith(frog.Bound) && frog.space)
                quit = true;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.BlanchedAlmond);

            // TODO: Add your drawing code here
            SamplerState samplerState = SamplerState.PointClamp;
            spriteBatch.Begin(SpriteSortMode.Deferred, null, samplerState, null, null, null, null);
            Color color = Color.White;
            if (q) color = Color.Yellow;
            if (qu) color = Color.Orange;
            if (qui) color = Color.Red;
            if (quit) color = Color.Black;

            for (int i = 0; i < 26; i++)
            {
                letter[i].Draw(gameTime, spriteBatch, color);
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
