using System.Numerics;
using Minakuru.Engine.Fields;

namespace Minakuru.Engine.MoveGenerators;

public class BishopMoveGenerator : IMoveGenerator
{
	private static readonly byte[,][] _diagonalPerDirectionFieldNos = DiagonalFieldNo.DiagonalPerDirectionFieldNos;

	public void GenerateMove(Board board, MoveList moveList)
	{
		var color = board.ColorToMove;

		var bishops = color == Color.White ? board.WhiteBishops : board.BlackBishops;
		var workingCopy = bishops;

		(ulong ownPiecesAt, ulong opponentPiecesAt) = color == Color.White ? (board.WhitePieces, board.BlackPieces) : (board.BlackPieces, board.WhitePieces);

		byte from = 0;
		while (workingCopy != 0UL)
		{
			byte trailingZeroes = (byte)BitOperations.TrailingZeroCount(workingCopy);

			from += trailingZeroes;
			workingCopy >>= trailingZeroes;
			var filter = (ulong)1 << from;
			if ((bishops & filter) != 0)
			{
				for (int direction = 0; direction < 4; direction++)
				{
					byte[] fieldNos = _diagonalPerDirectionFieldNos[from, direction];

					bool ownPiece = false;
					bool isCapture = false;
					byte fieldIndex = 0;
					while (fieldIndex < fieldNos.Length && !ownPiece && !isCapture)
					{
						var fieldNo = fieldNos[fieldIndex];
						ulong toFilter = (ulong)1 << fieldNo;

						if ((ownPiecesAt & toFilter) == 0)
						{
							isCapture = (opponentPiecesAt & toFilter) != 0;
							moveList.Add(new Move(from, fieldNo, isCapture));
						}
						ownPiece = (ownPiecesAt & toFilter) != 0;

						fieldIndex++;
					}
				}

				workingCopy &= ~1UL;
			}
		}
	}
}
