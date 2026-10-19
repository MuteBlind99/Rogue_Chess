namespace Chess.Core
{
    /// <summary>Le camp d'une pièce. Le joueur joue les blancs.</summary>
    public enum Side
    {
        White,
        Black
    }

    public static class SideExtensions
    {
        /// <summary>Renvoie le camp adverse : White.Opponent() == Black.</summary>
        public static Side Opponent(this Side side) => side == Side.White ? Side.Black : Side.White;
    }
}
