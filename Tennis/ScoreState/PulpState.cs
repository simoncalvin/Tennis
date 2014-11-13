using System;
using System.Collections.Generic;
using System.Linq;
using Olf.TechEx.Tennis.ScoreState.PulpScoreState;

namespace Olf.TechEx.Tennis.ScoreState
{
    internal class PulpState : IScoreState
    {
        private readonly IScoreState _deuceState;

        private readonly Dictionary<Player, IPulpScoreState> _states = new Dictionary<Player, IPulpScoreState>
        {
            {Player.Frank, null},
            {Player.Lola, null}
        };

        public PulpState(Player player, IPulpScoreState fifteenState, IPulpScoreState pulpLoveState, IScoreState deuceState)
        {
            _states[player] = fifteenState;
            _states[GetOtherPlayer(player)] = pulpLoveState;
            
            _deuceState = deuceState;
        }

        public IScoreState Point(Player player)
        {
            var state = _states[player].Point();

            if (state is IScoreState)
                return state as IScoreState;

            _states[player] = state as IPulpScoreState;

            if (_states.All(x => x.Value is FortyState))
                return _deuceState;

            return this;
        }

        public override string ToString()
        {
            const string scoreFormat = "{0} - {1}";

            return string.Format(scoreFormat, _states[Player.Frank].ToString(), _states[Player.Lola].ToString());
        }

        private static Player GetOtherPlayer(Player player)
        {
            // (•_•) 
            // ( •_•)>⌐■-■ 
            // (⌐■_■)
            // deal with it
            return (Player)((int)player ^ 1);
        }
    }
}
