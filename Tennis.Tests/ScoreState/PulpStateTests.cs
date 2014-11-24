using System;
using System.Collections.Generic;
using System.Reflection;
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
        private Func<IPulpScoreState, Func<Player, IPulpScoreState>> _fifteenStateFactoryFactory;
        private Func<IPulpScoreState, Func<Player, IPulpScoreState>> _loveStateFactoryFactory;

        private Func<Func<Player, IPulpScoreState>, Func<Player, IPulpScoreState>, PulpState> _targetFactory;

        [TestInitialize]
        public void BeforeEach()
        {
            _repo = new MockRepository(MockBehavior.Strict);

            _fifteenStateFactoryFactory = state => OnLoveStateFactory(state, Player.Frank);
            _loveStateFactoryFactory = state => OnLoveStateFactory(state, Player.Lola);

            _deuceState = _repo.Create<IScoreState>();

            _targetFactory = (f1, f2) => new PulpState(Player.Frank, f1, f2, _deuceState.Object);
        }

        private Func<Player, IPulpScoreState> OnLoveStateFactory(IPulpScoreState state, Player player)
        {
            var fac = _repo.Create<Func<Player, IPulpScoreState>>();
            fac.Setup(x => x(player)).Returns(state);
            return fac.Object;
        }

        [TestMethod]
        public void Ctor_GetsInitialStatesFromFactory()
        {
            var state = _repo.Create<IPulpScoreState>();

            var fifteenFactory = _fifteenStateFactoryFactory(state.Object);
            var loveFactory = _loveStateFactoryFactory(state.Object);

            var players = new Queue<Player>(new[] { Player.Frank, Player.Lola });

            state.SetupGet(x => x.Player).Returns(players.Dequeue);
            _targetFactory(fifteenFactory, loveFactory);

            state.VerifyGet(x => x.Player, Times.Exactly(2));

            Mock.Get(fifteenFactory).Verify(x => x(Player.Frank));
            Mock.Get(loveFactory).Verify(x => x(Player.Lola));
        }

        [TestMethod]
        public void Point_ScoringStateReturnsPulpScoreState_ReturnsSelf()
        {
            var frankState = GetScoreState(Player.Frank, Mock.Of<IPulpScoreState>());
            var lolaState = GetScoreState(Player.Lola);

            var target = _targetFactory(_fifteenStateFactoryFactory(frankState), _loveStateFactoryFactory(lolaState));
            var actual = target.Point(Player.Frank);

            Mock.Get(frankState).Verify(x => x.Point());

            Assert.AreEqual(target, actual);
        }

        [TestMethod]
        public void Point_ScoringStateReturnsFortyTied_ReturnsDeuceState()
        {
            var frankState = GetScoreState(Player.Frank, new FortyState(Player.Frank, null));
            var lolaState = new FortyState(Player.Lola, null);

            var actual = _targetFactory(_fifteenStateFactoryFactory(frankState), _loveStateFactoryFactory(lolaState)).Point(Player.Frank);

            Mock.Get(frankState).Verify(x => x.Point());

            Assert.AreEqual(_deuceState.Object, actual);
        }

        [TestMethod]
        public void Point_ChildStateReturnsNonPulpScoreState_ReturnsThatState()
        {
            var expected = Mock.Of<IScoreState>();

            var frankState = GetScoreState(Player.Frank, expected);
            var lolaState = GetScoreState(Player.Lola);

            var actual = _targetFactory(_fifteenStateFactoryFactory(frankState), _loveStateFactoryFactory(lolaState)).Point(Player.Frank);

            Mock.Get(frankState).Verify(x => x.Point());

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToString_ReturnsFormattedScoreString()
        {
            // this incidentally tests that the ctor is assigning the states to the right players

            const string expected = "unlikely - preposterous";

            var frankState = GetScoreState(Player.Frank, "unlikely");
            var lolaState = GetScoreState(Player.Lola, "preposterous");

            var actual = _targetFactory(_fifteenStateFactoryFactory(frankState), _loveStateFactoryFactory(lolaState)).ToString();

            Mock.Get(frankState).Verify(x => x.ToString());
            Mock.Get(lolaState).Verify(x => x.ToString());

            Assert.AreEqual(expected, actual);
        }

        private IPulpScoreState GetScoreState(Player player, string s)
        {
            return GetScoreState(player, null, s);
        }

        private IPulpScoreState GetScoreState(Player player, IState next = null, string s = null)
        {
            return _repo.OneOf<IPulpScoreState>(x => x.Player == player && x.Point() == next && x.ToString() == s);
        }
    }
}
