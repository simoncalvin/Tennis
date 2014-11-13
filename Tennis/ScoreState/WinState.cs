namespace Olf.TechEx.Tennis.ScoreState
{
    internal class WinState: IScoreState
    {
        private readonly Player _winningPlayer;

        public WinState(Player winningPlayer)
        {
            _winningPlayer = winningPlayer;
        }

        public IScoreState Point(Player player)
        {
            return this;
        }

        public override string ToString()
        {
            const string format = "Winner: {0}";
            return string.Format(format, _winningPlayer);
        }
    }
}
