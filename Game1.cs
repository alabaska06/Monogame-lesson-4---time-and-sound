using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static System.Formats.Asn1.AsnWriter;

namespace Monogame_lesson_4___time_and_sound
{
    public class Game1 : Game
    {

        Texture2D bombTexture;
        Texture2D boomTexture;

        SoundEffect explode;
        SoundEffectInstance explodeInstance;

        MouseState mouseState;

        float seconds;
        bool exploded;

        private SpriteFont timefont;
        private int time = 0;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            this.Window.Title = "Time and Sound";

            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 500;
            _graphics.ApplyChanges();

            exploded = false;
            seconds = 0;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            bombTexture = Content.Load<Texture2D>("bomb");
            timefont = Content.Load<SpriteFont>("Time");
            explode = Content.Load<SoundEffect>("explosion");
            explodeInstance = explode.CreateInstance();
            boomTexture = Content.Load<Texture2D>("boom");


            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
                seconds = 0f;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (seconds >= 15)
            {
                explodeInstance.Play();
                exploded = true;
                seconds = 0f;
               
            }
            
            if (explodeInstance.State == SoundState.Stopped && exploded)
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(bombTexture, new Rectangle(50, 50, 700, 400), Color.White);
            
            
            if (exploded)
            {
                _spriteBatch.Draw(boomTexture, new Rectangle(100, 20, 600, 600), Color.White);
            }   
            else
                _spriteBatch.DrawString(timefont, (15 - seconds).ToString("00.0"), new Vector2(270, 200), Color.Black);

            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}