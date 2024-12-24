
namespace Engine.ThreatCheckers
{
	public class PawnThreatChecker : IThreatChecker
	{
		public bool IsUnderAttack(Board board, byte targetFieldNo, Color color)
		{
			var pawns = color == Color.White ? board.WhitePawns : board.BlackPawns;

			var fromColumnNo = targetFieldNo % 8;
			ulong pawnsFilter = 0L;

			if (fromColumnNo > 0)
			{
				if (color == Color.White)
				{
					pawnsFilter |= (ulong)1 << (targetFieldNo - 8 - 1);
				}
				else if (color == Color.Black)
				{
					pawnsFilter |= (ulong)1 << (targetFieldNo + 8 - 1);
				}
			}
			if (fromColumnNo < 7)
			{
				if (color == Color.White)
				{
					pawnsFilter |= (ulong)1 << (targetFieldNo - 8 + 1);
				}
				else if (color == Color.Black)
				{
					pawnsFilter |= (ulong)1 << (targetFieldNo + 8 + 1);
				}
			}

			return (pawns & pawnsFilter) != 0;
		}
	}
}
