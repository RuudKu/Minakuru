
namespace Engine;

public enum ColoredPiece
{
	Empty = 0,
	WhitePawn = 1 << 0,
	WhiteKnight = 1 << 1,
	WhiteBishop = 1 << 2,
	WhiteRook = 1 << 3,
	WhiteQueen = 1 << 4,
	WhiteKing = 1 << 5,

	BlackPawn = 1 << 6,
	BlackKnight = 1 << 7,
	BlackBishop = 1 << 8,
	BlackRook = 1 << 9,
	BlackQueen = 1 << 10,
	BlackKing = 1 << 11
}
