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
    }
}

