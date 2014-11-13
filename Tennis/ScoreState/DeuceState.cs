using System;

namespace Olf.TechEx.Tennis.ScoreState
{
    internal class DeuceState : IScoreState
    {
        private readonly Func<Player, IScoreState> _advantageStateFactory;

        public DeuceState(Func<Player, IScoreState> advantageStateFactory)
        {
            _advantageStateFactory = advantageStateFactory;
        }

        public IScoreState Point(Player player)
        {
            return _advantageStateFactory(player);
        }

        public override string ToString()
        {
            return "Deuce";
        }
    }
}
