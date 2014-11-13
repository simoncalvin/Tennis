using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Olf.TechEx.Tennis.ScoreState;

namespace Olf.TechEx.Tennis.Tests.ScoreState
{
    [TestClass]
    public class AdvantageStateTests
    {
        private MockRepository _repo;

        private Mock<IScoreState> _deuceState;
        private Mock<Func<Player, IScoreState>> _winFactory;
        private Func<Player, AdvantageState> _targetFactory;

        [TestInitialize]
        public void BeforeEach()
        {
            _repo = new MockRepository(MockBehavior.Strict);

            _deuceState = _repo.Create<IScoreState>();
            _winFactory = _repo.Create<Func<Player, IScoreState>>();

            _targetFactory = p => new AdvantageState(p, _deuceState.Object, _winFactory.Object);
        }

        [TestMethod]
        public void Point_AdvantagePlayerScores_ReturnsWinState()
        {
            var expected = _repo.Create<IScoreState>().Object;

            _winFactory.Setup(x => x(Player.Frank)).Returns(expected);

            var actual = _targetFactory(Player.Frank).Point(Player.Frank);

            _winFactory.Verify(x => x(Player.Frank));

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Point_TrailingPlayerScores_ReturnsDeuceState()
        {
            var actual = _targetFactory(Player.Frank).Point(Player.Lola);

            Assert.AreEqual(_deuceState.Object, actual);
        }

        [TestMethod]
        public void ToString_FrankHasAdvantage_ReturnsAdvantageColonFrank()
        {
            const string expected = "Advantage: Frank";

            var actual = _targetFactory(Player.Frank).ToString();

            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void ToString_LolaHasAdvantage_ReturnsAdvantageColonLola()
        {
            const string expected = "Advantage: Lola";

            var actual = _targetFactory(Player.Lola).ToString();

            Assert.AreEqual(expected, actual);
        }


    }
}
