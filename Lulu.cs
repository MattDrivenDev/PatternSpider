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
    private MoveDirection _moveDirection;
    private Vector2 _origin;
    private const Int32 _speed = 8;

    public Lulu(Vector2 postition, Texture2D texture)
    {
        _width = 128;
        _height = 58;
        _moveDirection = MoveDirection.Right;
        _position = postition;
        _texture = texture;
        _origin = new Vector2(_width / 2, _height / 2);
    }

    public Rectangle LandscapeHitbox => new Rectangle(
        (Int32)_position.X - _width / 2, 
        (Int32)_position.Y - _height / 2,
        _width, _height);

    public Rectangle PortraitHitbox => new Rectangle(
        (Int32)_position.X - _height / 2, 
        (Int32)_position.Y - _width / 2,
        _height, _width);

    public Boolean Falling => 
        !LandscapeHitbox.Intersects(World.Floor)
        && !LandscapeHitbox.Intersects(World.Ceiling)
        && !PortraitHitbox.Intersects(World.LeftWall)
        && !PortraitHitbox.Intersects(World.RightWall);

    public void Update(GameTime gameTime, KeyboardState keyboardState)
    {
        // Pseudo-gravity effect.
        if (Falling)
        {
            _position.Y += 8;
        }

        // Moving right can only happen if there is space to move right.
        if (keyboardState.IsKeyDown(Keys.Right)
            && !PortraitHitbox.Intersects(World.RightWall))
        {
            _moveDirection = MoveDirection.Right;
            
            // Movement moving right is slower when falling.
            _position.X += !Falling ? _speed : _speed / 2;
            
            // No walk animation when falling.
            if (!Falling)
            {
                _frame++;
            }
        }
        
        // Moving left can only happen if there is space to move right.
        if (keyboardState.IsKeyDown(Keys.Left)
            && !PortraitHitbox.Intersects(World.LeftWall))
        {
            _moveDirection = MoveDirection.Left;
            
            // Movement moving right is slower when falling.
            _position.X -= !Falling ? _speed : _speed / 2;
            
            // No walk animation when falling.
            if (!Falling)
            {
                _frame--;
            }
        }
        
        // Moving up can only happen if there is space to move up,
        // and if there is a wall to climb on.
        if (keyboardState.IsKeyDown(Keys.Up)
            && !LandscapeHitbox.Intersects(World.Ceiling)
            && (PortraitHitbox.Intersects(World.LeftWall) || PortraitHitbox.Intersects(World.RightWall)))
        {
            _position.Y -= _speed;

            if (_moveDirection == MoveDirection.Left
                || _moveDirection == MoveDirection.LeftUp
                || _moveDirection == MoveDirection.LeftDown)
            {
                _moveDirection = MoveDirection.LeftUp;
                _frame--;
            }
            else if (_moveDirection == MoveDirection.Right
                || _moveDirection == MoveDirection.RightUp
                || _moveDirection == MoveDirection.RightDown)
            {
                _moveDirection = MoveDirection.RightUp;
                _frame++;
            }
        }
        
        // Moving down can only happen if there is space to move down,
        // and if there is a wall to climb on.
        if (keyboardState.IsKeyDown(Keys.Down)
            && !LandscapeHitbox.Intersects(World.Floor)
            && (PortraitHitbox.Intersects(World.LeftWall) || PortraitHitbox.Intersects(World.RightWall)))
        {
            _position.Y += _speed;
            
            if (_moveDirection == MoveDirection.Left
                || _moveDirection == MoveDirection.LeftDown
                || _moveDirection == MoveDirection.LeftUp)
            {
                _moveDirection = MoveDirection.LeftDown;
                _frame++;
            }
            else if (_moveDirection == MoveDirection.Right
                || _moveDirection == MoveDirection.RightDown
                || _moveDirection == MoveDirection.RightUp)
            {
                _moveDirection = MoveDirection.RightDown;
                _frame--;
            }
        }

        // If you're on the ceiling you can drop down.
        if (keyboardState.IsKeyDown(Keys.Down) && OnCeiling)
        {
            _position.Y += _speed;
        }
        
        WrapAnimation();
    }
    
    private void WrapAnimation()
    {
        if(_frame >= 25)
        {
            _frame = 0;
        }

        if(_frame < 0)
        {
            _frame = 25;
        }
    }

    private Boolean OnCeiling => LandscapeHitbox.Intersects(World.Ceiling);

    public void Draw(SpriteBatch spriteBatch)
    {
        // The texture contains all the frames of the animation in a vertical strip.
        var sourceRectangle = new Rectangle(0, (_frame * _height), _width, _height);

        var effects = SpriteEffects.None;
        Single rotation = 0f;

        // Rotate clockwise if climbing the left wall.
        if (_moveDirection == MoveDirection.LeftUp
            || _moveDirection == MoveDirection.LeftDown)
        {
            rotation = MathHelper.ToRadians(90);
        } 
        
        // Rotate counter-clockwise if climbing the right wall.
        if (_moveDirection == MoveDirection.RightUp
            || _moveDirection == MoveDirection.RightDown)
        {
            rotation = MathHelper.ToRadians(-90);
        }
        
        // Flip the sprite horizontally if moving leftwards.
        if (_moveDirection == MoveDirection.Left
            || _moveDirection == MoveDirection.LeftUp
            || _moveDirection == MoveDirection.RightDown)
        {
            effects = SpriteEffects.FlipHorizontally;
        }
        
        if(OnCeiling)
        {
            effects = SpriteEffects.FlipVertically 
                | (_moveDirection == MoveDirection.Left ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
        }

        spriteBatch.Draw(
            texture: _texture,
            position: _position,
            sourceRectangle: sourceRectangle,
            origin: _origin,
            color: Color.White,
            rotation: rotation,
            scale: 1f,
            effects: effects,
            layerDepth: 0f);
    }
}