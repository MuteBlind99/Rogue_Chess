using System;
using System.Collections.Generic;

namespace Chess.Core
{
    /// <summary>
    /// Le plateau : une grille de cases, chacune vide (null) ou occupée par une pièce.
    /// La taille est paramétrable pour les boss à plateau réduit (7×7).
    /// </summary>
    public class Board
    {
        public const int DefaultSize = 8;

        private readonly Piece[,] _squares;

        public int Size { get; }

        public Board(int size = DefaultSize)
        {
            Size = size;
            _squares = new Piece[size, size];
        }

        /// <summary>Vrai si la case existe sur ce plateau.</summary>
        public bool Contains(Coord c) => c.File >= 0 && c.File < Size && c.Rank >= 0 && c.Rank < Size;

        /// <summary>La pièce sur cette case, ou null si elle est vide.</summary>
        public Piece Get(Coord c)
        {
            ThrowIfOutside(c);
            return _squares[c.File, c.Rank];
        }

        /// <summary>Pose une pièce sur une case (null pour la vider).</summary>
        public void Set(Coord c, Piece piece)
        {
            ThrowIfOutside(c);
            _squares[c.File, c.Rank] = piece;
        }

        public bool IsEmpty(Coord c) => Get(c) == null;

        /// <summary>Toutes les pièces du plateau avec leur case.</summary>
        public IEnumerable<(Coord coord, Piece piece)> AllPieces()
        {
            for (int file = 0; file < Size; file++)
            for (int rank = 0; rank < Size; rank++)
            {
                Piece piece = _squares[file, rank];
                if (piece != null)
                    yield return (new Coord(file, rank), piece);
            }
        }

        /// <summary>Crée un plateau 8×8 avec la position de départ classique.</summary>
        public static Board CreateStandard()
        {
            var board = new Board(DefaultSize);

            PieceType[] backRank =
            {
                PieceType.Rook, PieceType.Knight, PieceType.Bishop, PieceType.Queen,
                PieceType.King, PieceType.Bishop, PieceType.Knight, PieceType.Rook
            };

            for (int file = 0; file < DefaultSize; file++)
            {
                board.Set(new Coord(file, 0), new Piece(backRank[file], Side.White));
                board.Set(new Coord(file, 1), new Piece(PieceType.Pawn, Side.White));
                board.Set(new Coord(file, 6), new Piece(PieceType.Pawn, Side.Black));
                board.Set(new Coord(file, 7), new Piece(backRank[file], Side.Black));
            }

            return board;
        }

        private void ThrowIfOutside(Coord c)
        {
            if (!Contains(c))
                throw new ArgumentOutOfRangeException(nameof(c), $"La case {c} est hors du plateau.");
        }
    }
}
