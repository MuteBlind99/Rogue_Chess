using System;

namespace Chess.Core
{
    public readonly struct Move : IEquatable<Move>
    {
        public readonly Coord From;
        public readonly Coord To;

        public Move(Coord from, Coord to)
        {
            From = from;
            To = to;
        }
        //public static Move operator +(Move a, Move b) => new Move(a.From + b.From, a.To + b.To);

        public static bool operator ==(Move a, Move b) => a.Equals(b);
        public static bool operator !=(Move a, Move b) => !a.Equals(b);

        public bool Equals(Move other) => From == other.From && To == other.To;
        public override bool Equals(object obj) => obj is Move other && Equals(other);
        public override int GetHashCode() => HashCode.Combine(From, To);

        public override string ToString() => $"{From}-{To}";

        public static Move Parse(string square)
        {
            if (square == null)
                throw new ArgumentException($"Case invalide : '{square}'", nameof(square));

            string[] coords;
            coords = square.Split('-');

            if (coords.Length != 2)
                throw new ArgumentException($"Case invalide : '{square}'", nameof(square));

            return new Move(Coord.Parse(coords[0]), Coord.Parse(coords[1]));
        }
    }
}