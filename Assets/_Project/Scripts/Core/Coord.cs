using System;

namespace Chess.Core
{
    /// <summary>
    /// Une case du plateau. File = colonne (0 = a), Rank = rangée (0 = rangée 1).
    /// Struct immuable : on la copie comme un int, sans allocation.
    /// </summary>
    public readonly struct Coord : IEquatable<Coord>
    {
        public readonly int File;
        public readonly int Rank;

        public Coord(int file, int rank)
        {
            File = file;
            Rank = rank;
        }

        // Addition de deux coordonnées : sert à appliquer un déplacement (ex. e1 + (0, 1) = e2).
        public static Coord operator +(Coord a, Coord b) => new Coord(a.File + b.File, a.Rank + b.Rank);

        public static bool operator ==(Coord a, Coord b) => a.Equals(b);
        public static bool operator !=(Coord a, Coord b) => !a.Equals(b);

        public bool Equals(Coord other) => File == other.File && Rank == other.Rank;
        public override bool Equals(object obj) => obj is Coord other && Equals(other);
        public override int GetHashCode() => HashCode.Combine(File, Rank);

        /// <summary>Notation échecs : (4, 0) devient "e1".</summary>
        public override string ToString() => $"{(char)('a' + File)}{Rank + 1}";

        /// <summary>Lit une notation échecs : "e1" devient (4, 0). Pratique dans les tests.</summary>
        public static Coord Parse(string square)
        {
            if (square == null || square.Length != 2)
                throw new ArgumentException($"Case invalide : '{square}'", nameof(square));
            string[] parts = square.Split(',');

            return new Coord(square[0] - 'a', square[1] - '1');
        }
    }
}
