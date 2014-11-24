using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Olf.TechEx.Tennis.ScoreState;
using Olf.TechEx.Tennis.ScoreState.PulpScoreState;

namespace Olf.TechEx.Tennis.Tests.ScoreState.PulpScoreState
{
    [TestClass]
    public class PulpLoveStateTests
    {
        private MockRepository _repo;
        private Mock<Func<Player, IPulpScoreState>> _fifteenStateFactory;
        private Func<Player, PulpLoveState> _targetFactory;

        [TestInitialize]
        public void BeforeEach()
        {
            _repo = new MockRepository(MockBehavior.Strict);

            _fifteenStateFactory = _repo.Create<Func<Player, IPulpScoreState>>(MockBehavior.Strict);

            _targetFactory = p => new PulpLoveState(p, _fifteenStateFactory.Object);
        }

        [TestMethod]
        public void Point_InitWithFrank_ReturnsFifteenStateForFrank()
        {
            var expected = _repo.OneOf<IPulpScoreState>();

            _fifteenStateFactory.Setup(x => x(Player.Frank)).Returns(expected);

            var actual = _targetFactory(Player.Frank).Point();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Point_InitWithLola_ReturnsFifteenStateForLola()
        {
            var expected = _repo.OneOf<IPulpScoreState>();

            _fifteenStateFactory.Setup(x => x(Player.Lola)).Returns(expected);

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
        public void ToString_ReturnsLove()
        {
            const string expected = "Love";

            var actual = _targetFactory(Player.Frank).ToString();

            Assert.AreEqual(expected, actual);
        }
    }
}
