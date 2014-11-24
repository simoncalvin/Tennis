using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Olf.TechEx.Tennis.ScoreState;
using Olf.TechEx.Tennis.ScoreState.PulpScoreState;

namespace Olf.TechEx.Tennis.Tests.ScoreState.PulpScoreState
{
    [TestClass]
    public class ThirtyStateTests
    {
        private MockRepository _repo;
        private Mock<Func<Player, IPulpScoreState>> _fortyStateFactory;
        private Func<Player, ThirtyState> _targetFactory;

        [TestInitialize]
        public void BeforeEach()
        {
            _repo = new MockRepository(MockBehavior.Strict);
            _fortyStateFactory = _repo.Create<Func<Player, IPulpScoreState>>();

            _targetFactory = p => new ThirtyState(p, _fortyStateFactory.Object);
        }

        [TestMethod]
        public void Point_InitWithFrank_ReturnsFortyStateForFrank()
        {
            var fortyState = _repo.OneOf<IPulpScoreState>();
            _fortyStateFactory.Setup(x=>x(Player.Frank)).Returns(fortyState);
            
            var actual = _targetFactory(Player.Frank).Point();

            _fortyStateFactory.Verify(x=>x(Player.Frank));

            Assert.AreEqual(fortyState, actual);
        }

        [TestMethod]
        public void Point_InitWithLola_ReturnsFortyStateForLola()
        {
            var fortyState = _repo.OneOf<IPulpScoreState>();
            _fortyStateFactory.Setup(x => x(Player.Lola)).Returns(fortyState);

            var actual = _targetFactory(Player.Lola).Point();

            _fortyStateFactory.Verify(x => x(Player.Lola));

            Assert.AreEqual(fortyState, actual);
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
        public void ToString_ReturnsThirty()
        {
            const string expected = "Thirty";

            var actual = _targetFactory(Player.Frank).ToString();

            Assert.AreEqual(expected, actual);
        }
    }
}
