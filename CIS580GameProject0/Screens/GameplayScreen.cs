using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CIS580GameProject0
{
    // This screen implements the actual game logic. It is just a
    // placeholder to get the idea across: you'll probably want to
    // put some more interesting gameplay in here!
    public class GameplayScreen : GameScreen
    {
        private ContentManager content;

        private FrogSprite frog;
        List<LetterSprite> letter = new List<LetterSprite>();

        private SoundEffect letterHit;
        private Song backgroundMusic;

        bool oldSpace;
        bool newSpace;
        bool firstTime = true;
        private bool quit = false;
        private double quitTimer;
        private int wordProgress = 0;
        private string secretWord = "frog";

        public GameplayScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        // Load graphics content for the game
        public override void Activate()
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");

            frog = new FrogSprite();

            for (int i = 0; i < 26; i++)
            {
                letter.Add(new LetterSprite(i));
            }

            // TODO: use this.Content to load your game content here
            frog.LoadContent(content);
            for (int i = 0; i < 26; i++)
            {
                letter[i].LoadContent(content);
            }
            letterHit = content.Load<SoundEffect>("LetterHit");
            backgroundMusic = content.Load<Song>("Lobo Loco - Woke up This Morning - RocknRoll (ID 1672)");
        }


        public override void Deactivate()
        {
            base.Deactivate();
        }

        public override void Unload()
        {
            content.Unload();
        }

        // This method checks the GameScreen.IsActive property, so the game will
        // stop updating when the pause menu is active, or if you tab away to a different application.
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);

            if (IsActive)
            {
                
            }
        }

        // Unlike the Update method, this will only be called when the gameplay screen is active.
        public override void HandleInput(GameTime gameTime, InputState input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (firstTime)
            {
                firstTime = false;
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Play(backgroundMusic);
            }


            frog.Update(gameTime);
            oldSpace = newSpace;
            newSpace = frog.space;

            for (int i = 0; i < 26; i++)
            {
                letter[i].Update(gameTime);
            }

            if (wordProgress == secretWord.Length) quitTimer += gameTime.ElapsedGameTime.TotalSeconds;
            else if (!oldSpace && newSpace && letter[secretWord[wordProgress] - 'a'].Bound.CollidesWith(frog.Bound))
            {
                wordProgress++;
                letterHit.Play();
            }
            if (quitTimer > 0.4)
            {
                MediaPlayer.Stop();
                ExitScreen();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
            var spriteBatch = ScreenManager.SpriteBatch;
            SamplerState samplerState = SamplerState.PointClamp;
            spriteBatch.Begin(SpriteSortMode.Deferred, null, samplerState, null, null, null, null);
            Color color = Color.White;

            for (int i = 0; i < 26; i++)
            {
                letter[i].Draw(gameTime, spriteBatch, color);
            }

            frog.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }
    }
}
