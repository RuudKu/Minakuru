using System.Text;

namespace Engine
{
	public static class FenConverter
	{
		public static string ToFen(this Board board)
		{
			char ToChar(ColoredPiece coloredPiece)
			{
				return coloredPiece switch
				{
					// white pieces
					ColoredPiece.WhiteKing => 'K',
					ColoredPiece.WhiteQueen => 'Q',
					ColoredPiece.WhiteRook => 'R',
					ColoredPiece.WhiteBishop => 'B',
					ColoredPiece.WhiteKnight => 'N',
					ColoredPiece.WhitePawn => 'P',
					// black pieces
					ColoredPiece.BlackKing => 'k',
					ColoredPiece.BlackQueen => 'q',
					ColoredPiece.BlackRook => 'r',
					ColoredPiece.BlackBishop => 'b',
					ColoredPiece.BlackKnight => 'n',
					ColoredPiece.BlackPawn => 'p',
					ColoredPiece.Empty => ' ',
					_ => throw new NotImplementedException()
				};
			}

			void AddPiecePlacement(Board board, StringBuilder sb)
			{
				for (sbyte rowNo = 7; rowNo >= 0; rowNo--)
				{
					int emptyFieldsInARow = 0;
					for (byte columnNo = 0; columnNo < 8; columnNo++)
					{
						var coloredPiece = board.PieceAt(columnNo, (byte)rowNo);
						var c = ToChar(coloredPiece);
						if (c == ' ')
						{
							emptyFieldsInARow++;
						}
						else
						{
							if (emptyFieldsInARow > 0)
							{
								sb.Append(emptyFieldsInARow);
								emptyFieldsInARow = 0;
							}
							sb.Append(c);
						}
					}
					if (emptyFieldsInARow > 0)
					{
						sb.Append(emptyFieldsInARow);
					}
					if (rowNo > 0)
					{
						sb.Append('/');
					}
				}
			}

			void AddSideToMove(Board board, StringBuilder sb)
			{
				if ((board.Specials & Board.ColorToMove) == 0)
				{
					sb.Append(" w ");
				}
				else
				{
					sb.Append(" b ");
				}
			}

			void AddCastlingAbility(Board board, StringBuilder sb)
			{
				if ((board.Specials & Board.WhiteCantCastleShort) == 0)
				{
					sb.Append('K');
				}
				if ((board.Specials & Board.WhiteCantCastleLong) == 0)
				{
					sb.Append('Q');
				}
				if ((board.Specials & Board.BlackCantCastleShort) == 0)
				{
					sb.Append('k');
				}
				if ((board.Specials & Board.BlackCantCastleLong) == 0)
				{
					sb.Append('q');
				}
			}

			void AddEnPassantTargetSquare(Board board, StringBuilder sb)
			{
				sb.Append(' ');
				if ((board.Specials & Board.EnPassantPossible) == 0)
				{
					sb.Append('-');
				}
				else
				{
					bool whiteToMove = (board.Specials & Board.ColorToMove) == 0;
					byte columnNo = (byte)(board.Specials & Board.EnPassantColumnFilter);
					char column = (char)((byte)'a' + columnNo);
					sb.Append(column);
					sb.Append(whiteToMove ? '7' : '2');
					sb.Append(column);
					sb.Append(whiteToMove ? '5' : '4');
				}
			}

			void AddHalfMoveClock(Board board, StringBuilder sb)
			{
				sb.Append(" " + board.HalfMoveClock);
			}

			void AddFullMoveCounter(Board board, StringBuilder sb)
			{
				sb.Append(" " + board.FullMoveCounter);
			}

			var sb = new StringBuilder();

			AddPiecePlacement(board, sb);

			AddSideToMove(board, sb);

			AddCastlingAbility(board, sb);

			AddEnPassantTargetSquare(board, sb);

			AddHalfMoveClock(board, sb);

			AddFullMoveCounter(board, sb);

			return sb.ToString();
		}

		public static Board ToBoard(this string fen)
		{
			return new Board();
		}
	}
}
