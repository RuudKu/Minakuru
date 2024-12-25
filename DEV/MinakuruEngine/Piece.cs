namespace Minakuru.Engine;

[Flags]
public enum Piece
{
	None = 0,
	Pawn = 1,
	Knight = 1 << 1,
	Bishop = 1 << 2,
	Rook = 1 << 3,
	Queen = 1 << 4,
	King = 1 << 5,
	Any = 63,
}
