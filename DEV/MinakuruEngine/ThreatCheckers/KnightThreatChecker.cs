using Minakuru.Engine.Fields;

namespace Minakuru.Engine.ThreatCheckers;

public class KnightThreatChecker : IThreatChecker
{
	private static readonly ulong[] _bitmasks = KnightBitmasks.KnightFieldBitmasks;

	public bool IsUnderAttack(Board board, byte targetFieldNo, Color attackedByColor)
	{
		var opponentKnights = attackedByColor == Color.White ? board.WhiteKnights : board.BlackKnights;
		return (opponentKnights & _bitmasks[targetFieldNo]) != 0;
	}
}
