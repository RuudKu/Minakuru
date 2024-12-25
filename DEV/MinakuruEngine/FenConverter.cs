using System.Text;

namespace Minakuru.Engine;

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
					byte fieldNo = (byte)(8 * rowNo + columnNo);
					var coloredPiece = board.GetColoredPieceAt(fieldNo);

					if (coloredPiece == ColoredPiece.Empty)
					{
						emptyFieldsInARow++;
					}
					else
					{
						var c = ToChar(coloredPiece);
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
			if (board.ColorToMove == Color.White)
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
			if (board.WhiteCanCastleShort)
			{
				sb.Append('K');
			}
			if (board.WhiteCanCastleLong)
			{
				sb.Append('Q');
			}
			if (board.BlackCanCastleShort)
			{
				sb.Append('k');
			}
			if (board.BlackCanCastleLong)
			{
				sb.Append('q');
			}
			if (!(board.WhiteCanCastle || board.BlackCanCastle))
			{
				sb.Append('-');
			}
		}

		void AddEnPassantTargetSquare(Board board, StringBuilder sb)
		{
			sb.Append(' ');
			if (board.EnPassantPossible)
			{
				bool whiteToMove = board.ColorToMove == Color.White;
				byte columnNo = board.EnPassantTargetColumn;
				char column = (char)((byte)'a' + columnNo);
				sb.Append(column);
				sb.Append(whiteToMove ? '6' : '3');
			}
			else
			{
				sb.Append('-');
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
				piecesThisRow = piecesThisRow.Replace("8", new string('.', 8));
				piecesThisRow = piecesThisRow.Replace("7", new string('.', 7));
				piecesThisRow = piecesThisRow.Replace("6", new string('.', 6));
				piecesThisRow = piecesThisRow.Replace("5", new string('.', 5));
				piecesThisRow = piecesThisRow.Replace("4", new string('.', 4));
				piecesThisRow = piecesThisRow.Replace("3", new string('.', 3));
				piecesThisRow = piecesThisRow.Replace("2", new string('.', 2));
				piecesThisRow = piecesThisRow.Replace("1", new string('.', 1));

				for (byte columnNo = 0; columnNo < 8; columnNo++)
				{
					byte fieldNo = (byte)(rowNo * 8 + columnNo);

					switch (piecesThisRow[columnNo])
					{
						case 'K':
							board.SetColoredPieceAt(fieldNo, ColoredPiece.WhiteKing);
							break;
						case 'Q':
							board.SetColoredPieceAt(fieldNo, ColoredPiece.WhiteQueen);
							break;
						case 'R':
							board.SetColoredPieceAt(fieldNo, ColoredPiece.WhiteRook);
							break;
						case 'B':
							board.SetColoredPieceAt(fieldNo, ColoredPiece.WhiteBishop);
							break;
						case 'N':
							board.SetColoredPieceAt(fieldNo, ColoredPiece.WhiteKnight);
							break;
						case 'P':
							board.SetColoredPieceAt(fieldNo, ColoredPiece.WhitePawn);
							break;
						case 'k':
							board.SetColoredPieceAt(fieldNo, ColoredPiece.BlackKing);
							break;
						case 'q':
							board.SetColoredPieceAt(fieldNo, ColoredPiece.BlackQueen);
							break;
						case 'r':
							board.SetColoredPieceAt(fieldNo, ColoredPiece.BlackRook);
							break;
						case 'b':
							board.SetColoredPieceAt(fieldNo, ColoredPiece.BlackBishop);
							break;
						case 'n':
							board.SetColoredPieceAt(fieldNo, ColoredPiece.BlackKnight);
							break;
						case 'p':
							board.SetColoredPieceAt(fieldNo, ColoredPiece.BlackPawn);
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
			board.ColorToMove = part switch
			{
				"w" => Color.White,
				"b" => Color.Black,
				_ => throw new NotSupportedException(),
			};
		}

		void SetCastlingAbility(Board board, string part)
		{
			foreach (var c in part)
			{
				switch (c)
				{
					case 'K':
						board.WhiteCanCastleShort = true;
						break;
					case 'Q':
						board.WhiteCanCastleLong = true;
						break;
					case 'k':
						board.BlackCanCastleShort = true;
						break;
					case 'q':
						board.BlackCanCastleLong = true;
						break;
					case '-':
						board.WhiteCanCastleShort = false;
						board.WhiteCanCastleLong = false;
						board.BlackCanCastleShort = false;
						board.BlackCanCastleLong = false;
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

			byte columnNo = (byte)(part[0] - 'a');
			board.EnPassantTargetColumn = columnNo;
			board.EnPassantPossible = true;
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
