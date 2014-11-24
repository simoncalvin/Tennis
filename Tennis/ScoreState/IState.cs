namespace Olf.TechEx.Tennis.ScoreState
{
    internal interface IState
    {
        string ToString();
    }

    internal interface IScoreState : IState
    {
        IScoreState Point(Player player);
    }

    internal interface IPulpScoreState : IState
    {
        Player Player { get; }
        IState Point();
    }
}
