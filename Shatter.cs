using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PatternSpider;

public class Shatter
{
    private Vector2 _position;
    private Texture2D _texture;
    private const Int32 _width = 72, _height = 67;
    private Vector2 _origin;

    public Rectangle Hitbox => new Rectangle(
        (Int32)_position.X - _width / 2,
        (Int32)_position.Y - _height / 2,
        _width, _height);

    public Shatter(Vector2 position, Texture2D texture)
    {
        _position = position;
        _texture = texture;
        _origin = new Vector2(_width / 2, _height / 2);
    }

    public void Update()
    {

    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            texture: _texture,
            position: _position,
            sourceRectangle: new Rectangle(0, 0, _width, _height),
            origin: _origin,
            color: Color.White,
            rotation: 0f,
            scale: 1f,
            effects: SpriteEffects.None,
            layerDepth: 0f);
    }
}