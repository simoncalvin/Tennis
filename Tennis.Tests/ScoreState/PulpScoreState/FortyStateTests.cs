using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Olf.TechEx.Tennis.ScoreState;
using Olf.TechEx.Tennis.ScoreState.PulpScoreState;

namespace Olf.TechEx.Tennis.Tests.ScoreState.PulpScoreState
{
    [TestClass]
    public class FortyStateTests
    {
        private MockRepository _repo;
        private Mock<Func<Player, IScoreState>> _winStateFactory;
        private FortyState _target;

        [TestInitialize]
        public void BeforeEach()
        {
            _repo = new MockRepository(MockBehavior.Strict);

            _winStateFactory = _repo.Create<Func<Player, IScoreState>>();

            _target = new FortyState(Player.Frank, _winStateFactory.Object);
        }

        [TestMethod]
        public void Point_ReturnsWinState()
        {
            var winState = _repo.Create<IScoreState>().Object;

            _winStateFactory.Setup(x => x(Player.Frank)).Returns(winState);

            var actual = _target.Point();

            Assert.AreEqual(winState, actual);
        }

        [TestMethod]
        public void ToString_ReturnsThirty()
        {
            const string expected = "Forty";

            var actual = _target.ToString();

            Assert.AreEqual(expected, actual);
        }
    }
}
