using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetro.AgeOfTetro
{
    public sealed class World
    {
        public readonly Engine Engine;
        public List<Chunk> Chunks = new List<Chunk>();
        public Dictionary<Position, Tile> TilesByPosition = new Dictionary<Position, Tile>();
        public Random Rng = new Random();

        public bool ContainsTile(Position position)
        {
            return this.TilesByPosition.ContainsKey(position);
        }

        public Tile GetTile(Position position)
        {
            if (!this.TilesByPosition.ContainsKey(position)) this.GenerateChunkAt(position);
            return this.TilesByPosition[position];
        }

        private void GenerateChunkAt(Position position)
        {
            var size = 100;
            var sizeTier = this.Rng.Next(3);
            for (int i = 0; i < sizeTier; i++)
            {
                size = size * 2;
            }
            var biome = "Land";
            var chunk = new Chunk(this, biome, size, position);
            this.Chunks.Add(chunk);
        }
    }
}
