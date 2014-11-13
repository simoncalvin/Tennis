using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Olf.TechEx.Tennis.ScoreState;
using Olf.TechEx.Tennis.ScoreState.PulpScoreState;

namespace Olf.TechEx.Tennis.Tests.ScoreState
{
    [TestClass]
    public class PulpStateTests
    {
        private MockRepository _repo;

        private Mock<IScoreState> _deuceState;

        private Func<Player, IPulpScoreState, IPulpScoreState, PulpState> _targetFactory;

        [TestInitialize]
        public void BeforeEach()
        {
            _repo = new MockRepository(MockBehavior.Strict);

            _deuceState = _repo.Create<IScoreState>();
            
            _targetFactory = (player, playerState, otherState) => new PulpState(player, otherState, playerState, _deuceState.Object);
        }

        [TestMethod]
        public void Point_ScoringStateReturnsPulpScoreState_ReturnsSelf()
        {
            var frankState = _repo.Create<IPulpScoreState>();
            var lolaState = _repo.Create<IPulpScoreState>();
            
            frankState.Setup(x => x.Point()).Returns(_repo.Create<IPulpScoreState>().Object);

            var target = _targetFactory(Player.Frank, lolaState.Object, frankState.Object);
            var actual = target.Point(Player.Frank);

            frankState.Verify(x => x.Point());

            Assert.AreEqual(target, actual);
        }

        [TestMethod]
        public void Point_ScoringStateReturnsFortyTied_ReturnsDeuceState()
        {
            var frankState = _repo.Create<IPulpScoreState>();
            var lolaState = new FortyState(null);

            frankState.Setup(x => x.Point()).Returns(new FortyState(null));

            var actual = _targetFactory(Player.Frank, lolaState, frankState.Object).Point(Player.Frank);

            frankState.Verify(x => x.Point());

            Assert.AreEqual(_deuceState.Object, actual);
        }

        [TestMethod]
        public void Point_ChildStateReturnsNonPulpScoreState_ReturnsThatState()
        {
            var frankState = _repo.Create<IPulpScoreState>();
            var lolaState = _repo.Create<IPulpScoreState>();

            var expected = _repo.Create<IScoreState>().Object;

            frankState.Setup(x => x.Point()).Returns(expected);

            var actual = _targetFactory(Player.Frank, lolaState.Object, frankState.Object).Point(Player.Frank);

            frankState.Verify(x => x.Point());

            Assert.AreEqual(expected, actual);            
        }

        [TestMethod]
        public void ToString_ReturnsFormattedScoreString()
        {
            // this incidentally tests that the ctor is assigning the states to the right players

            const string expected = "unlikely - preposterous";

            var frankState = _repo.Create<IPulpScoreState>();
            var lolaState = _repo.Create<IPulpScoreState>();

            frankState.Setup(x => x.ToString()).Returns("unlikely");
            lolaState.Setup(x => x.ToString()).Returns("preposterous");

            var actual = _targetFactory(Player.Lola, frankState.Object, lolaState.Object).ToString();

            Assert.AreEqual(expected, actual);
        }
    }
}
