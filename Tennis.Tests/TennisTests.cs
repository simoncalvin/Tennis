using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Olf.TechEx.Tennis.ScoreState;

namespace Olf.TechEx.Tennis.Tests
{
    [TestClass]
    public class TennisTests
    {
        private MockRepository _repo;
        private Mock<Func<IScoreState>> _stateFactory;
        private Tennis _target;
        private Mock<IScoreState> _state;

        [TestInitialize]
        public void BeforeEach()
        {
            _repo = new MockRepository(MockBehavior.Strict);

            _stateFactory = _repo.Create<Func<IScoreState>>();
            _state = _repo.Create<IScoreState>();

            _stateFactory.Setup(x => x()).Returns(_state.Object);

            _target = new Tennis(_stateFactory.Object);
        }

        [TestMethod]
        public void Ctor_GetsStateFromFactory()
        {
            _stateFactory.Verify(x => x());
        }

        [TestMethod]
        public void Point_PassesPlayerToState()
        {
            _state.Setup(x => x.Point(Player.Frank)).Returns((IScoreState)null);

            _target.Point(Player.Frank);

            _state.Verify(x => x.Point(Player.Frank));
        }

        [TestMethod]
        public void Point_ReplacesCurrentState()
        {
            var newState = _repo.Create<IScoreState>();

            _state.Setup(x => x.Point(Player.Frank)).Returns(newState.Object);
            newState.Setup(x => x.Point(Player.Lola)).Returns((IScoreState)null);

            _target.Point(Player.Frank);
            _target.Point(Player.Lola);

            _state.Verify(x => x.Point(Player.Frank));
            newState.Verify(x => x.Point(Player.Lola));
        }

        [TestMethod]
        public void Score_ReturnsStringifiedState()
        {
            const string expected = "a very unlikely return value";

            _state.Setup(x => x.ToString()).Returns(expected);

            var actual = _target.Score;

            _state.Verify(x => x.ToString());

            Assert.AreEqual(expected, actual);
        }
    }
}
