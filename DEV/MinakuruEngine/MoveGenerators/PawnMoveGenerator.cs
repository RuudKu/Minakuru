namespace Minakuru.Engine.MoveGenerators;

public class PawnMoveGenerator : IMoveGenerator
{
	private readonly Piece[] PromotionToPieces = [Piece.Queen, Piece.Rook, Piece.Bishop, Piece.Knight];

	public IEnumerable<Move> GenerateMove(Board board)
	{
		var color = board.ColorToMove;
		var whitePiecesAt = board.WhitePieces;
		var blackPiecesAt = board.BlackPieces;

		var pawns = color == Color.White ? board.WhitePawns : board.BlackPawns;
		var opponentPiecesAt = color == Color.White ? blackPiecesAt : whitePiecesAt;
		var piecesAt = whitePiecesAt | blackPiecesAt;
		var enPassantPossible = board.EnPassantPossible;
		var enPassantTargetColumn = board.EnPassantTargetColumn;

		for (byte from = 0; from < 64; from++)
		{
			var filter = (ulong)1 << from;
			if ((pawns & filter) != 0)
			{
				int fromColumn = from % 8;
				int fromRow = from / 8;

				int toColumn;
				int toRow;

				toColumn = fromColumn;

				// move pawn 1 field
				int deltaRow = color == Color.White ? 1 : -1;
				toRow = fromRow + deltaRow;

				byte to = (byte)(8 * toRow + toColumn);
				ulong toFilter = (ulong)1 << to;

				if ((piecesAt & toFilter) == 0)
				{
					if (toRow == 0 || toRow == 7)
					{
						foreach (var promotionToPiece in PromotionToPieces)
						{
							yield return new Move(from, to, false, promotionToPiece);
						}
					}
					else
					{
						yield return new Move(from, to, false);
					}

					// move 2 up
					// note: only possible if at least 1 up was possible
					// note: only from 2nd rw (white) or 7th row (black)
					bool atStartingRow = color == Color.White && fromRow == 1 || color == Color.Black && fromRow == 6;

					if (atStartingRow)
					{
						toRow += deltaRow;
						to = (byte)(8 * toRow + toColumn);
						toFilter = (ulong)1 << to;
						if ((piecesAt & toFilter) == 0)
						{
							yield return new Move(from, to, false);
						}
					}
				}

				// capture to lower column
				if (fromColumn > 0)
				{
					toRow = fromRow + deltaRow;
					toColumn = fromColumn - 1;
					to = (byte)(8 * toRow + toColumn);
					toFilter = (ulong)1 << to;
					if ((opponentPiecesAt & toFilter) != 0)
					{
						if (toRow == 0 || toRow == 7)
						{
							// all promotions
							foreach (var promotionToPiece in PromotionToPieces)
							{
								yield return new Move(from, to, true, promotionToPiece);
							}
						}
						else
						{
							// no promotion
							yield return new Move(from, to, true);
						}
					}
				}

				// capture to higher column
				if (fromColumn < 7)
				{
					toRow = fromRow + deltaRow;
					toColumn = fromColumn + 1;
					to = (byte)(8 * toRow + toColumn);
					toFilter = (ulong)1 << to;
					if ((opponentPiecesAt & toFilter) != 0)
					{
						if (toRow == 0 || toRow == 7)
						{
							// all promotions
							foreach (var promotionToPiece in PromotionToPieces)
							{
								yield return new Move(from, to, true, promotionToPiece);
							}
						}
						else
						{
							// no promotion
							yield return new Move(from, to, true);
						}
					}
				}

				// en passant capture
				if (enPassantPossible)
				{
					var fromRowEnPassant = color == Color.White ? 4 : 3;
					if (fromRow == fromRowEnPassant)
					{
						toRow = fromRow + deltaRow;
						toColumn = enPassantTargetColumn;
						if (toColumn == fromColumn + 1 || toColumn == fromColumn - 1)
						{
							to = (byte)(8 * toRow + toColumn);
							yield return new Move(from, to, true);
						}
					}
				}

				pawns &= ~filter;
				if (pawns == 0)
				{
					break;
				}
			}
		}
	}
}
