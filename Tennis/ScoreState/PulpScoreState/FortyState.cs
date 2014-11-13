using System;

namespace Olf.TechEx.Tennis.ScoreState.PulpScoreState
{
    internal class FortyState : IPulpScoreState
    {
        private readonly IScoreState _winState;

        public FortyState(IScoreState winState)
        {
            _winState = winState;
        }

        public IState Point()
        {
            return _winState;
        }

        public override string ToString()
        {
            return "Forty";
        }
    }
}