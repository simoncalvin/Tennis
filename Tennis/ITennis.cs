namespace Olf.TechEx.Tennis
{
    public interface ITennis
    {
        /// <summary>
        /// Scores a point for the specified player.
        /// </summary>
        /// <param name="player">The player who socred the point.</param>
        void Point(Player player);

        /// <summary>
        /// Gets the current score of the tennis game in the form of:
        /// "Love - Love", "Love - 15", "Advantage [player]", "[player] Wins!"
        /// </summary>
        string Score { get; }
    }
}
