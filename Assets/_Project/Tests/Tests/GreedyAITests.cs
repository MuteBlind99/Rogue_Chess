using System.Collections.Generic;
using Chess.AI;
using NUnit.Framework;
using Chess.Core;

namespace Chess.Core.Tests
{
    public class GreedyAITests
    {
        [Test]
        public void White_CapturesMostValuablePiece()
        {
            var board = new Board();
            board.Set(Coord.Parse("a1"), new Piece(PieceType.Rook, Side.White));
            board.Set(Coord.Parse("a8"), new Piece(PieceType.Queen, Side.Black));
            board.Set(Coord.Parse("h1"), new Piece(PieceType.Pawn, Side.Black));

            Move? move = GreedyAI.ChooseMove(board, Side.White);

            Assert.AreEqual(Move.Parse("a1-a8"), move);
        }

        [Test]
        public void Black_CapturesMostValuablePiece()
        {
            var board = new Board();
            board.Set(Coord.Parse("a8"), new Piece(PieceType.Rook, Side.Black));
            board.Set(Coord.Parse("a1"), new Piece(PieceType.Queen, Side.White));
            board.Set(Coord.Parse("h8"), new Piece(PieceType.Pawn, Side.White));

            Move? move = GreedyAI.ChooseMove(board, Side.Black);

            Assert.AreEqual(Move.Parse("a8-a1"), move);
        }

        [Test]
        public void CapturesKingBeforeQueen()
        {
            var board = new Board();
            board.Set(Coord.Parse("a1"), new Piece(PieceType.Rook, Side.White));
            board.Set(Coord.Parse("a8"), new Piece(PieceType.King, Side.Black));
            board.Set(Coord.Parse("h1"), new Piece(PieceType.Queen, Side.Black));

            Move? move = GreedyAI.ChooseMove(board, Side.White);

            Assert.AreEqual(Move.Parse("a1-a8"), move);
        }

        [Test]
        public void NoPieces_ReturnsNull()
        {
            Move? move = GreedyAI.ChooseMove(new Board(), Side.White);

            Assert.IsNull(move);
        }

        [Test]
        public void ChooseMove_LeavesBoardUnchanged()
        {
            Board board = Board.CreateStandard();
            var before = new List<(Coord, Piece)>(board.AllPieces());
            GreedyAI.ChooseMove(board, Side.White);
            var after = new List<(Coord, Piece)>(board.AllPieces());
            CollectionAssert.AreEqual(before, after);
        }
    }
}