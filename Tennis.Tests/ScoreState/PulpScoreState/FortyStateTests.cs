using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Olf.TechEx.Tennis.ScoreState;
using Olf.TechEx.Tennis.ScoreState.PulpScoreState;

namespace Olf.TechEx.Tennis.Tests.ScoreState.PulpScoreState
{
    [TestClass]
    public class FortyStateTests
    {
        private IScoreState _winState;
        private FortyState _target;

        [TestInitialize]
        public void BeforeEach()
        {
            _winState = new Mock<IScoreState>(MockBehavior.Strict).Object;

            _target = new FortyState(_winState);
        }

        [TestMethod]
        public void Point_ReturnsFortyState()
        {
            var actual = _target.Point();

            Assert.AreEqual(_winState, actual);
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
