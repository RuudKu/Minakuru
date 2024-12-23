using System.Text;

namespace Engine;

public record Board
{

	// Specials filter
	private const ulong EnPassantTargetColumnFilter = 0b_0111;  // bits 0..2
	private const ulong EnPassantPossibleFilter = 1 << 3;
	private const ulong ColorToMoveFilter = 1 << 4;
	private const ulong WhiteCantCastleShortFilter = 1 << 5;
	private const ulong WhiteCantCastleLongFilter = 1 << 6;
	private const ulong BlackCantCastleShortFilter = 1 << 7;
	private const ulong BlackCantCastleLongFilter = 1 << 8;

	internal ulong WhitePawns { get; private set; }
	internal ulong WhiteKnights { get; private set; }
	internal ulong WhiteBishops { get; private set; }
	internal ulong WhiteRooks { get; private set; }
	internal ulong WhiteQueens { get; private set; }
	internal ulong WhiteKing { get; private set; }

	internal ulong BlackPawns { get; private set; }
	internal ulong BlackKnights { get; private set; }
	internal ulong BlackBishops { get; private set; }
	internal ulong BlackRooks { get; private set; }
	internal ulong BlackQueens { get; private set; }
	internal ulong BlackKing { get; private set; }

	private ulong Specials;
	public byte HalfMoveClock { get; set; }
	public byte FullMoveCounter { get; set; } = 1;

	public ulong WhitePieces
	{
		get
		{
			return WhiteKing | WhiteQueens | WhiteRooks | WhiteBishops | WhiteKnights | WhitePawns;
		}
	}

	public ulong BlackPieces
	{
		get
		{
			return BlackKing | BlackQueens | BlackRooks | BlackBishops | BlackKnights | BlackPawns;
		}
	}

	public ulong Pieces
	{
		get
		{
			return WhiteKing | WhiteQueens | WhiteRooks | WhiteBishops | WhiteKnights | WhitePawns |
				BlackKing | BlackQueens | BlackRooks | BlackBishops | BlackKnights | BlackPawns;
		}
	}

	public void EmptyField(byte fieldNo, ColoredPiece coloredPiece)
	{
		ulong filter = (ulong)1 << fieldNo;
		switch (coloredPiece)
		{
			case ColoredPiece.WhiteKing:
				WhiteKing &= ~filter;
				break;
			case ColoredPiece.WhiteQueen:
				WhiteQueens &= ~filter;
				break;
			case ColoredPiece.WhiteRook:
				WhiteRooks &= ~filter;
				break;
			case ColoredPiece.WhiteBishop:
				WhiteBishops &= ~filter;
				break;
			case ColoredPiece.WhiteKnight:
				WhiteKnights &= ~filter;
				break;
			case ColoredPiece.WhitePawn:
				WhitePawns &= ~filter;
				break;

			case ColoredPiece.BlackKing:
				BlackKing &= ~filter;
				break;
			case ColoredPiece.BlackQueen:
				BlackQueens &= ~filter;
				break;
			case ColoredPiece.BlackRook:
				BlackRooks &= ~filter;
				break;
			case ColoredPiece.BlackBishop:
				BlackBishops &= ~filter;
				break;
			case ColoredPiece.BlackKnight:
				BlackKnights &= ~filter;
				break;
			case ColoredPiece.BlackPawn:
				BlackPawns &= ~filter;
				break;

			default:
				throw new Exception();
		}
	}

	public void Clear()
	{
		WhitePawns = 0;
		WhiteKnights = 0;
		WhiteBishops = 0;
		WhiteRooks = 0;
		WhiteQueens = 0;
		WhiteKing = 0;

		BlackPawns = 0;
		BlackKnights = 0;
		BlackBishops = 0;
		BlackRooks = 0;
		BlackQueens = 0;
		BlackKing = 0;

		Specials = 0;
		HalfMoveClock = 0;
		FullMoveCounter = 1;
	}

