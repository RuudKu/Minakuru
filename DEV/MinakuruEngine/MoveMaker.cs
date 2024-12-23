
using System.Net.Http.Headers;

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

			fromPiece = UpdatePiecePlacement(newBoard, board, move, colorToMove, fromFilter, toFilter, fromPiece);

			UpdateSideToMove(newBoard, colorToMove);

			UpdateCastlingAbility(newBoard, move);

			UpdateEnPassantPossibility(newBoard, fromPiece, move);

			UpdateHalfMoveClock(newBoard, move, fromPiece);

			UpdateFullMoveCounter(newBoard, colorToMove);

			return newBoard;
		}

		private static void UpdateFullMoveCounter(Board newBoard, Color colorToMove)
		{
			if (colorToMove == Color.Black)
			{
				newBoard.FullMoveCounter++;
			}
		}

		private static void UpdateHalfMoveClock(Board newBoard, Move move, Piece fromPiece)
		{
			if (fromPiece == Piece.Pawn || move.Capture)
			{
				newBoard.HalfMoveClock = 0;
			}
			else
			{
				newBoard.HalfMoveClock++;
			}
		}

		private static void UpdateEnPassantPossibility(Board newBoard, Piece fromPiece, Move move)
		{
			int fromRow = move.From / 8;
			int toRow = move.To / 8;

			if (fromPiece == Piece.Pawn && (fromRow == 1 || fromRow == 6) && (toRow == 3 || toRow == 4))
			{
				newBoard.Specials |= Board.EnPassantPossibleFilter;
				newBoard.Specials &= ~Board.EnPassantTargetColumnFilter;
				newBoard.Specials |= (ulong)move.From % 8;
			}
			else
			{
				newBoard.Specials &= ~(Board.EnPassantPossibleFilter | Board.EnPassantTargetColumnFilter);
			}
		}

		private static void UpdateCastlingAbility(Board newBoard, Move move)
		{
			switch (move.From)
			{
				case Field.A1FieldNo:
					newBoard.Specials |= Board.WhiteCantCastleLongFilter;
					break;
				case Field.E1FieldNo:
					newBoard.Specials |= (Board.WhiteCantCastleShortFilter | Board.WhiteCantCastleLongFilter);
					break;
				case Field.H1FieldNo:
					newBoard.Specials |= Board.WhiteCantCastleShortFilter;
					break;
				case Field.A8FieldNo:
					newBoard.Specials |= Board.BlackCantCastleLongFilter;
					break;
				case Field.E8FieldNo:
					newBoard.Specials |= (Board.BlackCantCastleShortFilter | Board.BlackCantCastleLongFilter);
					break;
				case Field.H8FieldNo:
					newBoard.Specials |= Board.BlackCantCastleShortFilter;
					break;
				default:
					break;
			}
		}

		private static void UpdateSideToMove(Board newBoard, Color colorToMove)
		{
			newBoard.Specials &= ~Board.ColorToMoveFilter;
			if (colorToMove == Color.White)
			{
				newBoard.Specials |= Board.ColorToMoveFilter;
			}
		}

		private static Piece UpdatePiecePlacement(Board newBoard, Board board, Move move, Color colorToMove, ulong fromFilter, ulong toFilter, Piece fromPiece)
		{
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

			return fromPiece;
		}
	}
}
