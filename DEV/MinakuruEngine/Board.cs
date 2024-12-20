namespace Engine;

public class Board
{
	public const ulong WhiteKingMoved = 1 << 0;
	public const ulong WhiteARookMoved = 1 << 1;
	public const ulong WhiteHRookMoved = 1 << 2;
	public const ulong WhiteCantCastleShort = WhiteKingMoved | WhiteHRookMoved;
	public const ulong WhiteCantCastleLong = WhiteKingMoved | WhiteARookMoved;
	public const ulong BlackKingMoved = 1 << 3;
	public const ulong BlackARookMoved = 1 << 4;
	public const ulong BlackHRookMoved = 1 << 5;
	public const ulong BlackCantCastleShort = BlackKingMoved | BlackHRookMoved;
	public const ulong BlackCantCastleLong = BlackKingMoved | BlackARookMoved;

	// Specials filter
	public const ulong ColorToMove = 1 << 4;
	public const ulong EnPassantPossible = 1 << 3;
	public const ulong EnPassantColumnFilter = 0b_0111;  // bits 0..2

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
}
