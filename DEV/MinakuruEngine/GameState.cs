
namespace Minakuru.Engine;

public class GameState
{
	public Board Board { get; set; }

	public List<Move> PlayedMoves { get; init; } = [];

	public int WhiteTimeInMilliseconds { get; set; }
	public int BlackTimeInMilliseconds { get; set; }
	public int WhiteIncrementInMilliseconds { get; set; }
	public int BlackIncrementInMilliseconds { get; set; }
}

