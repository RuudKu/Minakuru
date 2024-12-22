
namespace Engine
{
	public static class MoveMaker
	{
		public static Board MakeMove(Board board, Move move)
		{
			var newBoard = board with { };

			var colorToMove = (board.Specials & Board.ColorToMoveFilter) == 0 ? Color.White : Color.Black;
			ulong fromFilter = (ulong)1 << move.From;
			ulong toFilter = (ulong)1 << move.To;

			Piece fromPiece = Piece.Any;
			if (colorToMove == Color.White)
			{
				if ((newBoard.WhitePawns & fromFilter) != 0)
				{
					fromPiece = Piece.Pawn;
					newBoard.WhitePawns &= ~fromFilter;
				}
				else if ((newBoard.WhiteKnights & fromFilter) != 0)
				{
					fromPiece = Piece.Knight;
					newBoard.WhiteKnights &= ~fromFilter;
				}
				else if ((newBoard.WhiteBishops & fromFilter) != 0)
				{
					fromPiece = Piece.Bishop;
					newBoard.WhiteBishops &= ~fromFilter;
				}
				else if ((newBoard.WhiteRooks & fromFilter) != 0)
				{
					fromPiece = Piece.Rook;
					newBoard.WhiteRooks &= ~fromFilter;
				}
				else if ((newBoard.WhiteQueens & fromFilter) != 0)
				{
					fromPiece = Piece.Queen;
					newBoard.WhiteQueens &= ~fromFilter;
				}
				else if ((newBoard.WhiteKing & fromFilter) != 0)
				{
					fromPiece = Piece.King;
					newBoard.WhiteKing &= ~fromFilter;
				}
			}
			else if (colorToMove == Color.Black)
			{
				if ((newBoard.BlackPawns & fromFilter) != 0)
				{
					fromPiece = Piece.Pawn;
					newBoard.BlackPawns &= ~fromFilter;
				}
				else if ((newBoard.BlackKnights & fromFilter) != 0)
				{
					fromPiece = Piece.Knight;
					newBoard.BlackKnights &= ~fromFilter;
				}
				else if ((newBoard.BlackBishops & fromFilter) != 0)
				{
					fromPiece = Piece.Bishop;
					newBoard.BlackBishops &= ~fromFilter;
				}
				else if ((newBoard.BlackRooks & fromFilter) != 0)
				{
					fromPiece = Piece.Rook;
					newBoard.BlackRooks &= ~fromFilter;
				}
				else if ((newBoard.BlackQueens & fromFilter) != 0)
				{
					fromPiece = Piece.Queen;
					newBoard.BlackQueens &= ~fromFilter;
				}
				else if ((newBoard.BlackKing & fromFilter) != 0)
				{
					fromPiece = Piece.King;
					newBoard.BlackKing &= ~fromFilter;
				}
			}

			if (colorToMove == Color.White)
			{
				switch (fromPiece)
				{
					case Piece.Pawn:
						newBoard.WhitePawns |= toFilter;
						int fromRow = move.From / 8;
						int toRow = move.To / 8;
						if (fromRow == 1 && toRow == 3)
						{
							newBoard.Specials |= Board.EnPassantPossibleFilter;
							newBoard.Specials &= ~Board.EnPassantTargetColumnFilter;
							newBoard.Specials |= (ulong)move.From % 8;
						}
						break;
					case Piece.Knight:
						newBoard.WhiteKnights |= toFilter;
						break;
					case Piece.Bishop:
						newBoard.WhiteBishops |= toFilter;
						break;
					case Piece.Rook:
						newBoard.WhiteRooks |= toFilter;
						break;
					case Piece.Queen:
						newBoard.WhiteQueens |= toFilter;
						break;
					case Piece.King:
						newBoard.WhiteKing |= toFilter;
						break;
					default:
						throw new NotSupportedException();
				}
			}
			else if (colorToMove == Color.Black)
			{
				switch (fromPiece)
				{
					case Piece.Pawn:
						board.BlackPawns |= toFilter;
						break;
					case Piece.Knight:
						board.BlackKnights |= toFilter;
						break;
					case Piece.Bishop:
						board.BlackBishops |= toFilter;
						break;
					case Piece.Rook:
						board.BlackRooks |= toFilter;
						break;
					case Piece.Queen:
						board.BlackQueens |= toFilter;
						break;
					case Piece.King:
						board.BlackKing |= toFilter;
						break;
					default:
						throw new NotSupportedException();
				}
			}

			newBoard.Specials &= ~Board.ColorToMoveFilter;
			if (colorToMove == Color.White)
			{
				newBoard.Specials |= Board.ColorToMoveFilter;
			}

			return newBoard;
		}
	}
}
