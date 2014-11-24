using System;

namespace Olf.TechEx.Tennis.ScoreState.PulpScoreState
{
    internal class FortyState : IPulpScoreState
    {
        private readonly Player _player;
        private readonly Func<Player, IScoreState> _winStateFactory;

        public Player Player
        {
            get { return _player; }
        }

        public FortyState(Player player, Func<Player, IScoreState> winStateFactory)
        {
            _player = player;
            _winStateFactory = winStateFactory;
        }

        public IState Point()
        {
            return _winStateFactory(_player);
        }

        public override string ToString()
        {
            return "Forty";
        }
    }
}