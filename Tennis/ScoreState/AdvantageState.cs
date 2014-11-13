using System;

namespace Olf.TechEx.Tennis.ScoreState
{
    internal class AdvantageState : IScoreState
    {
        private readonly Player _advantagePlayer;
        private readonly IScoreState _deuceState;
        private readonly Func<Player, IScoreState> _winStateFactory;

        public AdvantageState(Player advantagePlayer, IScoreState deuceState, Func<Player, IScoreState> winStateFactory)
        {
            _advantagePlayer = advantagePlayer;
            _deuceState = deuceState;
            _winStateFactory = winStateFactory;
        }

        public IScoreState Point(Player player)
        {
            if (player == _advantagePlayer)
                return _winStateFactory(player);

            return _deuceState;
        }

        public override string ToString()
        {
            return string.Format("Advantage: {0}", _advantagePlayer);
        }
    }
}