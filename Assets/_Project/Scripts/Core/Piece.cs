namespace Chess.Core
{
    /// <summary>
    /// Une pièce posée sur le plateau. C'est une classe (et non une struct) parce qu'elle
    /// portera plus tard un état qui change : compétences, charges, bouclier...
    /// </summary>
    public class Piece
    {
        public PieceType Type { get; }
        public Side Side { get; }

        /// <summary>Utile pour le premier double pas du pion.</summary>
        public bool HasMoved { get; set; }

        public Piece(PieceType type, Side side)
        {
            Type = type;
            Side = side;
        }

        public override string ToString() => $"{Side} {Type}";
    }
}
