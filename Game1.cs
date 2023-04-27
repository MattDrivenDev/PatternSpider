using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace PatternSpider;

public enum Direction{Left,Right}

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _spiderSpriteSheet;
    private Rectangle _spiderRectangle;
    private Int32 _spiderFrame;
    private Direction _spiderDirection=Direction.Right;
    private Texture2D _background;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content"; 
        IsMouseVisible = true;
        _graphics.PreferredBackBufferWidth = 920;
        _graphics.PreferredBackBufferHeight = 540;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        

        _background = Content.Load<Texture2D>("background1");
        _spiderSpriteSheet = Content.Load<Texture2D>("spider128");
        _spiderRectangle = new Rectangle(100, _graphics.PreferredBackBufferHeight-58, 128, 58);
        _spiderFrame=0;
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        if(Keyboard.GetState().IsKeyDown(Keys.Right))
        {
            _spiderRectangle.X += 8;
            _spiderFrame++;
            _spiderDirection=Direction.Right;
        }
        if(Keyboard.GetState().IsKeyDown(Keys.Left))
        {
            _spiderRectangle.X -= 8;
            _spiderFrame--;
            _spiderDirection=Direction.Left;
        }

        
        if(_spiderFrame>=25)
        {
            _spiderFrame = 0;
        }

        if(_spiderFrame < 0)
        {
            _spiderFrame = 25;
        }


        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();

        // 920 x 540
        // 216 x 188 (108 x 94)
        for(var x = 0; x < 6; x++)
        for(var y = 0; y < 6; y++)
        {
            var r = new Rectangle(x*216,y*188,216,188);
            _spriteBatch.Draw(
                _background,
                r,
                Color.White*1f);
        }

        _spriteBatch.Draw(
            _spiderSpriteSheet, 
            _spiderRectangle, 
            new Rectangle(0,58*_spiderFrame,128,58),
            Color.White, 
            0, new Vector2(0,0), _spiderDirection==Direction.Left?SpriteEffects.FlipHorizontally:SpriteEffects.None,0);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
