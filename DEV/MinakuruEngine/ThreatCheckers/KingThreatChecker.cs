namespace Engine.ThreatCheckers;

public class KingThreatChecker : IThreatChecker
{
	public bool IsUnderAttack(Board board, byte targetFieldNo, Color color)
	{
		var king = color == Color.White ? board.WhiteKing : board.BlackKing;

		var toColumnNo = targetFieldNo % 8;
		var toRowNo = targetFieldNo / 8;
		ulong kingFilter = 0L;
		for (int option = 0; option < 8; option++)
		{
			int deltaColumn = new int[] { -1, -1, -1, 0, 0, +1, +1, +1 }[option];
			int deltaRow = new int[] { -1, 0, +1, -1, +1, -1, +0, +1 }[option];
			int fromColumn = toColumnNo + deltaColumn;
			int fromRow = toRowNo + deltaRow;

			if (fromColumn >= 0 && fromColumn < 8 && fromRow >= 0 && fromRow < 8)
			{
				byte fromFieldNo = (byte)(8 * fromRow + fromColumn);
				ulong fromFilter = (ulong)1 << fromFieldNo;
				kingFilter |= fromFilter;
			}
		}
		return (king & kingFilter) != 0;
	}
}
