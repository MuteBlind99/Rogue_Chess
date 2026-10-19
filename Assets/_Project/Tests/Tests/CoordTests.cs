using Chess.Core;
using NUnit.Framework;

namespace Chess.Core.Tests
{
    public class CoordTests
    {
        [Test]
        public void Addition_AppliesOffset()
        {
            Coord result = Coord.Parse("e1") + new Coord(0, 1);
            Assert.AreEqual(Coord.Parse("e2"), result);
        }

        [Test]
        public void ToString_UsesChessNotation()
        {
            Assert.AreEqual("e1", new Coord(4, 0).ToString());
            Assert.AreEqual("h8", new Coord(7, 7).ToString());
        }

        [Test]
        public void Parse_ReadsChessNotation()
        {
            Assert.AreEqual(new Coord(0, 0), Coord.Parse("a1"));
            Assert.AreEqual(new Coord(3, 7), Coord.Parse("d8"));
        }

        [Test]
        public void Opponent_ReturnsOtherSide()
        {
            Assert.AreEqual(Side.Black, Side.White.Opponent());
            Assert.AreEqual(Side.White, Side.Black.Opponent());
        }
    }
}
