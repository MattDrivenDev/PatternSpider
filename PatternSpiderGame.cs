using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace PatternSpider;

public class PatternSpiderGame : Game
{
    private const Int32 _screenWidth = 920, _screenHeight = 540;
    private const Int32 _gravity = 12;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _background;
    private Lulu _lulu;

    public PatternSpiderGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content"; 
        IsMouseVisible = true;
        _graphics.PreferredBackBufferWidth = _screenWidth;
        _graphics.PreferredBackBufferHeight = _screenHeight;
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
        var spiderSpriteSheet = Content.Load<Texture2D>("spider128");

        World.Ceiling = new Rectangle(0, -8, _screenWidth, 8);
        World.Floor = new Rectangle(0, _screenHeight - 4, _screenWidth, 8);
        World.LeftWall = new Rectangle(-8, 0, 8, _screenHeight);
        World.RightWall = new Rectangle(_screenWidth, 0, 8, _screenHeight);        

        _lulu = new Lulu(new Vector2(_screenWidth / 2, _screenHeight / 2), spiderSpriteSheet);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed 
            || Keyboard.GetState().IsKeyDown(Keys.Escape))
        {
            Exit();
        }

        _lulu.Update(gameTime, Keyboard.GetState());
        
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
                Color.White * 1f);
        }

        _lulu.Draw(_spriteBatch);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
