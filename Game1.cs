using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Animation_pPoject
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D MrPnutTexture1;
        Texture2D MrPnutTexture2;
        Texture2D stageTexture;
        Rectangle MrPnutRectangle;
        Vector2 MrPnutSpeed;
        Rectangle stageRect;
        Texture2D CurrentPeanut;
        MouseState mouseState;
        Texture2D introScreen;
        SoundEffect PeanutDance;
        SoundEffectInstance pnutDanceInstance;
        Texture2D EndScreen;
        SoundEffect Clapping;
        SoundEffectInstance clappingInstance;

        float seconds;
       

        Screen CurrentScreen;
        enum Screen
        {
            Intro,
            Action,
            Ending
        }


        
        
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 455;
            _graphics.PreferredBackBufferHeight = 482;
            
            _graphics.ApplyChanges();

            stageRect = new Rectangle(0, 0, 455, 482);
            MrPnutSpeed = new Vector2(7, 0);
            MrPnutRectangle = new Rectangle(200, 250, 100, 200);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            MrPnutTexture1 = Content.Load<Texture2D>("MrPnut0");
            MrPnutTexture2 = Content.Load<Texture2D>("MrPnut2");
            stageTexture = Content.Load<Texture2D>("stage");
            CurrentPeanut = MrPnutTexture1;
            introScreen = Content.Load<Texture2D>("StartScreen");
            PeanutDance = Content.Load<SoundEffect>("PeanutDance");
            pnutDanceInstance = PeanutDance.CreateInstance();
            EndScreen = Content.Load<Texture2D>("EndScreen");
            Clapping = Content.Load<SoundEffect>("Applause");
            clappingInstance = Clapping.CreateInstance();
        }

        protected override void Update(GameTime gameTime)
        {


            // TODO: Add your update logic here

            mouseState = Mouse.GetState();
            if (CurrentScreen == Screen.Intro)
            {

            }
            else if (CurrentScreen == Screen.Action)
            {
                MrPnutRectangle.X += (int)MrPnutSpeed.X;
                MrPnutRectangle.Y += (int)MrPnutSpeed.Y;

                if (MrPnutRectangle.Left < 0)
                {
                    MrPnutSpeed *= -1;
                    CurrentPeanut = MrPnutTexture1;

                }
                if (MrPnutRectangle.Right > _graphics.PreferredBackBufferWidth)
                {
                    MrPnutSpeed *= -1;
                    CurrentPeanut = MrPnutTexture2;
                }

                pnutDanceInstance.Play();

                seconds =  (float)gameTime.TotalGameTime.TotalSeconds ;
                if (seconds >= 10)
                {
                    CurrentScreen = Screen.Ending;
                }


            }

            



            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            
            if (CurrentScreen == Screen.Intro)
            {
                _spriteBatch.Draw(introScreen, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);

                if (mouseState.LeftButton == ButtonState.Pressed)
                    CurrentScreen = Screen.Action;
            }
            else if (CurrentScreen == Screen.Action)
            {
                _spriteBatch.Draw(stageTexture, stageRect, Color.White);

                _spriteBatch.Draw(CurrentPeanut, MrPnutRectangle, Color.White);


            }
            else if (CurrentScreen == Screen.Ending)
            {
                

                _spriteBatch.Draw(EndScreen, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
                clappingInstance.Play();
                seconds = (float)gameTime.TotalGameTime.TotalSeconds;
                if (seconds >= 15)
                {
                    this.Exit();
                }

            }
            
            






            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
