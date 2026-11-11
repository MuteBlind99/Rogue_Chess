using System;
using Chess.Core;
using NUnit.Framework;

namespace Chess.Core.Tests
{
    public class GameStateTests
    {
        // A Test behaves as an ordinary method
        [Test]
        public void NewGame_WhiteToMove_NotOver()
        {
            var game = new GameState(Board.CreateStandard());

            Assert.AreEqual(Side.White, game.SideToMove);
            Assert.IsFalse(game.IsOver);
            Assert.IsNull(game.Winner);
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [Test]
        public void StartingPosition_HAs20Moves()
        {
            var game = new GameState(Board.CreateStandard());
            Assert.AreEqual(20, game.GetAllMoves().Count);
        }

        [Test]
        public void Play_SwitchsSideToMove()
        {
            var game = new GameState(Board.CreateStandard());
            game.Play(Move.Parse("e2-e4"));
            Assert.AreEqual(Side.Black, game.SideToMove);
        }

        [Test]
        public void Play_ImpossibleMove_Throws()
        {
            var game = new GameState(Board.CreateStandard());

            Assert.Throws<InvalidOperationException>(() => game.Play(Move.Parse("e7-e5")));
        }

        [Test]
        public void CapturingKing_EndsGame()
        {
            var board = new Board();
            board.Set(Coord.Parse("a1"), new Piece(PieceType.Rook, Side.White));
            board.Set(Coord.Parse("a8"), new Piece(PieceType.King, Side.Black));
            var game = new GameState(board);

            game.Play(Move.Parse("a1-a8"));

            Assert.IsTrue(game.IsOver);
            Assert.AreEqual(Side.White, game.Winner);
        }

        [Test]
        public void Play_AfterGameOver_Throws()
        {
            var board = new Board();
            board.Set(Coord.Parse("a1"), new Piece(PieceType.Rook, Side.White));
            board.Set(Coord.Parse("a8"), new Piece(PieceType.King, Side.Black));
            var game = new GameState(board);

            game.Play(Move.Parse("a1-a8"));
            Assert.Throws<InvalidOperationException>(() => game.Play(Move.Parse("a8-a7")));
        }
    }
}