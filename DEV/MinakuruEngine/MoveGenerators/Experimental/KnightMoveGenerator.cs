using System.Numerics;
using Minakuru.Engine.Fields;

namespace Minakuru.Engine.MoveGenerators.Experimental;

public class KnightMoveGenerator : IMoveGenerator
{
	private static readonly ulong[] _bitmasks = KnightBitmasks.KnightFieldBitmasks;

	public MoveList GenerateMove(Board board)
	{
		MoveList moveList = [];
		var color = board.ColorToMove;
		var whitePiecesAt = board.WhitePieces;
		var blackPiecesAt = board.BlackPieces;

		var knights = color == Color.White ? board.WhiteKnights : board.BlackKnights;
		var ownPiecesAt = color == Color.White ? whitePiecesAt : blackPiecesAt;
		var opponentPiecesAt = color == Color.White ? blackPiecesAt : whitePiecesAt;

		var workingCopy = knights;
		byte from = 0;
		while (workingCopy != 0UL)
		{
			byte trailingZeroesFrom = (byte)BitOperations.TrailingZeroCount(workingCopy);
			from += trailingZeroesFrom;
			workingCopy >>= trailingZeroesFrom;
			var toFields = _bitmasks[from] & ~ownPiecesAt;

			while (toFields != 0UL)
			{
				byte toFieldNo = (byte)BitOperations.TrailingZeroCount(toFields);

				ulong toFilter = (ulong)1 << toFieldNo;

				bool isCapture = (opponentPiecesAt & toFilter) != 0;
				moveList.Add(new Move(from, toFieldNo, isCapture));
				toFields &= ~toFilter;
			}
			workingCopy &= ~1UL;
		}
		return moveList;
	}
}
