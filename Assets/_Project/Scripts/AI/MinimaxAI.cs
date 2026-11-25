using System.Collections.Generic;
using Chess.Core;

namespace Chess.AI
{
    public static class MinimaxAI
    {
        public const int WinScore = 100_000;
        
        public const int Infinity = 1_000_000;

        public static Move? ChooseMove(Board board, Side side, int depth)
        {
            Move? bestMove = null;
            int bestScore =-Infinity;
            foreach (Move move in MoveGenerator.GetAllMoves(board, side))
            {
                int score = ScoreMove(board, move, depth,side);
                if (score > bestScore)
                {
                    bestScore = score;
                    bestMove = move;
                }
            }
            return bestMove;
        }

        private static int Search(Board board, int depth, Side side)
        {
            if(depth == 0)
                return Evaluate(board, side);
            
            List<Move> moves = MoveGenerator.GetAllMoves(board, side);
            if(moves.Count == 0)
                return Evaluate(board, side);
            
            int best = -Infinity;
            foreach (Move move in moves)
            {
                int score= ScoreMove(board, move, depth , side);
                if(score > best)
                    best = score;
            }
            return best;
        }
        private static int ScoreMove(Board board, Move move, int depth, Side side)
        {
            MoveRecord record =board.MakeMove(move);
            int score;
            if (record.Captured != null && record.Captured.Type == PieceType.King)
            {
                score = WinScore+depth;
            }
            else
            {
                score=-Search(board, depth - 1, side.Opponent());
            }
            board.UndoMove(record);
            return score;
        }

        private static int Evaluate(Board board, Side side)
        {
            int sign = side == Side.White ? 1 : -1;
            return sign * Evaluator.Evaluate(board);
        }
    }
}
