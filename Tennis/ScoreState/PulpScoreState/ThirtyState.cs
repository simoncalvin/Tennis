using System;

namespace Olf.TechEx.Tennis.ScoreState.PulpScoreState
{
    internal class ThirtyState : IPulpScoreState
    {
        private readonly Func<Player, IPulpScoreState> _fortyStateFactory;
        
        public Player Player { get; private set; }

        public ThirtyState(Player player, Func<Player, IPulpScoreState> fortyStateFactory)
        {
            Player = player;
            _fortyStateFactory = fortyStateFactory;
        }

        public IState Point()
        {
            return _fortyStateFactory(Player);
        }

        public override string ToString()
        {
            return "Thirty";
        }
    }
}