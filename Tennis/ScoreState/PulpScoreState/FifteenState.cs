using System;

namespace Olf.TechEx.Tennis.ScoreState.PulpScoreState
{
    internal class FifteenState : IPulpScoreState
    {
        private readonly IPulpScoreState _thirtyState;

        public FifteenState(IPulpScoreState thirtyState)
        {
            _thirtyState = thirtyState;
        }

        public IState Point()
        {
            return _thirtyState;
        }

        public override string ToString()
        {
            return "Fifteen";
        }
    }
}