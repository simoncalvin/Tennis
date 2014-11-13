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
        private IPulpScoreState _thirtyState;
        private FifteenState _target;

        [TestInitialize]
        public void BeforeEach()
        {
            _thirtyState = new Mock<IPulpScoreState>(MockBehavior.Strict).Object;
            _target = new FifteenState(_thirtyState);
        }

        [TestMethod]
        public void Point_ReturnsThirtyState()
        {
            var actual = _target.Point();

            Assert.AreEqual(_thirtyState, actual);
        }

        [TestMethod]
        public void ToString_ReturnsFifteen()
        {
            const string expected = "Fifteen";

            var actual = _target.ToString();

            Assert.AreEqual(expected, actual);
        }
    }
}
