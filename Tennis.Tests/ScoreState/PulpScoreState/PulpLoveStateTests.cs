using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Olf.TechEx.Tennis.ScoreState;
using Olf.TechEx.Tennis.ScoreState.PulpScoreState;

namespace Olf.TechEx.Tennis.Tests.ScoreState.PulpScoreState
{
    [TestClass]
    public class PulpLoveStateTests
    {
        private IPulpScoreState _fifteenState;
        
        private PulpLoveState _target;

        [TestInitialize]
        public void BeforeEach()
        {
            _fifteenState = new Mock<IPulpScoreState>(MockBehavior.Strict).Object;

            _target = new PulpLoveState(_fifteenState);
        }

        [TestMethod]
        public void Point_ReturnsFifteenState()
        {
            var actual = _target.Point();

            Assert.AreEqual(_fifteenState, actual);
        }

        [TestMethod]
        public void ToString_ReturnsLove()
        {
            const string expected = "Love";

            var actual = _target.ToString();

            Assert.AreEqual(expected, actual);
        }
    }
}
