using Chess.Core;

namespace Chess.AI
{
    public static class Evaluator
    {
        public const int KingValue = 1000;

        public static int PieceValue(PieceType type)
        {
            return type switch
            {
                PieceType.Pawn => 1,
                PieceType.Knight => 3,
                PieceType.Bishop => 3,
                PieceType.Rook => 5,
                PieceType.Queen => 9,
                PieceType.King => KingValue,
                _ => 0
            };
        }

        public static int Evaluate(Board board)
        {
            int score = 0;
            foreach (var (_, piece) in board.AllPieces())
            {
                int value = PieceValue(piece.Type);

                if (piece.Side == Side.White)
                    score += value;
                else
                    score -= value;
            }

            return score;
        }
    }
}