using Chess.AI;
using Chess.Core;

namespace Chess.AI
{
    public static class GreedyAI
    {
        public static Move? ChooseMove(Board board, Side side)
        {
            int sign = side == Side.White ? 1 : -1;
            Move? bestMove = null;
            int bestScore = int.MinValue;
            foreach (Move move in MoveGenerator.GetAllMoves(board, side))
            {
                MoveRecord record = board.MakeMove(move);
                
                int score = sign * Evaluator.Evaluate(board);
                
                board.UndoMove(record);

                if (score > bestScore)
                {
                    bestScore = score;
                    bestMove = move;
                }
            }
            return bestMove;
        }
    }
}