using Chess.AI;
using Chess.Core;
using NUnit.Framework;

namespace Chess.Core.Tests
{
    public class EvaluatorTest
    {
        [Test]
        public void StartingPosition_IsEqual()
        {
            Assert.AreEqual(0, Evaluator.Evaluate(Board.CreateStandard()));
        }

        [Test]
        public void WhiteQueenAlone_Is9()
        {
            var board = new Board();
            board.Set(Coord.Parse("d1"), new Piece(PieceType.Queen, Side.White));
            Assert.AreEqual(9, Evaluator.Evaluate(board));
        }
        [Test]
        public void BlackRookAlone_IsMinus5()
        {
            var board = new Board();
            board.Set(Coord.Parse("a8"), new Piece(PieceType.Rook, Side.Black));
            Assert.AreEqual(-5, Evaluator.Evaluate(board));
        }
        [Test]
        public void WhiteQueenAgainstBlackRook_Is4()
        {
            var board = new Board();
            board.Set(Coord.Parse("d1"), new Piece(PieceType.Queen, Side.White));
            board.Set(Coord.Parse("a8"), new Piece(PieceType.Rook, Side.Black));
            Assert.AreEqual(4, Evaluator.Evaluate(board));
        }
        [Test]
        public void TwoKings_IsEqual()
        {
            var board = new Board();
            board.Set(Coord.Parse("e1"), new Piece(PieceType.King, Side.White));
            board.Set(Coord.Parse("e8"), new Piece(PieceType.King, Side.Black));
            Assert.AreEqual(0, Evaluator.Evaluate(board));
        }
        [Test]
        public void WhiteKingAlone_IsKingValue()
        {
            var board = new Board();
            board.Set(Coord.Parse("e1"), new Piece(PieceType.King, Side.White));
            Assert.AreEqual(Evaluator.KingValue, Evaluator.Evaluate(board));
        }
    }
}