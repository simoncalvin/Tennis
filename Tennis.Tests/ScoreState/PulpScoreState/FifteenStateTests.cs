using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Olf.TechEx.Tennis.ScoreState;
using Olf.TechEx.Tennis.ScoreState.PulpScoreState;

namespace Olf.TechEx.Tennis.Tests.ScoreState.PulpScoreState
{
    [TestClass]
    public class FifteenStateTests
    {
        private Mock<Func<Player, IPulpScoreState>> _thirtyStateFactory;
        private MockRepository _repo;
        private Func<Player, FifteenState> _targetFactory;

        [TestInitialize]
        public void BeforeEach()
        {
            _repo = new MockRepository(MockBehavior.Strict);

            _thirtyStateFactory = _repo.Create<Func<Player, IPulpScoreState>>();
            _targetFactory = p => new FifteenState(p, _thirtyStateFactory.Object);
        }

        [TestMethod]
        public void Point_InitWithFrank_ReturnsThirtyStateForFrank()
        {
            var expected = _repo.OneOf<IPulpScoreState>();

            _thirtyStateFactory.Setup(x => x(Player.Frank)).Returns(expected);

            var actual = _targetFactory(Player.Frank).Point();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Point_InitWithLola_ReturnsThirtyStateForLola()
        {
            var expected = _repo.OneOf<IPulpScoreState>();

            _thirtyStateFactory.Setup(x => x(Player.Lola)).Returns(expected);

            var actual = _targetFactory(Player.Lola).Point();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Player_InitWithFrank_ReturnsFrank()
        {
            Assert.AreEqual(Player.Frank, _targetFactory(Player.Frank).Player);
        }

        [TestMethod]
        public void Player_InitWithLola_ReturnsLola()
        {
            Assert.AreEqual(Player.Lola, _targetFactory(Player.Lola).Player);
        }

        [TestMethod]
        public void ToString_ReturnsFifteen()
        {
            const string expected = "Fifteen";

            var actual = _targetFactory(Player.Frank).ToString();

            Assert.AreEqual(expected, actual);
        }
    }
}
