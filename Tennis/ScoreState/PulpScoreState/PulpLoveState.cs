namespace Olf.TechEx.Tennis.ScoreState.PulpScoreState
{
    internal class PulpLoveState : IPulpScoreState
    {
        private readonly IPulpScoreState _fifteenState;

        public PulpLoveState(IPulpScoreState fifteenState)
        {
            _fifteenState = fifteenState;
        }

        public IState Point()
        {
            return _fifteenState;
        }

        public override string ToString()
        {
            return "Love";
        }
    }
}