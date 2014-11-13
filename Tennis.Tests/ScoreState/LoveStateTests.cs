using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Olf.TechEx.Tennis.ScoreState;

namespace Olf.TechEx.Tennis.Tests.ScoreState
{
    [TestClass]
    public class LoveStateTests
    {
        private MockRepository _repo;

        private Mock<Func<Player, IScoreState>> _factory;
        private LoveState _target;

        [TestInitialize]
        public void BeforeEach()
        {
            _repo = new MockRepository(MockBehavior.Strict);

            _factory = _repo.Create<Func<Player, IScoreState>>();
            _target = new LoveState(_factory.Object);
        }

        [TestMethod]
        public void Point_InitsAndReturnsPulpState()
        {
            const Player player = Player.Frank;

            var expected = _repo.Create<IScoreState>().Object;

            _factory.Setup(x => x(player)).Returns(expected);
            
            var actual = _target.Point(player);

            _factory.Verify(x => x(player));
            
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToString_ReturnsLove()
        {
            Assert.AreEqual("Love", _target.ToString());
        }
    }
}
