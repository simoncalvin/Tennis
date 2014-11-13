using System;
using System.Collections.Generic;
using Olf.TechEx.Tennis.ScoreState;

namespace Olf.TechEx.Tennis
{
    public sealed class Tennis : ITennis
    {
        private IScoreState _state;

        public IDictionary<Player, int> PlayerScore { get; set; }

        internal Tennis(Func<IScoreState> stateFactory)
        {
            _state = stateFactory();
        }

        public void Point(Player player)
        {
            _state = _state.Point(player);
        }

        public string Score
        {
            get { return _state.ToString(); }
        }
    }
}