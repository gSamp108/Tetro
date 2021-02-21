using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetro.AgeOfTetro
{
    public sealed class Chunk
    {
        public readonly World World;
        public Engine Engine { get { return this.World.Engine; } }
        public string Biome;
        public Dictionary<Position, Tile> TilesByPosition = new Dictionary<Position, Tile>();

        public Chunk(World world, string biome, int size, Position initialPosition)
        {
            this.World = world;
            this.Biome = biome;
            this.Generate(size, initialPosition);
        }

        private void Generate(int size, Position initialPosition)
        {
            var open = new HashSet<Position>();
            var closed = new HashSet<Position>();
            var weight = new Dictionary<Position, int>();
            open.Add(initialPosition);

            Action<Position> closeOn = null;
            closeOn = delegate (Position closeOnPosition)
            {
                closed.Add(closeOnPosition);
                open.Remove(closeOnPosition);
                foreach(var closeOnPositionAdjacent in closeOnPosition.Adjacent)
                {
                    if (!closed.Contains(closeOnPositionAdjacent) && !this.World.ContainsTile(closeOnPositionAdjacent)) open.Add(closeOnPositionAdjacent);
                }
                if (!weight.ContainsKey(closeOnPosition)) weight.Add(closeOnPosition, 0);
                weight[closeOnPosition] += 1;
                foreach (var closeOnPositionNearby in closeOnPosition.Nearby)
                {
                    if (!weight.ContainsKey(closeOnPositionNearby)) weight.Add(closeOnPositionNearby, 0);
                    weight[closeOnPositionNearby] += 1;
                    if ((weight[closeOnPositionNearby] > 4) && (open.Contains(closeOnPositionNearby))) closeOn(closeOnPositionNearby);
                }
            };


            while (open.Count > 0 && closed.Count < size)
            {
                var currentTile = open.Random();
                closeOn(currentTile);
            }

            foreach(var position in closed)
            {
                var tile = new Tile(this, position);
                this.TilesByPosition.Add(tile.Position, tile);
                this.World.TilesByPosition.Add(tile.Position, tile);
            }
        }
    }
}
