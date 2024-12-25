namespace Minakuru.Engine.ThreatCheckers;

public class PawnThreatChecker : IThreatChecker
{
	public bool IsUnderAttack(Board board, byte targetFieldNo, Color attackedByColor)
	{
		var opponentPawns = attackedByColor == Color.White ? board.WhitePawns : board.BlackPawns;

		var fromColumnNo = targetFieldNo % 8;
		ulong pawnsFilter = 0L;

		if (fromColumnNo > 0)
		{
			if (attackedByColor == Color.White)
			{
				pawnsFilter |= (ulong)1 << targetFieldNo - 8 - 1;
			}
			else if (attackedByColor == Color.Black)
			{
				pawnsFilter |= (ulong)1 << targetFieldNo + 8 - 1;
			}
		}
		if (fromColumnNo < 7)
		{
			if (attackedByColor == Color.White)
			{
				pawnsFilter |= (ulong)1 << targetFieldNo - 8 + 1;
			}
			else if (attackedByColor == Color.Black)
			{
				pawnsFilter |= (ulong)1 << targetFieldNo + 8 + 1;
			}
		}

		return (opponentPawns & pawnsFilter) != 0;
	}
}
