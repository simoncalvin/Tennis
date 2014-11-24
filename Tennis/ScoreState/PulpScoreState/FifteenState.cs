using System;

namespace Olf.TechEx.Tennis.ScoreState.PulpScoreState
{
    internal class FifteenState : IPulpScoreState
    {
        private readonly Func<Player, IPulpScoreState> _thirtyStateFactory;

        public Player Player { get; private set; }

        public FifteenState(Player player, Func<Player, IPulpScoreState> thirtyStateFactory)
        {
            Player = player;
            _thirtyStateFactory = thirtyStateFactory;
        }

        public IState Point()
        {
            return _thirtyStateFactory(Player);
        }

        public override string ToString()
        {
            return "Fifteen";
        }
    }
}