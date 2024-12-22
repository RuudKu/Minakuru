using System.Text;

namespace Engine;

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
			if ((board.Specials & Board.ColorToMoveFilter) == 0)
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
			if ((board.Specials & Board.WhiteCantCastleShortFilter) == 0)
			{
				sb.Append('K');
			}
			if ((board.Specials & Board.WhiteCantCastleLongFilter) == 0)
			{
				sb.Append('Q');
			}
			if ((board.Specials & Board.BlackCantCastleShortFilter) == 0)
			{
				sb.Append('k');
			}
			if ((board.Specials & Board.BlackCantCastleLongFilter) == 0)
			{
				sb.Append('q');
			}
		}

		void AddEnPassantTargetSquare(Board board, StringBuilder sb)
		{
			sb.Append(' ');
			if ((board.Specials & Board.EnPassantPossibleFilter) == 0)
			{
				sb.Append('-');
			}
			else
			{
				bool whiteToMove = (board.Specials & Board.ColorToMoveFilter) == 0;
				byte columnNo = (byte)(board.Specials & Board.EnPassantTargetColumnFilter);
				char column = (char)((byte)'a' + columnNo);
				sb.Append(column);
				sb.Append(whiteToMove ? '6' : '3');
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
		void SetPiecePlacement(Board board, string pieces)
		{
			var piecesPerRow = pieces.Split('/');
			for (sbyte rowNo = 7; rowNo >= 0; rowNo--)
			{
				var piecesThisRow = piecesPerRow[7 - rowNo];
				piecesThisRow = piecesThisRow.Replace("8", "........");
				piecesThisRow = piecesThisRow.Replace("7", ".......");
				piecesThisRow = piecesThisRow.Replace("6", "......");
				piecesThisRow = piecesThisRow.Replace("5", ".....");
				piecesThisRow = piecesThisRow.Replace("4", "....");
				piecesThisRow = piecesThisRow.Replace("3", "...");
				piecesThisRow = piecesThisRow.Replace("2", "..");
				piecesThisRow = piecesThisRow.Replace("1", ".");

				for (byte columnNo = 0; columnNo < 8; columnNo++)
				{
					ulong filter =((ulong)1) << (rowNo * 8 + columnNo);

					switch (piecesThisRow[columnNo])
					{
						case 'K':
							board.WhiteKing |= filter;
							break;
						case 'Q':
							board.WhiteQueens |= filter;
							break;
						case 'R':
							board.WhiteRooks |= filter;
							break;
						case 'B':
							board.WhiteBishops |= filter;
							break;
						case 'N':
							board.WhiteKnights |= filter;
							break;
						case 'P':
							board.WhitePawns |= filter;
							break;
						case 'k':
							board.BlackKing |= filter;
							break;
						case 'q':
							board.BlackQueens |= filter;
							break;
						case 'r':
							board.BlackRooks |= filter;
							break;
						case 'b':
							board.BlackBishops |= filter;
							break;
						case 'n':
							board.BlackKnights |= filter;
							break;
						case 'p':
							board.BlackPawns |= filter;
							break;
						case '.':
							break;
						default: throw new NotImplementedException();
					}
				}
			}
		}

		void SetSideToMove(Board board, string part)
		{
			if (part == "b")
			{
				board.Specials |= Board.ColorToMoveFilter;
			}
		}

		void SetCastlingAbility(Board board, string part)
		{
			foreach (var c in part)
			{
				switch (c)
				{
					case 'K':
						board.Specials &= ~Board.WhiteCantCastleShortFilter;
						break;
					case 'Q':
						board.Specials &= ~Board.WhiteCantCastleLongFilter;
						break;
					case 'k':
						board.Specials &= ~Board.BlackCantCastleShortFilter;
						break;
					case 'q':
						board.Specials &= ~Board.BlackCantCastleLongFilter;
						break;
					case '-':
						break;
					default:
						throw new InvalidCastException();
				}
			}
		}

		void SetEnPassantTargetSquare(Board board, string part)
		{
			if (part == "-")
			{
				return;
			}

			byte columnNo = (byte) (part[0] - 'a');
			board.Specials |= columnNo;
			board.Specials |= Board.EnPassantPossibleFilter;
		}

		void SetHalfMoveClock(Board board, string part)
		{
			board.HalfMoveClock = byte.Parse(part);
		}

		void SetFullMoveCounter(Board board, string part)
		{
			board.FullMoveCounter = byte.Parse(part);
		}

		var board = new Board();

		var parts = fen.Trim().Split(' ');

		SetPiecePlacement(board, parts[0]);

		SetSideToMove(board, parts[1]);

		SetCastlingAbility(board, parts[2]);

		SetEnPassantTargetSquare(board, parts[3]);

		SetHalfMoveClock(board, parts[4]);

		SetFullMoveCounter(board, parts[5]);

		return board;
	}
}
