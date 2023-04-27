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
    private Int32 _spiderFrame;
    private Direction _spiderDirection=Direction.Right;
    private Texture2D _background;
    private Rectangle _leftWall, _rightWall, _floor;
    private Boolean _climbing;
    private Lulu _lulu;

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
        _spiderFrame=0;
        _floor=new Rectangle(0, _graphics.PreferredBackBufferHeight-4, _graphics.PreferredBackBufferWidth, 8);
        _leftWall=new Rectangle(-8, 0, 8, _graphics.PreferredBackBufferHeight);
        _rightWall=new Rectangle(_graphics.PreferredBackBufferWidth, 0, 8, _graphics.PreferredBackBufferHeight);
        
        _lulu = new Lulu(new Vector2(100, _graphics.PreferredBackBufferHeight-58), _spiderSpriteSheet);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        
        _lulu.Update(gameTime, Keyboard.GetState());
        
        // TODO: Add your update logic here

        


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

        _lulu.Draw(_spriteBatch);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
