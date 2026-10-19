using System;
using System.Linq;
using Chess.Core;
using NUnit.Framework;

namespace Chess.Core.Tests
{
    public class BoardTests
    {
        [Test]
        public void StandardBoard_Has32Pieces()
        {
            Board board = Board.CreateStandard();
            Assert.AreEqual(32, board.AllPieces().Count());
        }

        [Test]
        public void StandardBoard_WhiteKingIsOnE1()
        {
            Piece piece = Board.CreateStandard().Get(Coord.Parse("e1"));

            Assert.IsNotNull(piece);
            Assert.AreEqual(PieceType.King, piece.Type);
            Assert.AreEqual(Side.White, piece.Side);
        }

        [Test]
        public void StandardBoard_BlackQueenIsOnD8()
        {
            Piece piece = Board.CreateStandard().Get(Coord.Parse("d8"));

            Assert.AreEqual(PieceType.Queen, piece.Type);
            Assert.AreEqual(Side.Black, piece.Side);
        }

        [Test]
        public void StandardBoard_MiddleRanksAreEmpty()
        {
            Board board = Board.CreateStandard();

            for (int file = 0; file < 8; file++)
            for (int rank = 2; rank <= 5; rank++)
                Assert.IsTrue(board.IsEmpty(new Coord(file, rank)), $"{new Coord(file, rank)} devrait être vide");
        }

        [Test]
        public void Contains_RejectsSquaresOutsideBoard()
        {
            var board = new Board();

            Assert.IsTrue(board.Contains(new Coord(0, 0)));
            Assert.IsTrue(board.Contains(new Coord(7, 7)));
            Assert.IsFalse(board.Contains(new Coord(8, 0)));
            Assert.IsFalse(board.Contains(new Coord(0, -1)));
        }

        [Test]
        public void Get_OutsideBoard_Throws()
        {
            var board = new Board();
            Assert.Throws<ArgumentOutOfRangeException>(() => board.Get(new Coord(8, 8)));
        }

        [Test]
        public void SmallBoard_HasReducedSize()
        {
            var board = new Board(7);
            Assert.IsFalse(board.Contains(new Coord(7, 0)));
        }
    }
}
