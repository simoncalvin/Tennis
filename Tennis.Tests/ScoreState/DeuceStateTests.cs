using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Olf.TechEx.Tennis.ScoreState;

namespace Olf.TechEx.Tennis.Tests.ScoreState
{
    [TestClass]
    public class DeuceStateTests
    {
        private MockRepository _repo;

        private DeuceState _target;
        private Mock<Func<Player, IScoreState>> _advantageStateFactory;

        [TestInitialize]
        public void BeforeEach()
        {
            _repo = new MockRepository(MockBehavior.Strict);

            _advantageStateFactory = _repo.Create<Func<Player, IScoreState>>();

            _target = new DeuceState(_advantageStateFactory.Object);
        }

        [TestMethod]
        public void Point_FrankScores_ReturnsAdvantageStateForFrank()
        {
            var expected = _repo.Create<IScoreState>().Object;

            _advantageStateFactory.Setup(x => x(Player.Frank)).Returns(expected);

            var actual = _target.Point(Player.Frank);

            _advantageStateFactory.Verify(x => x(Player.Frank));

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Point_LolaScores_ReturnsAdvantageStateForLola()
        {
            var expected = _repo.Create<IScoreState>().Object;

            _advantageStateFactory.Setup(x => x(Player.Lola)).Returns(expected);

            var actual = _target.Point(Player.Lola);

            _advantageStateFactory.Verify(x => x(Player.Lola));

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToString_ReturnsDeuce()
        {
            const string expected = "Deuce";

            var actual = _target.ToString();

            Assert.AreEqual(expected, actual);
        }
    }
}
