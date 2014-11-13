using System;

namespace Olf.TechEx.Tennis.ScoreState.PulpScoreState
{
    internal class ThirtyState : IPulpScoreState
    {
        private readonly IPulpScoreState _fortyState;

        public ThirtyState(IPulpScoreState fortyState)
        {
            _fortyState = fortyState;
        }

        public IState Point()
        {
            return _fortyState;
        }

        public override string ToString()
        {
            return "Thirty";
        }
    }
}