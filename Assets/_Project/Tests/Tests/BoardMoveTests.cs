using System.Collections;
using NUnit.Framework;
using UnityEditor.VersionControl;

namespace Chess.Core.Tests
{
    public class BoardMoveTests
    {
        [Test]
        public void MakeMove_MovesPieceToTarget()
        {
            Board board = Board.CreateStandard();
            Piece pawn = board.Get(Coord.Parse("e2"));

            MoveRecord record = board.MakeMove(Move.Parse("e2-e4"));

            Assert.AreSame(pawn, board.Get(Coord.Parse("e4"))); // le MÊME pion est arrivé en e4
            Assert.IsNull(board.Get(Coord.Parse("e2"))); // e2 est vide
            Assert.IsNull(record.Captured); // rien n'a été capturé
        }

        [Test]
        public void MakeMove_ReturnsCapturedPiece()
        {
            Board board = new Board();
            Piece rook = new Piece(PieceType.Rook, Side.White);
            Piece bishop = new Piece(PieceType.Bishop, Side.Black);
            board.Set(Coord.Parse("a1"), rook);
            board.Set(Coord.Parse("a5"), bishop);

            MoveRecord record = board.MakeMove(Move.Parse("a1-a5"));

            Assert.AreSame(bishop, record.Captured); // c'est bien CE fou qui est renvoyé
            Assert.AreSame(rook, board.Get(Coord.Parse("a5"))); // la tour l'a remplacé
            Assert.IsNull(board.Get(Coord.Parse("a1")));
        }

        [Test]
        public void UndoMove_RestoresSimpleMove()
        {
            Board board = Board.CreateStandard();
            Piece pawn = board.Get(Coord.Parse("e2"));
            Move move = Move.Parse("e2-e4");

            MoveRecord record = board.MakeMove(move);
            board.UndoMove(record);

            Assert.AreSame(pawn, board.Get(Coord.Parse("e2"))); // le pion est revenu
            Assert.IsNull(board.Get(Coord.Parse("e4"))); // e4 est de nouveau vide
        }

        [Test]
        public void UndoMove_RestoresCapturedPiece()
        {
            Board board = new Board();
            Piece rook = new Piece(PieceType.Rook, Side.White);
            Piece bishop = new Piece(PieceType.Bishop, Side.Black);
            board.Set(Coord.Parse("a1"), rook);
            board.Set(Coord.Parse("a5"), bishop);
            Move move = Move.Parse("a1-a5");

            MoveRecord record = board.MakeMove(move);
            board.UndoMove(record);

            Assert.AreSame(rook, board.Get(Coord.Parse("a1"))); // la tour est revenue
            Assert.AreSame(bishop, board.Get(Coord.Parse("a5"))); // le fou est ressuscité
        }

        [Test]
        public void MakeMove_FromEmptySquare_Throws()
        {
            Board board = new Board();
            Assert.Throws<System.InvalidOperationException>(() => board.MakeMove(Move.Parse("e2-e4")));
        }

        [Test]
        public void WhitePawn_OnLastRank_BecomesQueen()
        {
            Board board = new Board();
            board.Set(Coord.Parse("e7"), new Piece(PieceType.Pawn, Side.White));

            board.MakeMove(Move.Parse("e7-e8"));

            Piece promoted = board.Get(Coord.Parse("e8"));
            Assert.AreEqual(PieceType.Queen, promoted.Type);
            Assert.AreEqual(Side.White, promoted.Side);
        }

        [Test]
        public void BlackPawn_OnFirstRank_BecomeQueen()
        {
            Board board = new Board();
            board.Set(Coord.Parse("e2"), new Piece(PieceType.Pawn, Side.Black));

            board.MakeMove(Move.Parse("e2-e1"));

            Piece promoted = board.Get(Coord.Parse("e1"));
            Assert.AreEqual(PieceType.Queen, promoted.Type);
            Assert.AreEqual(Side.Black, promoted.Side);
        }

        [Test]
        public void UndoPromotionk_RestoresOriginPawn()
        {
            Board board = new Board();
            Piece pawn = new Piece(PieceType.Pawn, Side.White);
            board.Set(Coord.Parse("e7"), pawn);

            MoveRecord record = board.MakeMove(Move.Parse("e7-e8"));
            board.UndoMove(record);

            Assert.AreSame(pawn, board.Get(Coord.Parse("e7")));
            Assert.IsNull(board.Get(Coord.Parse("e8")));
        }

        [Test]
        public void UndoPromotionWhiteCapture_RestoreBothPieces()
        {
            Board board = new Board();
            Piece pawn = new Piece(PieceType.Pawn, Side.White);
            Piece rook = new Piece(PieceType.Rook, Side.Black);
            board.Set(Coord.Parse("e7"), pawn);
            board.Set(Coord.Parse("d8"), rook);

            MoveRecord record = board.MakeMove(Move.Parse("e7-d8"));
            Assert.AreEqual(PieceType.Queen, board.Get(Coord.Parse("d8")).Type);

            board.UndoMove(record);

            Assert.AreSame(pawn, board.Get(Coord.Parse("e7")));
            Assert.AreSame(rook, board.Get(Coord.Parse("d8")));
        }
    }
}