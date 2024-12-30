namespace Minakuru.Engine.ThreatCheckers.Experimental;

public class KnightThreatChecker : IThreatChecker
{
	private static readonly ulong[] _bitmasks = new ulong[64];

	static KnightThreatChecker()
	{
		for (int targetFieldNo = 0; targetFieldNo < 64; targetFieldNo++)
		{
			var toColumnNo = targetFieldNo % 8;
			var toRowNo = targetFieldNo / 8;
			ulong knightsFilter = 0L;
			for (int option = 0; option < 8; option++)
			{
				int deltaColumn = new int[] { -2, -2, -1, -1, +1, +1, +2, +2 }[option];
				int deltaRow = new int[] { -1, +1, -2, +2, -2, 2, -1, 1 }[option];
				int fromColumn = toColumnNo + deltaColumn;
				int fromRow = toRowNo + deltaRow;

				if (fromColumn >= 0 && fromColumn < 8 && fromRow >= 0 && fromRow < 8)
				{
					byte fromFieldNo = (byte)(8 * fromRow + fromColumn);
					ulong fromFilter = (ulong)1 << fromFieldNo;
					knightsFilter |= fromFilter;
				}
			}
			_bitmasks[targetFieldNo] = knightsFilter;
		}
	}

	public bool IsUnderAttack(Board board, byte targetFieldNo, Color attackedByColor)
	{
		var opponentKnights = attackedByColor == Color.White ? board.WhiteKnights : board.BlackKnights;
		return (opponentKnights & _bitmasks[targetFieldNo]) != 0;
	}
}
