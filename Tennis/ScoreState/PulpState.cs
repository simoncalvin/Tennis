using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Olf.TechEx.Tennis.ScoreState.PulpScoreState;

namespace Olf.TechEx.Tennis.ScoreState
{
    internal class PulpState : IScoreState
    {
        private readonly IScoreState _deuceState;

        private readonly IDictionary<Player, IPulpScoreState> _states;

        public PulpState(Player player, Func<Player, IPulpScoreState> fifteenState, Func<Player, IPulpScoreState> pulpLoveState, IScoreState deuceState)
        {
            _states = new[] {fifteenState(player), pulpLoveState(GetOtherPlayer(player))}.ToDictionary(x=>x.Player);
           
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
