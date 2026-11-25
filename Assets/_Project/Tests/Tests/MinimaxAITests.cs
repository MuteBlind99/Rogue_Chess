using System.Collections.Generic;
using Chess.AI;
using Chess.Core;
using NUnit.Framework;

namespace Chess.Core.Tests
{
    public class MinimaxAITests
    {
        // Dame blanche en d1 ; pion noir en d5, protégé par le pion noir en e6.
        private static Board CreateProtectedPawnTrap()
        {
            var board = new Board();
            board.Set(Coord.Parse("d1"), new Piece(PieceType.Queen, Side.White));
            board.Set(Coord.Parse("d5"), new Piece(PieceType.Pawn, Side.Black));
            board.Set(Coord.Parse("e6"), new Piece(PieceType.Pawn, Side.Black));
            return board;
        }

        [Test]
        public void Greedy_FallsIntoTrap()
        {
            Move? move = GreedyAI.ChooseMove(CreateProtectedPawnTrap(), Side.White);

            Assert.AreEqual(Move.Parse("d1-d5"), move);
        }

        [Test]
        public void Minimax_AvoidsTrap()
        {
            Move? move = MinimaxAI.ChooseMove(CreateProtectedPawnTrap(), Side.White, 2);

            Assert.IsNotNull(move);
            Assert.AreNotEqual(Move.Parse("d1-d5"), move);
        }

        [Test]
        public void Depth1_CapturesMostValuablePiece()
        {
            var board = new Board();
            board.Set(Coord.Parse("a1"), new Piece(PieceType.Rook, Side.White));
            board.Set(Coord.Parse("a8"), new Piece(PieceType.Queen, Side.Black));
            board.Set(Coord.Parse("h1"), new Piece(PieceType.Pawn, Side.Black));

            Move? move = MinimaxAI.ChooseMove(board, Side.White, 1);

            Assert.AreEqual(Move.Parse("a1-a8"), move);
        }

        [Test]
        public void CapturesKingWhenPossible()
        {
            var board = new Board();
            board.Set(Coord.Parse("a1"), new Piece(PieceType.Rook, Side.White));
            board.Set(Coord.Parse("a8"), new Piece(PieceType.King, Side.Black));

            Move? move = MinimaxAI.ChooseMove(board, Side.White, 3);

            Assert.AreEqual(Move.Parse("a1-a8"), move);
        }

        [Test]
        public void ChooseMove_LeavesBoardUnchanged()
        {
            Board board = Board.CreateStandard();
            var before = new List<(Coord, Piece)>(board.AllPieces());

            MinimaxAI.ChooseMove(board, Side.White, 3);

            var after = new List<(Coord, Piece)>(board.AllPieces());
            CollectionAssert.AreEqual(before, after);
        }
    }
}