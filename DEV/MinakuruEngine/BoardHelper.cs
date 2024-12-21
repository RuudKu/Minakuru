namespace Engine;

public static class BoardHelper
{
	public static Board StartPosition()
	{
		var board = new Board();

		board.WhiteKing = 0b_00010000;
		board.WhiteQueens = 0b_00001000;
		board.WhiteRooks = 0b_10000001;
		board.WhiteBishops = 0b_00100100;
		board.WhiteKnights = 0b_01000010;
		board.WhitePawns = ((ulong)0b_11111111) << 8;

		board.BlackKing = ((ulong)0b_00010000) << 56;
		board.BlackQueens = ((ulong)0b_00001000) << 56;
		board.BlackRooks = ((ulong)0b_10000001) << 56;
		board.BlackBishops = ((ulong)0b_00100100) << 56;
		board.BlackKnights = ((ulong)0b_01000010) << 56;
		board.BlackPawns = ((ulong)0b_11111111) << 48;

		board.Specials = 0;
		return board;
	}
}
