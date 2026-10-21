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

        private static readonly Coord[] KingOffsets =
        {
            new Coord(1, 1), new Coord(1, -1), new Coord(-1, -1), new Coord(-1, 1),
            new Coord(0, 1), new Coord(-1, 0), new Coord(0, -1), new Coord(1, 0)
        };

        private static readonly Coord[] RookDirections =
        {
            new Coord(0, 1), new Coord(0, -1), new Coord(1, 0), new Coord(-1, 0)
        };

        private static readonly Coord[] BishopDirections =
        {
            new Coord(1, 1), new Coord(1, -1), new Coord(-1, 1), new Coord(-1, -1)
        };

        private static readonly Coord[] PawnDirections =
        {
            new Coord(0, 1), new Coord(0, -1), new Coord(1, 0), new Coord(-1, 0),
            new Coord(1, 1), new Coord(1, -1), new Coord(-1, 1), new Coord(-1, -1)
        };

        public static List<Move> GetMoves(Board board, Coord from)
        {
            List<Move> moves = new List<Move>();
            Piece piece = board.Get(from);


            if (piece == null)
                return moves;

            if (piece.Type == PieceType.Knight)
                AddJumpMoves(board, from, piece, KnightOffsets, moves);
            else if (piece.Type == PieceType.King)
                AddJumpMoves(board, from, piece, KingOffsets, moves);
            else if (piece.Type == PieceType.Rook)
                AddSlideMoves(board, from, piece, RookDirections, moves);
            else if (piece.Type == PieceType.Bishop)
                AddSlideMoves(board, from, piece, BishopDirections, moves);
            else if (piece.Type == PieceType.Queen)
            {
                AddSlideMoves(board, from, piece, BishopDirections, moves);
                AddSlideMoves(board, from, piece, RookDirections, moves);
            }
            else if (piece.Type == PieceType.Pawn)
                AddPawnMoves(board, from, piece, moves);

            return moves;
        }

        // private static void AddKingMoves(Board board, Coord from, Piece piece, List<Move> moves)
        // {
        //     foreach (Coord offset in KingOffsets)
        //     {
        //         Coord to = from + offset;
        //
        //         if (!board.Contains(to))
        //             continue;
        //         Piece target = board.Get(to);
        //
        //         if (target == null || target.Side != piece.Side)
        //             moves.Add(new Move(from, to));
        //     }
        // }
        //
        // private static void AddKnightMoves(Board board, Coord from, Piece piece, List<Move> moves)
        // {
        //     foreach (Coord offset in KnightOffsets)
        //     {
        //         Coord to = from + offset;
        //
        //         if (!board.Contains(to))
        //             continue;
        //         Piece target = board.Get(to);
        //
        //         if (target == null || target.Side != piece.Side)
        //             moves.Add(new Move(from, to));
        //     }
        // }

        private static void AddJumpMoves(Board board, Coord from, Piece piece, Coord[] offsets, List<Move> moves)
        {
            foreach (Coord offset in offsets)
            {
                Coord to = from + offset;

                if (!board.Contains(to))
                    continue;
                Piece target = board.Get(to);

                if (target == null || target.Side != piece.Side)
                    moves.Add(new Move(from, to));
            }
        }

        private static void AddSlideMoves(Board board, Coord from, Piece piece, Coord[] directions, List<Move> moves)
        {
            foreach (Coord direction in directions)
            {
                Coord to = from + direction;

                while (board.Contains(to))
                {
                    Piece target = board.Get(to);

                    if (target == null)
                    {
                        moves.Add(new Move(from, to));
                        to = to + direction;
                    }
                    else if (target.Side != piece.Side)
                    {
                        moves.Add(new Move(from, to));
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        private static void AddPawnMoves(Board board, Coord from, Piece piece, List<Move> moves)
        {
            int forward = piece.Side == Side.White ? 1 : -1;

            Coord to = from + new Coord(0, forward);

            if (board.Contains(to))
            {
                Piece target = board.Get(to);
                if (target == null)
                {
                    moves.Add(new Move(from, to));
                    int startRank = piece.Side == Side.White ? 1 : 6;
                    Coord doubleTo = from + new Coord(0, 2 * forward);
                    
                    if(from.Rank == startRank && board.Contains(doubleTo) && board.Get(doubleTo) == null)
                        moves.Add(new Move(from, doubleTo));
                }
            }
            Coord[] captureOffsets = {new Coord(-1,forward), new Coord(1,forward)};
            foreach (Coord offset in captureOffsets)
            {
                Coord targetCoord = from + offset;
                
                if (!board.Contains(targetCoord))
                    continue;
                
                Piece target = board.Get(targetCoord);

                if (target != null && target.Side != piece.Side)
                    moves.Add(new Move(from, targetCoord));
            }
        }
    }
}