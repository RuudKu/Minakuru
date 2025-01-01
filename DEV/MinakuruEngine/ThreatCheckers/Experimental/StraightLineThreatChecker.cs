using Minakuru.Engine.Fields;

namespace Minakuru.Engine.ThreatCheckers.Experimental;

public class StraightLineThreatChecker : IThreatChecker
{
	private static readonly ulong[,][] _directionBitmasks = StraightLineBitmasks.StraightLinePerDirectionFieldBitmasks;
	private static readonly ulong[] _allDirectionsBitmasks = StraightLineBitmasks.StraightLineAllDirectionsFieldBitmasks;

	public bool IsUnderAttack(Board board, byte targetFieldNo, Color attackedByColor)
	{
		var piecesAt = board.Pieces;

		var opponentRooksQueens = attackedByColor == Color.White ? board.WhiteQueens | board.WhiteRooks : board.BlackQueens | board.BlackRooks;

		if ((opponentRooksQueens & _allDirectionsBitmasks[targetFieldNo]) == 0 )
		{
			return false;
		}

		for (int direction = 0; direction < 4; direction++)
		{
			bool occupied = false;
			ulong[] fieldIds = _directionBitmasks[targetFieldNo, direction];
			byte i = 0;
			while (!occupied && i < fieldIds.Length)
			{
				var fieldId = fieldIds[i];
				if ((piecesAt & fieldId) != 0)
				{
					if ((opponentRooksQueens & fieldId) != 0)
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
