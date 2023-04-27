using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PatternSpider;

/// <summary>
/// Lulu is the name of the spider in the game.
/// </summary>
public class Lulu
{
    private Vector2 _position;
    private Int32 _frame;
    private Texture2D _texture;
    private Int32 _width, _height;
    private Rectangle Hitbox => new Rectangle(
        (Int32)_position.X - _width / 2, 
        (Int32)_position.Y - _height / 2,
        _width, _height);
    private MoveDirection _moveDirection;
    private Vector2 _origin;

    public Lulu(Vector2 postition, Texture2D texture)
    {
        _width = 128;
        _height = 58;
        _moveDirection = MoveDirection.Right;
        _position = postition;
        _texture = texture;
        _origin = new Vector2(_width / 2, _height / 2);
    }

    public void Update(GameTime gameTime, KeyboardState keyboardState)
    {
        if (keyboardState.IsKeyDown(Keys.Right))
        {
            _position.X += 8;
            _moveDirection = MoveDirection.Right;
        }
        else if (keyboardState.IsKeyDown(Keys.Left))
        {
            _position.X -= 8;
            _moveDirection = MoveDirection.Left;
        }
        else if (keyboardState.IsKeyDown(Keys.Up))
        {
            _position.Y -= 8;

            if (_moveDirection == MoveDirection.Left
                || _moveDirection == MoveDirection.LeftUp
                || _moveDirection == MoveDirection.LeftDown)
            {
                _moveDirection = MoveDirection.LeftUp;
            }
            else if (_moveDirection == MoveDirection.Right
                || _moveDirection == MoveDirection.RightUp
                || _moveDirection == MoveDirection.RightDown)
            {
                _moveDirection = MoveDirection.RightUp;
            }
        }
        else if (keyboardState.IsKeyDown(Keys.Down))
        {
            _position.Y += 8;
            
            if (_moveDirection == MoveDirection.Left
                || _moveDirection == MoveDirection.LeftDown
                || _moveDirection == MoveDirection.LeftUp)
            {
                _moveDirection = MoveDirection.LeftDown;
            }
            else if (_moveDirection == MoveDirection.Right
                || _moveDirection == MoveDirection.RightDown
                || _moveDirection == MoveDirection.RightUp)
            {
                _moveDirection = MoveDirection.RightDown;
            }
        }

        // if(_spiderRectangle.Intersects(_floor))
        // {
        //     if(Keyboard.GetState().IsKeyDown(Keys.Right))
        //     {
        //         _spiderRectangle.X += 8;
        //         _spiderFrame++;
        //         _spiderDirection=Direction.Right;
        //     }
        //     if(Keyboard.GetState().IsKeyDown(Keys.Left))
        //     {
        //         _spiderRectangle.X -= 8;
        //         _spiderFrame--;
        //         _spiderDirection=Direction.Left;
        //     }
        // }

        // if(_spiderRectangle.Intersects(_leftWall)||_spiderRectangle.Intersects(_rightWall))
        // {
        //     if(Keyboard.GetState().IsKeyDown(Keys.Up))
        //     {
        //         _spiderRectangle.Y-=8;
        //         _spiderFrame--;
        //         _spiderDirection=_spiderRectangle.Intersects(_leftWall)
        //             ? Direction.Left:Direction.Right;
        //     }

        //     if(Keyboard.GetState().IsKeyDown(Keys.Down))
        //     {
        //         _spiderRectangle.Y+=8;
        //         _spiderFrame++;
        //         _spiderDirection=_spiderRectangle.Intersects(_leftWall)
        //             ? Direction.Right:Direction.Left;
        //     }
        // }

        // if(!_spiderRectangle.Intersects(_floor))
        // {
        //     _climbing=true;
        // }

        // if(_climbing&&_spiderRectangle.Intersects(_floor))
        // {
        //     _climbing=false;
        // }

        // if(_climbing && _spiderRectangle.Intersects(_leftWall)
        //     && _spiderRectangle.Y < 0)
        // {
        //     _spiderRectangle.Y = 0;
        // }
        // else if(_climbing && _spiderRectangle.Intersects(_rightWall)
        //     && _spiderRectangle.Y < 128)
        // {
        //     _spiderRectangle.Y = 128;
        // }

        // if(_spiderRectangle.Intersects(_floor)&&!_climbing)
        // {
        //     _spiderRectangle.Y = _graphics.PreferredBackBufferHeight-58;
        // }
        
        // if(_spiderRectangle.X<=-16)
        // {
        //     _spiderRectangle.X+=8;
        // }

        // if(_spiderRectangle.X+128-16>_graphics.PreferredBackBufferWidth)
        // {
        //     _spiderRectangle.X-=8;
        // }
        
        // if(_spiderFrame>=25)
        // {
        //     _spiderFrame = 0;
        // }

        // if(_spiderFrame < 0)
        // {
        //     _spiderFrame = 25;
        // }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        var effects = SpriteEffects.None;
        Single rotation = 0f;
        
        if (_moveDirection == MoveDirection.LeftUp
            || _moveDirection == MoveDirection.LeftDown)
        {
            rotation = MathHelper.ToRadians(90);
        } 
        
        if (_moveDirection == MoveDirection.RightUp
            || _moveDirection == MoveDirection.RightDown)
        {
            rotation = MathHelper.ToRadians(-90);
        }
        
        if (_moveDirection == MoveDirection.Left
            || _moveDirection == MoveDirection.LeftUp
            || _moveDirection == MoveDirection.RightDown)
        {
            effects = SpriteEffects.FlipHorizontally;
        }

        spriteBatch.Draw(
            texture: _texture,
            position: _position,
            sourceRectangle: new Rectangle(0, (_frame * _height), _width, _height),
            origin: _origin,
            color: Color.White,
            rotation: rotation,
            scale: 1f,
            effects: effects,
            layerDepth: 0f);
    }
}