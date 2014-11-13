using System;

namespace Olf.TechEx.Tennis.ScoreState
{
    internal class LoveState : IScoreState
    {
        private readonly Func<Player, IScoreState> _pulpStateFactory;

        public LoveState(Func<Player, IScoreState> pulpStateFactory)
        {
            _pulpStateFactory = pulpStateFactory;
        }

        public IScoreState Point(Player player)
        {
            return _pulpStateFactory(player);
        }

        public override string ToString()
        {
            return "Love";
        }
    }
}
