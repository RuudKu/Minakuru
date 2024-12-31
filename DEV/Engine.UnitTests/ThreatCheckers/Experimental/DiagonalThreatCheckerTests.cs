using FluentAssertions;
using Minakuru.Engine.ThreatCheckers;

namespace Minakuru.Engine.UnitTests.ThreatCheckers.Experimental;

[TestClass]
public class DiagonalThreatCheckerTests
{
	[DataRow(ColoredPiece.BlackQueen, "a1", false, DisplayName = "Qa1")]
	[DataRow(ColoredPiece.BlackBishop, "c3", true, DisplayName = "Bc3")]
	[DataRow(ColoredPiece.BlackQueen, "e3", true, DisplayName = "Qe3")]
	[DataRow(ColoredPiece.BlackBishop, "g1", true, DisplayName = "Bg1")]
	[DataRow(ColoredPiece.BlackQueen, "h8", false, DisplayName = "Qh8")]
	[DataRow(ColoredPiece.BlackBishop, "e5", true, DisplayName = "Be5")]
	[DataRow(ColoredPiece.BlackQueen, "a7", true, DisplayName = "Qa7")]
	[DataRow(ColoredPiece.BlackBishop, "c5", true, DisplayName = "Bc5")]
	[DataRow(ColoredPiece.BlackBishop, "d5", false, DisplayName = "Bd5")]
	[DataTestMethod]
	public void WhiteKingOnD4Tests(ColoredPiece coloredPiece, string fieldName, bool expected)
	{
		var fen = "6k1/8/5r2/8/3K4/8/1B6/8 w - - 0 1";
		var board = fen.ToBoard();
		var sut = new DiagonalThreatChecker();
		byte kingFieldNo = "d4".ToFieldNo();
		byte attackerFieldNo = fieldName.ToFieldNo();
		board.SetColoredPieceAt(attackerFieldNo, coloredPiece);

		var actual = sut.IsUnderAttack(board, kingFieldNo, Color.Black);

		actual.Should().Be(expected);
	}

	[DataRow(ColoredPiece.WhiteQueen, "a1", false, DisplayName = "Qa1")]
	[DataRow(ColoredPiece.WhiteBishop, "c3", true, DisplayName = "Bc3")]
	[DataRow(ColoredPiece.WhiteQueen, "e3", true, DisplayName = "Qe3")]
	[DataRow(ColoredPiece.WhiteBishop, "g1", true, DisplayName = "Bg1")]
	[DataRow(ColoredPiece.WhiteQueen, "h8", false, DisplayName = "Qh8")]
	[DataRow(ColoredPiece.WhiteBishop, "e5", true, DisplayName = "Be5")]
	[DataRow(ColoredPiece.WhiteQueen, "a7", true, DisplayName = "Qa7")]
	[DataRow(ColoredPiece.WhiteBishop, "c5", true, DisplayName = "Bc5")]
	[DataRow(ColoredPiece.WhiteBishop, "d5", false, DisplayName = "Bd5")]
	[DataTestMethod]
	public void BlackKingOnD4Tests(ColoredPiece coloredPiece, string fieldName, bool expected)
	{
		var fen = "6K1/8/5R2/8/3k4/8/1b6/8 b - - 0 1";
		var board = fen.ToBoard();
		var sut = new DiagonalThreatChecker();
		byte kingFieldNo = "d4".ToFieldNo();
		byte attackerFieldNo = fieldName.ToFieldNo();
		board.SetColoredPieceAt(attackerFieldNo, coloredPiece);

		var actual = sut.IsUnderAttack(board, kingFieldNo, Color.White);

		actual.Should().Be(expected);
	}
}

