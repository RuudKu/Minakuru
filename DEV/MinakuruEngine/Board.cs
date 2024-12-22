using System.Text;

namespace Engine;

public record Board
{

	// Specials filter
	public const ulong EnPassantTargetColumnFilter = 0b_0111;  // bits 0..2
	public const ulong EnPassantPossibleFilter = 1 << 3;
	public const ulong ColorToMoveFilter = 1 << 4;
	public const ulong WhiteCantCastleShortFilter = 1 << 5;
	public const ulong WhiteCantCastleLongFilter = 1 << 6;
	public const ulong BlackCantCastleShortFilter = 1 << 7;
	public const ulong BlackCantCastleLongFilter = 1 << 8;

	public ulong WhitePawns { get; set; }
	public ulong WhiteKnights { get; set; }
	public ulong WhiteBishops { get; set; }
	public ulong WhiteRooks { get; set; }
	public ulong WhiteQueens { get; set; }
	public ulong WhiteKing { get; set; }

	public ulong BlackPawns { get; set; }
	public ulong BlackKnights { get; set; }
	public ulong BlackBishops { get; set; }
	public ulong BlackRooks { get; set; }
	public ulong BlackQueens { get; set; }
	public ulong BlackKing { get; set; }

	public ulong Specials { get; set; }
	public byte HalfMoveClock { get; set; }
	public byte FullMoveCounter { get; set; } = 1;

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
}
