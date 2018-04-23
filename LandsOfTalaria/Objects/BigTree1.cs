﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Graphics;
using MonoGame.Extended;
using Microsoft.Xna.Framework.Content;
using System;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;

namespace LandsOfTalaria.Objects
{
    class BigTree1:Trees
    {
        public BigTree1(Vector2 newPosition) :base(newPosition) {
            radius = 16;
            layerSwitchingSize.X = 225;
            layerSwitchingSize.Y = 225;
            HitBoxPosition = new Vector2(position.X + 120, position.Y + 240);
            source = "Objects Textures/Trees/BigTree1Upper";
            source2 = "Objects Textures/Trees/BigTree1Lower";
        }

    }
}
