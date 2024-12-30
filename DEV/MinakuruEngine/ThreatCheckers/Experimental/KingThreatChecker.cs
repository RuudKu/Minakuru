namespace Minakuru.Engine.ThreatCheckers.Experimental;

public class KingThreatChecker : IThreatChecker
{
	private static readonly ulong[] _bitmasks = Fields.KingBitmasks.KingFieldBitmasks;

	public bool IsUnderAttack(Board board, byte targetFieldNo, Color attackedByColor)
	{
		var opponentKing = attackedByColor == Color.White ? board.WhiteKing : board.BlackKing;
		return (opponentKing & _bitmasks[targetFieldNo]) != 0;
	}
}
