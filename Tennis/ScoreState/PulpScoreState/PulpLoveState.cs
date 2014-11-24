using System;

namespace Olf.TechEx.Tennis.ScoreState.PulpScoreState
{
    internal class PulpLoveState : IPulpScoreState
    {
        private readonly Func<Player, IPulpScoreState> _fifteenStateFactory;
        
        public Player Player { get; private set; }

        public PulpLoveState(Player player, Func<Player, IPulpScoreState> fifteenStateFactory)
        {
            Player = player;
            _fifteenStateFactory = fifteenStateFactory;
        }

        public IState Point()
        {
            return _fifteenStateFactory(Player);
        }

        public override string ToString()
        {
            return "Love";
        }
    }
}