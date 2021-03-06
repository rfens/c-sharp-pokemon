﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MonoGame
{
    public class Level
    {
        Game1 game;
        public Player player;
        public LevelBuilder builder;
        CollisionDetection collision;
        public Texture2D background;

        public List<Tile> levelTiles = new List<Tile>();
        public List<Grass> levelGrass = new List<Grass>();
        public List<Ledge> levelLedges = new List<Ledge>();
        public List<Building> levelBuildings = new List<Building>();
        public List<Enemy> levelEnemies = new List<Enemy>();
        public List<Tile> levelBackground = new List<Tile>();
        public List<Exit> levelExits = new List<Exit>();

        IAnimatedSprite signTexture;
        public bool displaySign = false;

        public Level(string fileName, Game1 game)
        {
            this.game = game;
            builder = new LevelBuilder(this);
            player = builder.Build(fileName);
            collision = new CollisionDetection(player, game);
        }
        
        public void DisplayInfo(IAnimatedSprite sprite)
        {
            signTexture = sprite;
            displaySign = true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tile tile in levelBackground)
            {
                if (game.camera.IsInView(tile.GetBoundingBox()))
                {
                    tile.Draw(spriteBatch, tile.position, Color.White);
                }
            }
            foreach (Ledge ledge in levelLedges)
            {
                if (game.camera.IsInView(ledge.GetBoundingBox()))
                {
                    ledge.Draw(spriteBatch, ledge.position, Color.White);
                }
            }
            foreach (Tile tile in levelTiles)
            {
                if (game.camera.IsInView(tile.GetBoundingBox()))
                {
                    tile.Draw(spriteBatch, tile.position, Color.White);
                }
            }
            foreach (Grass grass in levelGrass)
            {
                if (game.camera.IsInView(grass.GetBoundingBox()))
                {
                    grass.Draw(spriteBatch, grass.position, Color.White);
                }
            }
            foreach (Building building in levelBuildings)
            {
                if (game.camera.IsInView(building.GetBoundingBox()))
                {
                    building.Draw(spriteBatch, building.position, Color.White);
                }
            }
            foreach (Enemy enemy in levelEnemies)
            {
                if (game.camera.IsInView(enemy.state.GetBoundingBox(enemy.position)))
                {
                    enemy.Draw(spriteBatch);
                }
            }
            foreach (Exit exit in levelExits)
            {
                if (game.camera.IsInView(exit.sprite.GetBoundingBox(exit.position)))
                {
                    exit.Draw(spriteBatch);
                }
            }
            player.Draw(spriteBatch);
            if (displaySign)
            {
                signTexture.Draw(spriteBatch, new Vector2(player.position.X - 100f, player.position.Y), Color.White);
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (Tile tile in levelTiles)
            {
                if (game.camera.IsInView(tile.GetBoundingBox()))
                {
                    tile.Update(gameTime);
                }
            }
            foreach (Grass grass in levelGrass)
            {
                if (game.camera.IsInView(grass.GetBoundingBox()))
                {
                    grass.Update(gameTime);
                }
            }
            foreach (Ledge ledge in levelLedges)
            {
                if (game.camera.IsInView(ledge.GetBoundingBox()))
                {
                    ledge.Update(gameTime);
                }
            }
            foreach (Building building in levelBuildings)
            {
                if (game.camera.IsInView(building.GetBoundingBox()))
                {
                    building.Update(gameTime);
                }
            }
            foreach (Enemy enemy in levelEnemies)
            {
                if (game.camera.IsInView(enemy.state.GetBoundingBox(enemy.position)))
                {
                    enemy.Update(gameTime);
                }
            }
            foreach (Exit exit in levelExits)
            {
                if (game.camera.IsInView(exit.sprite.GetBoundingBox(exit.position)))
                {
                    exit.Update(gameTime);
                }
            }
            collision.Detect(player, levelTiles, levelGrass, levelLedges, levelBuildings, levelEnemies, levelExits);
            player.Update(gameTime);
        }
    }
}
