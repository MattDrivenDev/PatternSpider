using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace PatternSpider;

public static class World
{
    public static Rectangle LeftWall { get; set; }
    public static Rectangle RightWall { get; set; }
    public static Rectangle Ceiling { get; set; }
    public static Rectangle Floor { get; set; }
    public static Shatter[,] Shatterings = new Shatter[8,5];
}