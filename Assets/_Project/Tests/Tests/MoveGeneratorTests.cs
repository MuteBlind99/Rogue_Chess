using Chess.Core;
using NUnit.Framework;

namespace Chess.Core.Tests
{
    public class MoveGeneratorTests
    {
        [Test]
        public void Knight_OnB1_Has2Moves()
        {
            Board board=Board.CreateStandard();
            var moves = MoveGenerator.GetMoves(board, Coord.Parse("b1"));
            Assert.AreEqual(2, moves.Count);
            Assert.Contains(Move.Parse("b1-a3"),moves);
            Assert.Contains(Move.Parse("b1-c3"),moves);
        }
        [Test]
        public void King_OnD4_Has8Moves()
        {
            Board board = new Board();
            board.Set(Coord.Parse("d4"), new Piece(PieceType.King,Side.White));
            var moves = MoveGenerator.GetMoves(board, Coord.Parse("d4"));
            Assert.AreEqual(8, moves.Count);
            Assert.Contains(Move.Parse("d4-d5"),moves);
            Assert.Contains(Move.Parse("d4-c3"),moves);
            Assert.Contains(Move.Parse("d4-e4"),moves);
        }
        [Test]
        public void King_OnA1_Has3Moves()
        {
            Board board = new Board();
            board.Set(Coord.Parse("a1"), new Piece(PieceType.King,Side.White));
            var moves = MoveGenerator.GetMoves(board, Coord.Parse("a1"));
            Assert.AreEqual(3, moves.Count);
            Assert.Contains(Move.Parse("a1-a2"),moves);
            Assert.Contains(Move.Parse("a1-b1"),moves);
            Assert.Contains(Move.Parse("a1-b2"),moves);
        }
        [Test]
        public void Rook_OnD4_Has14Moves()
        {
            Board board = new Board();
            board.Set(Coord.Parse("d4"), new Piece(PieceType.Rook,Side.White));
            var moves = MoveGenerator.GetMoves(board, Coord.Parse("d4"));
            Assert.AreEqual(14, moves.Count);
            Assert.Contains(Move.Parse("d4-d1"),moves);
            Assert.Contains(Move.Parse("d4-d8"),moves);
            Assert.Contains(Move.Parse("d4-h4"),moves);
            Assert.Contains(Move.Parse("d4-a4"),moves);
        }
        [Test]
        public void Rook_StopsAfterCapturingEnemy()
        {
            Board board = new Board();
            board.Set(Coord.Parse("a1"), new Piece(PieceType.Rook,Side.White));
            board.Set(Coord.Parse("a4"), new Piece(PieceType.Pawn,Side.Black));
            var moves = MoveGenerator.GetMoves(board, Coord.Parse("a1"));
            Assert.AreEqual(10, moves.Count);
            Assert.Contains(Move.Parse("a1-a4"),moves);
            CollectionAssert.DoesNotContain(moves,Move.Parse("a1-a5"));
        }
        [Test]
        public void Bishop_OnD4_Has13Moves()
        {
            Board board = new Board();
            board.Set(Coord.Parse("d4"), new Piece(PieceType.Bishop,Side.White));
            var moves = MoveGenerator.GetMoves(board, Coord.Parse("d4"));
            Assert.AreEqual(13, moves.Count);
            Assert.Contains(Move.Parse("d4-g1"),moves);
            Assert.Contains(Move.Parse("d4-a7"),moves);
            Assert.Contains(Move.Parse("d4-a1"),moves);
            Assert.Contains(Move.Parse("d4-h8"),moves);
        }
        [Test]
        public void Queen_OnD4_Has27Moves()
        {
            Board board = new Board();
            board.Set(Coord.Parse("d4"), new Piece(PieceType.Queen,Side.White));
            var moves = MoveGenerator.GetMoves(board, Coord.Parse("d4"));
            Assert.AreEqual(27, moves.Count);
            Assert.Contains(Move.Parse("d4-g1"),moves);
            Assert.Contains(Move.Parse("d4-a7"),moves);
            Assert.Contains(Move.Parse("d4-a1"),moves);
            Assert.Contains(Move.Parse("d4-h8"),moves);
            Assert.Contains(Move.Parse("d4-d1"),moves);
            Assert.Contains(Move.Parse("d4-d8"),moves);
            Assert.Contains(Move.Parse("d4-h4"),moves);
            Assert.Contains(Move.Parse("d4-a4"),moves);
        }
        [Test]
        public void Pawn_OnE3_Has1Move()
        {
            Board board = new Board();
            board.Set(Coord.Parse("e3"), new Piece(PieceType.Pawn,Side.White));
            var moves = MoveGenerator.GetMoves(board, Coord.Parse("e3"));
            Assert.AreEqual(1, moves.Count);
            Assert.Contains(Move.Parse("e3-e4"),moves);
            
        }
        [Test]
        public void Pawn_OnE2_Has2Moves()
        {
            Board board = new Board();
            board.Set(Coord.Parse("e2"), new Piece(PieceType.Pawn,Side.White));
            var moves = MoveGenerator.GetMoves(board, Coord.Parse("e2"));
            Assert.AreEqual(2, moves.Count);
            Assert.Contains(Move.Parse("e2-e3"),moves);
            Assert.Contains(Move.Parse("e2-e4"),moves);
        }
        [Test]
        public void PawnBlack_OnE7_Has2Moves()
        {
            Board board = new Board();
            board.Set(Coord.Parse("e7"), new Piece(PieceType.Pawn,Side.Black));
            var moves = MoveGenerator.GetMoves(board, Coord.Parse("e7"));
            Assert.AreEqual(2, moves.Count);
            Assert.Contains(Move.Parse("e7-e6"),moves);
            Assert.Contains(Move.Parse("e7-e5"),moves);
        }
        [Test]
        public void PawnBlack_CannotCaptureForward()
        {
            Board board = new Board();
            board.Set(Coord.Parse("e5"), new Piece(PieceType.Pawn,Side.Black));
            board.Set(Coord.Parse("e4"), new Piece(PieceType.Pawn,Side.White));
            var moves = MoveGenerator.GetMoves(board, Coord.Parse("e5"));
            Assert.AreEqual(0, moves.Count);
            CollectionAssert.DoesNotContain(moves,Move.Parse("e5-e4"));
        }
        [Test]
        public void PawnWhite_CapturesDiagonally()
        {
            Board board = new Board();
            board.Set(Coord.Parse("d5"), new Piece(PieceType.Pawn,Side.Black));
            board.Set(Coord.Parse("f5"), new Piece(PieceType.Pawn,Side.Black));
            board.Set(Coord.Parse("e4"), new Piece(PieceType.Pawn,Side.White));
            var moves = MoveGenerator.GetMoves(board, Coord.Parse("e4"));
            Assert.AreEqual(3, moves.Count);
            Assert.Contains(Move.Parse("e4-e5"),moves);
            Assert.Contains(Move.Parse("e4-d5"),moves);
            Assert.Contains(Move.Parse("e4-f5"),moves);
        }
    }
}

