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
        private IPulpScoreState _fortyState;
        private ThirtyState _target;

        [TestInitialize]
        public void BeforeEach()
        {
            _fortyState = new Mock<IPulpScoreState>(MockBehavior.Strict).Object;

            _target = new ThirtyState(_fortyState);
        }

        [TestMethod]
        public void Point_ReturnsFortyState()
        {
            var actual = _target.Point();

            Assert.AreEqual(_fortyState, actual);
        }

        [TestMethod]
        public void ToString_ReturnsThirty()
        {
            const string expected = "Thirty";

            var actual = _target.ToString();

            Assert.AreEqual(expected, actual);
        }
    }
}
