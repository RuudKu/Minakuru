using System.Numerics;
using Minakuru.Engine.Fields;

namespace Minakuru.Engine.MoveGenerators.Experimental;

public class RookMoveGenerator : IMoveGenerator
{
	private static readonly byte[,][] _straightLinePerDirectionFieldNos = StraightLineFieldNo.StraightLinePerDirectionFieldNos;

	public void GenerateMove(Board board, MoveList moveList)
	{
		var color = board.ColorToMove;
		var rooks = color == Color.White ? board.WhiteRooks : board.BlackRooks;
		var workCopy = rooks;
		(ulong ownPiecesAt, ulong opponentPiecesAt) = color == Color.White ? (board.WhitePieces, board.BlackPieces) : (board.BlackPieces, board.WhitePieces);

		byte from = 0;
		while (workCopy != 0L)
		{
			int trailingZeroes = BitOperations.TrailingZeroCount(workCopy);
			from += (byte)trailingZeroes;
			workCopy >>= trailingZeroes;
			var filter = (ulong)1 << from;
			if ((rooks & filter) != 0)
			{
				for (int direction = 0; direction < 4; direction++)
				{
					byte[] fieldNos = _straightLinePerDirectionFieldNos[from, direction];

					bool ownPiece = false;
					bool isCapture = false;
					byte i = 0;
					while (i < fieldNos.Length && !ownPiece && !isCapture)
					{
						byte fieldNo = fieldNos[i];
						ulong toFilter = (ulong)1 << fieldNo;

						if ((ownPiecesAt & toFilter) == 0)
						{
							isCapture = (opponentPiecesAt & toFilter) != 0;
							moveList.Add(new Move(from, fieldNo, isCapture));
						}
						ownPiece = (ownPiecesAt & toFilter) != 0;

						i++;
					}
				}

				workCopy &= ~1UL;
			}
		}
	}
}
