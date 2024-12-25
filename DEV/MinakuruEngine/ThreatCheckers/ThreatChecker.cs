namespace Minakuru.Engine.ThreatCheckers;

public class ThreatChecker : IThreatChecker
{
	private readonly IThreatChecker[] _threatCheckers;

	public ThreatChecker()
	{
		_threatCheckers = [
			// Note:
			// // - Queens are checked via straight+diagonal
			// // - Rook are checked via straight
			// // - Bishops are checked via diagonal
			new StraightLineThreatChecker(),
			new DiagonalThreatChecker(),
			new KnightThreatChecker(),
			new PawnThreatChecker(),
			new KingThreatChecker()
			];
	}

	public bool IsUnderAttack(Board board, byte targetFieldNo, Color attackedByColor)
	{
		return _threatCheckers.Any(m => m.IsUnderAttack(board, targetFieldNo, attackedByColor));
	}
}
