using System.Collections.Generic;

namespace Chess.Core
{
    public static class MoveGenerator
    { 
        private static readonly Coord[] KnightOffsets =
        {
            new Coord(1, 2), new Coord(2, 1), new Coord(2, -1), new Coord(1, -2),
            new Coord(-1, -2), new Coord(-2, -1), new Coord(-2, 1), new Coord(-1, 2)
        };
        public static List<Move> GetMoves(Board board, Coord from)
        {
            List<Move> moves = new List<Move>();
            Piece piece = board.Get(from);
            
            if(piece==null)
                return moves;
            
            if(piece.Type == PieceType.Knight)
                AddKnightMoves(board, from, piece, moves);
            
            return moves;
        }
        private static void AddKnightMoves(Board board, Coord from, Piece piece, List<Move> moves)
        {
            foreach (Coord offset in KnightOffsets)
            {
                Coord to = from + offset;

                if (!board.Contains(to))
                    continue;
                Piece target = board.Get(to);
                
                if(target == null || target.Side != piece.Side)
                    moves.Add(new Move(from, to));
            }
        }
    }

}
