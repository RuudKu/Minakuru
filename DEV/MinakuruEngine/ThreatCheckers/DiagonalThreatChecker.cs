using Minakuru.Engine.Fields;

namespace Minakuru.Engine.ThreatCheckers;

public class DiagonalThreatChecker : IThreatChecker
{
	private static readonly ulong[,][] _diagonalPerDirectionBitmasks = DiagonalBitmasks.DiagonalPerDirectionFieldBitmasks;
	private static readonly ulong[] _diagonalAllDirectionsBitmasks = DiagonalBitmasks.DiagonalAllDirectionsFieldBitmasks;

	public bool IsUnderAttack(Board board, byte targetFieldNo, Color attackedByColor)
	{
		var piecesAt = board.Pieces;

		var opponentBishopsQueens = attackedByColor == Color.White ? board.WhiteQueens | board.WhiteBishops : board.BlackQueens | board.BlackBishops;

		if ((opponentBishopsQueens & _diagonalAllDirectionsBitmasks[targetFieldNo]) == 0)
		{
			return false;
		}

		for (int direction = 0; direction < 4; direction++)
		{
			bool occupied = false;
			ulong[] fieldIds = _diagonalPerDirectionBitmasks[targetFieldNo, direction];
			byte i = 0;
			while (!occupied && i < fieldIds.Length)
			{
				var fieldId = fieldIds[i];
				if ((piecesAt & fieldId) != 0)
				{
					if ((opponentBishopsQueens & fieldId) != 0)
					{
						return true;
					}
					occupied = true;
				}
				i++;
			}
		}

		return false;
	}
}
