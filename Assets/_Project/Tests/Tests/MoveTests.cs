using Chess.Core;
using NUnit.Framework;

namespace Chess.Core.Tests
{
    public class MoveTests
    {
        [Test]
        public void Parse_ReadRToStringFormat()
        {
            var move = new Move(Coord.Parse("g1"), Coord.Parse("f3"));
            Assert.AreEqual(move, Move.Parse(move.ToString()));
        }
    }

}
