using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetro.AgeOfTetro
{
    public sealed class Tile
    {
        public readonly Chunk Chunk;
        public World World { get { return this.Chunk.World; } }
        public Engine Engine { get { return this.Chunk.Engine; } }
        public readonly Position Position;
        public bool IsLand;

        public Tile(Chunk chunk, Position position)
        {
            this.Chunk = chunk;
            this.Position = position;
            this.IsLand = true;
        }
    }
}