	public ColoredPiece? GetColoredPieceAt(byte fieldNo)
	{
		var filter = (ulong)1 << fieldNo;
		if ((WhitePawns & filter) != 0)
		{
			return ColoredPiece.WhitePawn;
		}
		if ((BlackPawns & filter) != 0)
		{
			return ColoredPiece.BlackPawn;
		}
		if ((WhiteKnights & filter) != 0)
		{
			return ColoredPiece.WhiteKnight;
		}
		if ((BlackKnights & filter) != 0)
		{
			return ColoredPiece.BlackKnight;
		}
		if ((WhiteBishops & filter) != 0)
		{
			return ColoredPiece.WhiteBishop;
		}
		if ((BlackBishops & filter) != 0)
		{
			return ColoredPiece.BlackBishop;
		}
		if ((WhiteRooks & filter) != 0)
		{
			return ColoredPiece.WhiteRook;
		}
		if ((BlackRooks & filter) != 0)
		{
			return ColoredPiece.BlackRook;
		}
		if ((WhiteQueens & filter) != 0)
		{
			return ColoredPiece.WhiteQueen;
		}
		if ((BlackQueens & filter) != 0)
		{
			return ColoredPiece.BlackQueen;
		}
		if ((WhiteKing & filter) != 0)
		{
			return ColoredPiece.WhiteKing;
		}
		if ((BlackKing & filter) != 0)
		{
			return ColoredPiece.BlackKing;
		}
		return null;
	}

	public ColoredPiece? GetColoredPieceAt(byte fieldNo, Color color)
	{
		var filter = (ulong)1 << fieldNo;
		switch (color)
		{
			case Color.White:
				if ((WhitePawns & filter) != 0)
				{
					return ColoredPiece.WhitePawn;
				}
				if ((WhiteKnights & filter) != 0)
				{
					return ColoredPiece.WhiteKnight;
				}
				if ((WhiteBishops & filter) != 0)
				{
					return ColoredPiece.WhiteBishop;
				}
				if ((WhiteRooks & filter) != 0)
				{
					return ColoredPiece.WhiteRook;
				}
				if ((WhiteQueens & filter) != 0)
				{
					return ColoredPiece.WhiteQueen;
				}
				if ((WhiteKing & filter) != 0)
				{
					return ColoredPiece.WhiteKing;
				}
				break;

			case Color.Black:
				if ((BlackPawns & filter) != 0)
				{
					return ColoredPiece.BlackPawn;
				}
				if ((BlackKnights & filter) != 0)
				{
					return ColoredPiece.BlackKnight;
				}
				if ((BlackBishops & filter) != 0)
				{
					return ColoredPiece.BlackBishop;
				}
				if ((BlackRooks & filter) != 0)
				{
					return ColoredPiece.BlackRook;
				}
				if ((BlackQueens & filter) != 0)
				{
					return ColoredPiece.BlackQueen;
				}
				if ((BlackKing & filter) != 0)
				{
					return ColoredPiece.BlackKing;
				}
				break;

			default:
				throw new NotImplementedException();
		}
		return null;
	}

	public void SetColoredPieceAt(byte fieldNo, ColoredPiece coloredPiece)
	{
		var filter = (ulong)1 << fieldNo;
		switch (coloredPiece)
		{
			case ColoredPiece.WhitePawn:
				WhitePawns |= filter;
				break;
			case ColoredPiece.BlackPawn:
				BlackPawns |= filter;
				break;
			case ColoredPiece.WhiteKnight:
				WhiteKnights |= filter;
				break;
			case ColoredPiece.BlackKnight:
				BlackKnights |= filter;
				break;
			case ColoredPiece.WhiteBishop:
				WhiteBishops |= filter;
				break;
			case ColoredPiece.BlackBishop:
				BlackBishops |= filter;
				break;
			case ColoredPiece.WhiteRook:
				WhiteRooks |= filter;
				break;
			case ColoredPiece.BlackRook:
				BlackRooks |= filter;
				break;
			case ColoredPiece.WhiteQueen:
				WhiteQueens |= filter;
				break;
			case ColoredPiece.BlackQueen:
				BlackQueens |= filter;
				break;
			case ColoredPiece.WhiteKing:
				WhiteKing |= filter;
				break;
			case ColoredPiece.BlackKing:
				BlackKing |= filter;
				break;
			default:
				throw new NotImplementedException();
		}
	}

	public void ToggleColorToMove()
	{
		Specials ^= ColorToMoveFilter;
	}

	public Color ColorToMove
	{
		get
		{
			return (Specials & ColorToMoveFilter) == 0 ? Color.White : Color.Black;
		}
		set
		{
			switch (value)
			{
				case Color.White:
					Specials &= ~ColorToMoveFilter;
					break;
				case Color.Black:
					Specials |= ColorToMoveFilter;
					break;
				default:
					throw new NotSupportedException();
			}
		}
	}

