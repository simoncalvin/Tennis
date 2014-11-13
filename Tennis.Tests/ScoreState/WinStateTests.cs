using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Olf.TechEx.Tennis.ScoreState;

namespace Olf.TechEx.Tennis.Tests.ScoreState
{
    [TestClass]
    public class WinStateTests
    {
        private Func<Player, WinState> _targetFactory;

        [TestInitialize]
        public void BeforeEach()
        {
            _targetFactory = p => new WinState(p);
        }

        [TestMethod]
        public void Point_ReturnsSelf()
        {
            var target = _targetFactory(Player.Frank);

            var actual = target.Point(Player.Frank);

            Assert.AreEqual(target, actual);
        }

        [TestMethod]
        public void ToString_FrankWins_ReturnsWinnerColonFrank()
        {
            const string expected = "Winner: Frank";

            var actual = _targetFactory(Player.Frank).ToString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToString_LolaWins_ReturnsWinnerColonLola()
        {
            const string expected = "Winner: Lola";

            var actual = _targetFactory(Player.Lola).ToString();

            Assert.AreEqual(expected, actual);
        }
    }
}
