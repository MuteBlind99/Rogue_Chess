using System;
using System.Collections.Generic;
using Chess.Core;

namespace Chess.Core
{
    public readonly struct MoveRecord
    {
        public readonly  Move Move;
        public readonly Piece Moved;
        public readonly Piece Captured;

        public MoveRecord(Move move, Piece moved, Piece captured)
        {
            Move = move;
            Moved = moved;
            Captured = captured;
        }
        
    }
}