	public bool WhiteCanCastleShort
	{
		get
		{
			return (Specials & WhiteCantCastleShortFilter) == 0;
		}
		set
		{
			if (value)
			{
				Specials &= ~WhiteCantCastleShortFilter;
			}
			else
			{
				Specials |= WhiteCantCastleShortFilter;
			}
		}
	}

	public bool WhiteCanCastleLong
	{
		get
		{
			return (Specials & WhiteCantCastleLongFilter) == 0;
		}
		set
		{
			if (value)
			{
				Specials &= ~WhiteCantCastleLongFilter;
			}
			else
			{
				Specials |= WhiteCantCastleLongFilter;
			}
		}
	}

	public bool BlackCanCastleShort
	{
		get
		{
			return (Specials & BlackCantCastleShortFilter) == 0;
		}
		set
		{
			if (value)
			{
				Specials &= ~BlackCantCastleShortFilter;
			}
			else
			{
				Specials |= BlackCantCastleShortFilter;
			}
		}
	}

	public bool BlackCanCastleLong
	{
		get
		{
			return (Specials & BlackCantCastleLongFilter) == 0;
		}
		set
		{
			if (value)
			{
				Specials &= ~BlackCantCastleLongFilter;
			}
			else
			{
				Specials |= BlackCantCastleLongFilter;
			}
		}
	}

	public bool EnPassantPossible
	{
		get
		{
			return (Specials & EnPassantPossibleFilter) != 0;
		}
		set
		{
			if (value)
			{
				Specials |= EnPassantPossibleFilter;
			}
			else
			{
				Specials &= ~EnPassantPossibleFilter;
			}
		}
	}

	public byte EnPassantTargetColumn
	{
		get
		{
			return (byte)(Specials & EnPassantTargetColumnFilter);
		}
		set
		{
			Specials &= ~EnPassantTargetColumnFilter;
			Specials |= value;
		}
	}

	public override string ToString()
	{
		static void FindPieces(StringBuilder sb, string mnemonic, ulong bits)
		{
			sb.Append(mnemonic + ':');
			int count = 0;
			for (int rowNo = 0; rowNo < 8; rowNo++)
			{
				for (int columnNo = 0; columnNo < 8; columnNo++)
				{
					var filter = (ulong)1 << (rowNo * 8 + columnNo);
					if ((bits & filter) != 0)
					{
						if (count > 0)
						{
							sb.Append(',');
						}

						sb.Append((char)('a' + columnNo));
						sb.Append((char)('1' + rowNo));

						count++;
					}
				}
			}
			if (count == 0)
			{
				sb.Append(' ');
			}
			sb.Append(' ');
		}

		var sb = new StringBuilder();
		FindPieces(sb, "WK", WhiteKing);
		FindPieces(sb, "WQ", WhiteQueens);
		FindPieces(sb, "WR", WhiteRooks);
		FindPieces(sb, "WB", WhiteBishops);
		FindPieces(sb, "WN", WhiteKnights);
		FindPieces(sb, "WP", WhitePawns);

		FindPieces(sb, "BK", BlackKing);
		FindPieces(sb, "BQ", BlackQueens);
		FindPieces(sb, "BR", BlackRooks);
		FindPieces(sb, "BB", BlackBishops);
		FindPieces(sb, "BN", BlackKnights);
		FindPieces(sb, "BP", BlackPawns);
		return sb.ToString();
	}

	public static Board Init()
	{
		var board = new Board
		{
			WhiteKing = 0b_00010000,
			WhiteQueens = 0b_00001000,
			WhiteRooks = 0b_10000001,
			WhiteBishops = 0b_00100100,
			WhiteKnights = 0b_01000010,
			WhitePawns = ((ulong)0b_11111111) << 8,

			BlackKing = ((ulong)0b_00010000) << 56,
			BlackQueens = ((ulong)0b_00001000) << 56,
			BlackRooks = ((ulong)0b_10000001) << 56,
			BlackBishops = ((ulong)0b_00100100) << 56,
			BlackKnights = ((ulong)0b_01000010) << 56,
			BlackPawns = ((ulong)0b_11111111) << 48,

			Specials = 0
		};
		return board;
	}

	public bool IsEmpty(byte fieldNo)
	{
		var filter = (ulong)1 << fieldNo;
		return (Pieces & filter) == 0;
	}
}
