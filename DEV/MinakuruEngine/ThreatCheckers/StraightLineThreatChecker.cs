namespace Minakuru.Engine.ThreatCheckers;

public class StraightLineThreatChecker : IThreatChecker
{
	public bool IsUnderAttack(Board board, byte targetFieldNo, Color attackedByColor)
	{
		var piecesAt = board.Pieces;

		var opponentRooksQueens = attackedByColor == Color.White ? board.WhiteQueens | board.WhiteRooks : board.BlackQueens | board.BlackRooks;

		var toColumnNo = targetFieldNo % 8;
		var toRowNo = targetFieldNo / 8;

		int currentColumn;
		int currentRow;

		for (int direction = 0; direction < 4; direction++)
		{
			int deltaColumn = new int[] { -1, +1, +0, +0 }[direction];
			int deltaRow = new int[] { +0, 0, -1, +1 }[direction];
			currentColumn = toColumnNo + deltaColumn;
			currentRow = toRowNo + deltaRow;

			bool ownPiece = false;
			bool occupied = false;
			while (currentColumn >= 0 && currentColumn < 8 && currentRow >= 0 && currentRow < 8 && !ownPiece && !occupied)
			{
				byte to = (byte)(8 * currentRow + currentColumn);
				ulong toFilter = (ulong)1 << to;

				if ((piecesAt & toFilter) != 0)
				{
					if ((opponentRooksQueens & toFilter) != 0)
					{
						return true;
					}
					occupied = true;
				}

				currentColumn += deltaColumn;
				currentRow += deltaRow;
			}
		}

		return false;
	}
}
