namespace Minakuru.Engine.ThreatCheckers;

public class PawnThreatChecker : IThreatChecker
{
	private static readonly ulong[] _whiteBitmasks = Fields.PawnBitmasks.WhiteAttackedByFieldBitmasks;
	private static readonly ulong[] _blackBitmasks = Fields.PawnBitmasks.BlackAttackedByFieldBitmasks;

	public bool IsUnderAttack(Board board, byte targetFieldNo, Color attackedByColor)
	{
		var opponentPawns = attackedByColor == Color.White ? board.WhitePawns : board.BlackPawns;
		var opponentBitmask = attackedByColor == Color.White ? _whiteBitmasks[targetFieldNo] : _blackBitmasks[targetFieldNo];

		return (opponentPawns & opponentBitmask) != 0;
	}
}
